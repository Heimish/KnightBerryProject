using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAni_State_Shild
{
    IdleRun = 0,
    Defense_Mode,
    Defense_ModeIdle,
    CountAttack,
    Attack1,
    Attack2,
    Attack3,
    Dash,
    Defense_ModeBack,
    SweatL,
    SweatR,
    ShildRun,
    SweatCount,
    Interaction,
    Attack4,
    None,
    Stun,
    Damage,
    Die,
}
public enum PlayerAni_State_Scythe
{
    IdleRun = 0,
    Attack1,
    Attack2,
    Attack3,
    Skill2,
    Skill1,
    Damage,
    Die,
}


public class CPlayerAni_Contorl : CPlayerBase
{
    private CPlayerAniEvent _CPlayerAniEvent;
    private CPlayerSwap _CPlayerSwap;
    // 애니 초기화
    public PlayerAni_State_Shild _PlayerAni_State_Shild = PlayerAni_State_Shild.IdleRun;
    public PlayerAni_State_Scythe _PlayerAni_State_Scythe = PlayerAni_State_Scythe.IdleRun;

    private Animator _PlayerAniFile;
    public Animator PlayerAniFile { get { return _PlayerAniFile; } set { value = _PlayerAniFile; } }

    public bool m_bDefenseIdle;
    public bool m_bDefenseBack;
    private CPlayerAttackEffect _CPlayerAttackEffect;

    public bool m_bAttack1;
    private float m_fAttack1;

    private float fScytheCameraTime; // 전방베기 카메라연출

    private float fSweatTime;
    private bool isSweat;
    public bool _isSweat { get { return isSweat; } set { value = isSweat; } }

    public bool isSweatCount;
    private bool _isSweatCount { get { return isSweatCount; } set { value = isSweatCount; } }

    private bool isSweatCountTime;
    private bool isSweatChackSet;

    private float _downwardskillcool;
    public float DownWardSkillCool { get { return _downwardskillcool; } set { value = _downwardskillcool; } }

    private float _scytheSkillCool;
    public float ScytheSkillCool { get { return _scytheSkillCool; } set { _scytheSkillCool = value; } }
    public bool ScytheSkillOn
    {
        get
        {
            return _scytheSkillCool <= 0.0f;
        }
    }

    private float _scytheShiftCool;
    public float ScytheShiftCool { get { return _scytheShiftCool; }  set { _scytheShiftCool = value; } }
    public bool ScytheShiftOn
    {
        get
        {
            return _scytheShiftCool <= 0.0f;
        }
    }

    private float _rushskillcool;
    public float RushSkillcool { get { return _rushskillcool; } set { value = _rushskillcool; } }

    private float _shieldSkillCool;
    public float SheildSkillCool { get { return _shieldSkillCool; } set { _shieldSkillCool = value; } }
    public bool ShieldSkillOn
    {
        get
        {
            return _shieldSkillCool <= 0.0f;
        }
    }

    private float _shieldShiftCool;
    public float SheildShiftCool { get { return _shieldShiftCool; } set { _shieldShiftCool = value; } }
    public bool ShieldShiftOn
    {
        get
        {
            return _shieldShiftCool <= 0.0f;
        }
    }

    void Start()
    {
        _CPlayerSwap = GetComponent<CPlayerSwap>();
        _CPlayerAniEvent = GetComponent<CPlayerAniEvent>();
        _CPlayerAttackEffect = GetComponent<CPlayerAttackEffect>();

        // 방패로 가는 모션 체크
        m_bDefenseIdle = false;
        // 현재 애니메이터 값 가져오기
        _PlayerAniFile = GetComponent<Animator>();
    }

