using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialType
{
    Key = 0,
    Skill,
    More,
    End
}

public class ABUGameManager : Singleton<ABUGameManager>
{
    private MonsterObjectPool _monsterPool;
    private WitchMonsterPhaseManager _monsterPhase;

    //properties
    public MonsterObjectPool MonsterPool { get { return _monsterPool; } set { _monsterPool = value; } }
    public WitchMonsterPhaseManager MonsterPhase { get { return _monsterPhase; } set { _monsterPhase = value; } }

    public bool IsTutorial { get { return _tutorialType != TutorialType.End; } }
    public GameObject[] TutorialObj = new GameObject[3];
    private float _tutoTime = 0.0f;

    public float KeyTime = 10.0f;
    public float SkillTime = 20.0f;
    public float MoreTime = 10.0f;

    private TutorialType _tutorialType;

    private void Awake()
    {
        _tutorialType = TutorialType.Key;
        _monsterPool = FindObjectOfType<MonsterObjectPool>();
        _monsterPhase = FindObjectOfType<WitchMonsterPhaseManager>();
        TutorialObj[(int)TutorialType.Key].SetActive(true);
    }

    void Start()
    {

    }

    void Update()
    {
        Update_Tutorial();

        if (Input.GetKeyDown(KeyCode.F5))
        {
            foreach (GameObject obj in TutorialObj)
                obj.SetActive(false);

            MonsterWaveManager.I.AddStage();
            MonsterWaveManager.I.StartWave();
            _tutorialType = TutorialType.End;
        }
    }

    private void Update_Tutorial()
    {
        switch (_tutorialType)
        {
            case TutorialType.Key: Update_KeyInfo(); break;
            case TutorialType.Skill: Update_SkillInfo(); break;
            case TutorialType.More: Update_MoreInfo(); break;
        }
    }

    private void Update_KeyInfo()
    {
        _tutoTime += Time.deltaTime;

        if (_tutoTime >= KeyTime)
        {
            _tutoTime = 0.0f;
            TutorialObj[(int)_tutorialType].SetActive(false);
            _tutorialType = TutorialType.Skill;
            TutorialObj[(int)_tutorialType].SetActive(true);
            return;
        }
    }
    private void Update_SkillInfo()
    {
        _tutoTime += Time.deltaTime;

        if (_tutoTime >= SkillTime)
        {
            _tutoTime = 0.0f;
            TutorialObj[(int)_tutorialType].SetActive(false);
            _tutorialType = TutorialType.More;
            TutorialObj[(int)_tutorialType].SetActive(true);
            return;
        }
    }
    private void Update_MoreInfo()
    {
        _tutoTime += Time.deltaTime;

        if (_tutoTime >= MoreTime)
        {
            _tutoTime = 0.0f;
            TutorialObj[(int)_tutorialType].SetActive(false);
            _tutorialType = TutorialType.End;
            MonsterWaveManager.I.AddStage();
            MonsterWaveManager.I.StartWave();
            return;
        }
    }
}
