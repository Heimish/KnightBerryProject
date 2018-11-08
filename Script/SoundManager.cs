using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaySoundId
{
    Bgm1 = 0,
    Walk_TankerStone,
    Walk_TankerGrass,
    Attack_Original,
    Attack_Scythe,
    Attack_Counter,
    Defense_Shield,
    Skill_ScytheWideCut,
    Hit_StandardMonster,
    Attack_Finish,
    Boss_Release = 10,
    Boss_FootHold,
    Boss_Arrow,
    Boss_Arrow_Spawn,
    Boss_Orb,
    Boss_Orb_Spawn,
    Hit_Pc,
    Walk_DealerStone,
    Walk_DealerGrass,
    Tanker_Dash,
    Dealer_Blink = 20,
    Dealer_QuickCut,
    Tanker_Knockback,
    Dealer_Holding,
    Boss_Walk,
    Boss_Teleport,
    Boss_Attack1,
    Boss_Attack2,
    Boss_Attack3,
    Boss_MonSpawn,
    Tanker_Swap = 30,
    Dealer_Swap,
    Tanker_Rush,
    Tanker_Defense,
    Vine,
    Vine_Fast,
    Monster_Death,
    Goblin_Missile,
    Goblin_Heal,
    ShildGoblin_Hit,
    GuardGoblin_AttackHit = 40,
    ShildGoblin_AttackHit,
    GuardGoblin_Sword,
    ShildGoblin_Sword,
    Bgm2,
    Boss_Groggy,
    Tanker_Rush_Hit,
}

[Serializable]
public struct PlaySoundType
{
    public PlaySoundId Id;
    [FMODUnity.EventRef]
    public string Path;
}

public class SoundManager : MonoBehaviour
{
    public GameObject isBossZone;
    private static SoundManager _instance;
    public static SoundManager I { get { return _instance; } }

    public PlaySoundType[] Sounds;
    FMOD.Studio.EventInstance bgmSound;
    FMOD.Studio.EventInstance BossbgmSound;
    FMOD.Studio.ParameterInstance bgmVolume;

    public float _Volume;
    private float _Stack;
    private float _WaitTime;
    WitchBoss _Boss;
    UiManager _UiMana;

    void Awake()
    {
        _instance = this;
        _Volume = 0;
    }

    void Start()
    {
        _Stack = 0;
        _WaitTime = 0;
    }

    public void Update()
    {
        _WaitTime += Time.deltaTime;
        if (_WaitTime > 7f)
        {
            _Boss = FindObjectOfType<WitchBoss>().GetComponent<WitchBoss>();
        }
        _UiMana = FindObjectOfType<UiManager>().GetComponent<UiManager>();

        bgmVolume.setValue(_Volume);
        SoundSet();

        if (isBossZone.activeInHierarchy || _Stack == 0 ||_UiMana.GotoTitle)
            SoundPlay(CPlayerManager._instance.transform);
    }


    public void SoundPlay(Transform Target)
    {
        if (isBossZone.activeInHierarchy || _UiMana.GotoTitle)
            bgmSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        if(_Stack == 0)
        { 
            bgmSound = FMODUnity.RuntimeManager.CreateInstance(GetSound(PlaySoundId.Bgm1));
            bgmSound.getParameter("Parameter 1", out bgmVolume);
            //FMODUnity.RuntimeManager.PlayOneShot(MyEvent1[SoundType], Target.position);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(bgmSound, Target, GetComponent<Rigidbody>());
            bgmSound.start();
            _Stack++;
        }
    }

    public void BossSoundPlay(Transform Target)
    {
        if (_Boss.IsDead)
        {
            BossbgmSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            return;
        }

            BossbgmSound = FMODUnity.RuntimeManager.CreateInstance(GetSound(PlaySoundId.Bgm2));
            BossbgmSound.getParameter("Parameter 1", out bgmVolume);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(BossbgmSound, Target, GetComponent<Rigidbody>());
            BossbgmSound.start();
        
    }

    public void SoundSet()
    {
        if (CPlayerManager._instance.isDead)
        {
            BossbgmSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            bgmSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void PlaySound(Transform target, PlaySoundId id)
    {
        PlaySound(target.position, id);
    }

    public void PlaySound(Vector3 target, PlaySoundId id)
    {
        FMODUnity.RuntimeManager.PlayOneShot(GetSound(id), CPlayerManager._instance.transform.position);
    }

    private string GetSound(PlaySoundId id)
    {
        foreach (PlaySoundType t in Sounds)
        {
            if (t.Id == id)
                return t.Path;
        }

        return "";
    }
}