    void Update()
    {
        if (_PlayerManager.m_bAnimator)
        {
            if (_PlayerManager.m_bSwap == false)
            {
                if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                {
                    ShieldAniGetKey();
                    ShieldAni();
                    SweatCountStart();
                    SweatCount();
                }
                else
                {
                    ScytheAniGetKey();
                    ScytheAni();
                }
            }
        }

        ScytheSkillCoolTime();
        ShieldSkillCoolTime();
        ScytheShiftCoolTime();
        ShieldShiftCoolTime();
        SkillcoolChange();
    }
    void ShieldAniGetKey()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && _PlayerManager.m_PlayerStm > InspectorManager._InspectorManager.fSweatStm)
            //{
            //    fSweatTime = 0;
            //    _PlayerAni_State_Shild = PlayerAni_State_Shild.SweatL;
            //    _PlayerManager.m_PlayerStm -= InspectorManager._InspectorManager.fSweatStm;
            //}
            //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && _PlayerManager.m_PlayerStm > InspectorManager._InspectorManager.fSweatStm)
            //{
            //    fSweatTime = 0;
            //    _PlayerAni_State_Shild = PlayerAni_State_Shild.SweatR;
            //    _PlayerManager.m_PlayerStm -= InspectorManager._InspectorManager.fSweatStm;
            //}

        }
        else if (Input.GetMouseButtonDown(0))
        {
            // 현재 콤보가 0일경우
            if (_PlayerManager.m_nAttackCombo == 0)
            {
                // 1타로넘김
                CCameraFind._instance.SetZoom(InspectorManager._InspectorManager.TankerAttackZoom[_PlayerManager.m_nAttackCombo]);
                _PlayerAni_State_Shild = PlayerAni_State_Shild.Attack1;
            }
            // 공격중일경우
            if (_PlayerManager.m_bAttack)
            {
                // 1타일때
                if (_PlayerManager.m_nAttackCombo == 1)
                {
                    //2타로넘김
                    CCameraFind._instance.SetZoom(InspectorManager._InspectorManager.TankerAttackZoom[_PlayerManager.m_nAttackCombo]);
                    _PlayerAni_State_Shild = PlayerAni_State_Shild.Attack2;
                }// 2타일때
                else if (_PlayerManager.m_nAttackCombo == 2)
                {
                    //3타로넘김
                    CCameraFind._instance.SetZoom(InspectorManager._InspectorManager.TankerAttackZoom[_PlayerManager.m_nAttackCombo]);
                    _PlayerAni_State_Shild = PlayerAni_State_Shild.Attack3;
                }// 3타일때
                else if (_PlayerManager.m_nAttackCombo == 3)
                {
                    //4타로넘김
                    CCameraFind._instance.SetZoom(InspectorManager._InspectorManager.TankerAttackZoom[_PlayerManager.m_nAttackCombo]);
                    _PlayerAni_State_Shild = PlayerAni_State_Shild.Attack4;
                }
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            _PlayerManager._PlayerMove.SpeedReset();
            if (_PlayerManager.m_nAttackCombo != 0 && _PlayerManager.m_bAttack)
            {
                _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_Mode;
            }
            else
            {
                if (m_bDefenseIdle)
                {
                    _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_ModeIdle;
                }
                else
                {
                    _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_Mode;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !_PlayerManager.m_bMove)
        {
            _CPlayerAniEvent.MoveTypes(2);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!ShieldShiftOn)
                return;

            _PlayerAni_State_Shild = PlayerAni_State_Shild.Dash;
        }
        else
        {
            m_bDefenseIdle = false;
        }

        if (m_bDefenseBack)
        {
            _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_ModeBack;
        }
        if (Input.GetMouseButton(1) && _PlayerManager._isCountAttack)
        {
            _PlayerAni_State_Shild = PlayerAni_State_Shild.CountAttack;
            _PlayerManager._CPlayerCountAttack.isCount = true;
            _PlayerManager._isCountAttack = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!ShieldSkillOn)
                return;

            _PlayerAni_State_Shild = PlayerAni_State_Shild.ShildRun;
        }
    }
    void ShieldAni()
    {
        switch (_PlayerAni_State_Shild)
        {
            case PlayerAni_State_Shild.None:
                {

                }
                break;
            case PlayerAni_State_Shild.IdleRun:
                {
                    Animation_Change(0);
                    MoveMent();
                }
                break;
            case PlayerAni_State_Shild.Defense_Mode:
                {
                    Animation_Change(1);
                }
                break;
            case PlayerAni_State_Shild.Defense_ModeIdle:
                {
                    Animation_Change(2);
                }
                break;
            case PlayerAni_State_Shild.CountAttack:
                {
                    Animation_Change(3);
                }
                break;
            case PlayerAni_State_Shild.Attack1:
                {
                    Animation_Change(4);
                }
                break;
            case PlayerAni_State_Shild.Attack2:
                {
                    Animation_Change(5);
                }
                break;
            case PlayerAni_State_Shild.Attack3:
                {
                    Animation_Change(6);
                }
                break;
            case PlayerAni_State_Shild.Dash:
                {
                    Animation_Change(7);
                }
                break;
            case PlayerAni_State_Shild.Defense_ModeBack:
                {
                    //CPlayerAttackEffect._instance.Effect9();
                    //EffectManager.I.EventOnEffect(6);
                    Animation_Change(8);
                }
                break;
            case PlayerAni_State_Shild.SweatL:
                {
                    Animation_Change(9);
                }
                break;
            case PlayerAni_State_Shild.SweatR:
                {
                    Animation_Change(10);
                }
                break;
            case PlayerAni_State_Shild.ShildRun:
                {
                    Animation_Change(11);
                }
                break;
            case PlayerAni_State_Shild.SweatCount:
                {
                    Animation_Change(12);
                }
                break;
            case PlayerAni_State_Shild.Interaction:
                {
                    Animation_Change(13);
                }
                break;
            case PlayerAni_State_Shild.Attack4:
                {
                    Animation_Change(14);
                }
                break;
            case PlayerAni_State_Shild.Stun:
                {
                    Animation_Change(16);
                }
                break;
            //case PlayerAni_State_Shild.Damage:
            //    {
            //        Animation_Change(17);
            //    }
                //break;
            case PlayerAni_State_Shild.Die:
                {
                    Animation_Change(18);
                }
                break;
        }
    }
    void ScytheAniGetKey()
    {
        if (_PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill1)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!ScytheSkillOn)
                return;

            fScytheCameraTime = 0;
            _PlayerAni_State_Scythe = PlayerAni_State_Scythe.Skill2;
            
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 현재 콤보가 0일경우
                if (_PlayerManager.m_nAttackCombo == 0)
                {
                    // 1타로넘김
                    CCameraFind._instance.SetZoom(InspectorManager._InspectorManager.DealerAttackZoom[_PlayerManager.m_nAttackCombo]);
                    _PlayerAni_State_Scythe = PlayerAni_State_Scythe.Attack1;
                }
                // 공격중일경우
                if (_PlayerManager.m_bAttack)
                {
                    // 1타일때
                    if (_PlayerManager.m_nAttackCombo == 1)
                    {
                        //2타로넘김                    
                        CCameraFind._instance.SetZoom(InspectorManager._InspectorManager.DealerAttackZoom[_PlayerManager.m_nAttackCombo]);
                        _PlayerAni_State_Scythe = PlayerAni_State_Scythe.Attack2;
                    }// 2타일때
                    else if (_PlayerManager.m_nAttackCombo == 2)
                    {
                        //3타로넘김
                        CCameraFind._instance.SetZoom(InspectorManager._InspectorManager.DealerAttackZoom[_PlayerManager.m_nAttackCombo]);
                        _PlayerAni_State_Scythe = PlayerAni_State_Scythe.Attack3;
                    }
                }
            }
        }
    }
    void ScytheAni()
    {
        switch (_PlayerAni_State_Scythe)
        {
            case PlayerAni_State_Scythe.IdleRun:
                {
                    Animation_Change(0);
                    MoveMent();
                }
                break;
            case PlayerAni_State_Scythe.Attack1:
                {
                    Animation_Change(1);
                }
                break;
            case PlayerAni_State_Scythe.Attack2:
                {
                    Animation_Change(2);
                }
                break;
            case PlayerAni_State_Scythe.Attack3:
                {
                    Animation_Change(3);
                }
                break;
            case PlayerAni_State_Scythe.Skill2:
                {
                    //fScytheCameraTime += Time.deltaTime;
                    //if(fScytheCameraTime < 1.0f)
                    //    _PlayerManager.PlayerHitCamera2(2.5f);
                    //else
                    //    _PlayerManager.PlayerHitCamera2(5.0f);

                    Animation_Change(4);
                }
                break;

            case PlayerAni_State_Scythe.Skill1:
                {
                    Animation_Change(5);
                }
                break;
            //case PlayerAni_State_Scythe.Damage:
            //    {
            //        Animation_Change(6);
            //    }
            //    break;
            case PlayerAni_State_Scythe.Die:
                {
                    Animation_Change(7);
                }
                break;
        }
    }
    public void Animation_Change(int animation_number)
    {
        _PlayerAniFile = GetComponent<Animator>();
        _PlayerAniFile.SetInteger("motion", animation_number);
    }
    public void DefenseIdle()
    {
        m_bDefenseIdle = true;
        _PlayerAni_State_Shild = PlayerAni_State_Shild.Defense_ModeIdle;
    }
    void MoveMent()
    {
        _PlayerManager.m_nAttackCombo = 0;
        if (_PlayerManager.m_nAttackCombo == 0 && _PlayerManager.m_bMove)
        {
            float horizontal = _PlayerManager._PlayerMove.fHorizontal;
            float vertical = _PlayerManager._PlayerMove.fVertical;
            _PlayerAniFile.SetFloat("MoveSpeed", horizontal * horizontal + vertical * vertical, 0.1f, Time.deltaTime);
        }
    }
    public void SweatStart()
    {
        _PlayerManager._isPlayerHorn = false;
        _PlayerManager._PlayerShild._isShildCounter = true;
        isSweat = true;
    }
    void SweatCountStart()
    {
        if (!isSweatCount)
            return;

        if (_PlayerManager._isPlayerHorn)
        {
            if (!isSweatChackSet)
            {
                //CPlayerAttackEffect._instance.Effect9(); 이펙트
                StartCoroutine("TimeSweatCountTime");
                isSweatCountTime = true;
                isSweatChackSet = true;
            }
        }
    }
    void SweatCount()
    {
        if (!isSweatCountTime)
            return;

        if (Input.GetMouseButton(1))
        {
            _PlayerAni_State_Shild = PlayerAni_State_Shild.SweatCount;
        }
    }
    IEnumerator TimeSweatCountTime()
    {
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fPlayerSweatCountTime);
        isSweatCountTime = false;
        _PlayerManager._isPlayerHorn = false;
        isSweatChackSet = false;
    }
    public void AniStiff()
    {
        StartCoroutine("Stiff");
    }
    IEnumerator Stiff()
    {
        _PlayerAniFile.speed = 0;
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fPlayerAttackStiff);
        _PlayerAniFile.speed = 1;
    }
    public void isSweatEvent()
    {
        isSweat = false;
    }
    public void isSweatCountEvent()
    {
        isSweatCount = false;
    }
    public void InteractionOn()
    {
        _PlayerAni_State_Shild = PlayerAni_State_Shild.Interaction;
    }

    private void DashAttackEffect()
    {
        EffectManager.I.OnEffect(EffectType.Tanker_DashAttack, transform, 0.6f);
    }

    private void ScytheSkillCoolTime()
    {
        if (ScytheSkillOn)
            return;

        _scytheSkillCool -= Time.deltaTime;
    }

    private void ScytheShiftCoolTime()
    {
        if (ScytheShiftOn)
            return;

        _scytheShiftCool -= Time.deltaTime;
    }

    private void ShieldSkillCoolTime()
    {
        if (ShieldSkillOn)
            return;

        _shieldSkillCool -= Time.deltaTime;
    }

    private void ShieldShiftCoolTime()
    {
        if (ShieldShiftOn)
            return;

        _shieldShiftCool -= Time.deltaTime;
    }

    private void SkillcoolChange()
    {
        _downwardskillcool = _scytheSkillCool;
        _rushskillcool = _shieldSkillCool;
    }

    private void PlayRushSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Tanker_Rush);
    }

    private void PlayDefenseSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Tanker_Defense);
    }

    private void OnAttackEffect(int id)
    {
        switch (id)
        {
            case 0:
                EffectManager.I.OnEffect(EffectType.Hero_Scythe_Attack1, transform, 5.0f, 1);
                break;
            case 1:
                EffectManager.I.OnEffect(EffectType.Hero_Scythe_Attack2, transform, 5.0f, 1);
                break;
            case 2:
                EffectManager.I.OnEffect(EffectType.Hero_Scythe_Attack3, transform, 5.0f, 1);
                break;
        }
    }

    private void OnChopEffect()
    {
        EffectManager.I.OnEffect(EffectType.Hero_Scythe_JunBang, transform, 5.0f, 1);
    }

    private void OnSwapCutEffect()
    {
        EffectManager.I.OnEffect(EffectType.Hero_SwapAttack_Scythe, transform, 5.0f, 1);
    }

    private void ScytheSkillCoolTimeEvent()
    {
        _scytheSkillCool = InspectorManager._InspectorManager.ScytheSkillCoolTime;
    }

    private void ShieldSkillCoolTimeEvent()
    {
        _shieldSkillCool = InspectorManager._InspectorManager.ShieldSkillCoolTime;
    }

    private void ShieldShiftCoolTimeEvent()
    {
        _shieldShiftCool = InspectorManager._InspectorManager.ShieldShiftCoolTime;
    }
}
