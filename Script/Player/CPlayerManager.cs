using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerManager : MonoBehaviour
{
    public static CPlayerManager _instance = null;

    private CPlayerMove _CPlayerMove = null;
    public CPlayerMove _PlayerMove { get { return _CPlayerMove; } }

    private CPlayerAni_Contorl _CPlayerAni_Contorl = null;
    public CPlayerAni_Contorl _PlayerAni_Contorl { get { return _CPlayerAni_Contorl; } }

    public CPlayerAniEvent _CPlayerAniEvent = null;

    private CPlayerSwap _CPlayerSwap = null;
    public CPlayerSwap _PlayerSwap { get { return _CPlayerSwap; } }

    private CPlayerShild _CPlayerShild = null;
    public CPlayerShild _PlayerShild { get { return _CPlayerShild; } }

    public CPlayerCountAttack _CPlayerCountAttack;

    public SphereCollider SkillSphereCollider;

    // 플레이어 속도
    [SerializeField]
    private float m_fMoveSpeed;
    public float m_MoveSpeed { get { return m_fMoveSpeed; } set { m_fMoveSpeed = value; } }
    // 플레이어 중력

    [SerializeField]
    private float m_fGravity;
    public float m_Gravity { get { return m_fGravity; } set { m_fGravity = value; } }

    // 플레이어 HP
    [SerializeField]
    private float m_fPlayerHp;
    public float m_PlayerHp { get { return m_fPlayerHp; } set { m_fPlayerHp = value; } }

    // 플레이어 MaxHP
    [SerializeField]
    private float m_fPlayerMaxHp;
    public float m_PlayerMaxHp { get { return m_fPlayerMaxHp; } set { m_fPlayerMaxHp = value; } }

    //// 플레이어 딜러형 HP
    //[SerializeField]
    //private float m_fscyPlayerHp;
    //public float m_ScyPlayerHp { get { return m_fscyPlayerHp; } set { m_fscyPlayerHp = value; } }

    //// 플레이어 딜러형 MaxHP
    //[SerializeField]
    //private float m_fscyPlayerMaxHp;
    //public float m_ScyPlayerMaxHp { get { return m_fscyPlayerMaxHp; } set { m_fscyPlayerMaxHp = value; } }

    // 플레이어 스테미나
    [SerializeField]
    private float m_fPlayerStm;
    public float m_PlayerStm { get { return m_fPlayerStm; } set { m_fPlayerStm = value; } }

    // 플레이어 Max스테미나
    [SerializeField]
    private float m_fPlayerMaxStm;
    public float m_PlayerMaxStm { get { return m_fPlayerMaxStm; } set { m_fPlayerMaxStm = value; } }

    // 플레이어 게이지 (반격,방어 등)
    [SerializeField]
    private float m_fPlayerGauge;
    public float m_PlayerGauge { get { return m_fPlayerGauge; } set { m_fPlayerGauge = value; } }

    // 플레이어 반격공격 데미지
    [SerializeField]
    private int m_nPlayerParryDmg;
    public int m_PlayerParryDmg { get { return m_nPlayerParryDmg; } set { m_nPlayerParryDmg = value; } }

    // 플레이어 검방 공격력
    [SerializeField]
    private int[] m_nPlayerShildHitDmg = new int[5];
    public int[] m_PlayerShildHitDmg { get { return m_nPlayerShildHitDmg; } set { m_nPlayerShildHitDmg = value; } }

    // 플레이어 낫모드 공격력
    [SerializeField]
    private int[] m_nPlayerScytheHitDmg = new int[3];
    public int[] m_PlayerScytheHitDmg { get { return m_nPlayerScytheHitDmg; } set { m_nPlayerScytheHitDmg = value; } }

    // 스왑 모션이 일어났는지를 체크하기 위한 bool
    private bool _isPull;
    public bool isPull { get { return _isPull; } set { _isPull = value; } }

    // 스왑 모션이 일어났는지를 체크하기 위한 bool
    private bool _isPush;
    public bool isPush { get { return _isPush; } set { _isPush = value; } }

    public Quaternion vPlayerQuaternion = Quaternion.identity; // 플레이어 로테이션
    public CharacterController _PlayerController; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    public bool m_bMove; // 플레이어가 이동중인지
    public bool m_bAttack; // 플레이어가 공격중인지 체크
    public int m_nAttackCombo; // 플레이어 타수콤보 체크 ( 1타,2타,3타 연계에 사용함)
    public bool m_bAnimator; // 기본 공격이 아닌 스킬을 사용할때 다른동작을 막기위해 사용
    public bool m_bSwap; // 스왑할때 애니메이션 Idle 안들어가게 막기

    public bool m_isRotation; // 현재 플레이어가 회전중인가
    public bool m_isRotationAttack;
    public float fRotationSave; // 플레이어 공격할때 회전각도 저장
    public bool isDead; // 현재 플레이어가 죽었는지

    public float EDITOR_MOVESPEED;
    public float EDITOR_ROTATIONSPEED;
    public float EDITOR_MOVEANGLE;

    private bool isCountAttack;
    public bool _isCountAttack { get { return isCountAttack; } set { isCountAttack = value; } }

    private bool isPlayerHorn; // 플레이어 무적
    public bool _isPlayerHorn { get { return isPlayerHorn; } set { isPlayerHorn = value; } }

    [SerializeField]
    private float _scytheGauge; // 변신 게이지
    public float ScytheGauge { get { return _scytheGauge; } set { _scytheGauge = value; } }
    public bool CanSwap
    {
        get
        {
            return _scytheGauge >= InspectorManager._InspectorManager.SwapMaxGauge;
        }
    }

    void Awake()
    {
        CPlayerManager._instance = this;

        Init();
    }

    void Init()
    {
        _CPlayerMove = GetComponent<CPlayerMove>();
        _CPlayerAni_Contorl = GetComponent<CPlayerAni_Contorl>();
        _PlayerController = GetComponent<CharacterController>();
        _CPlayerSwap = GetComponent<CPlayerSwap>();
        _CPlayerShild = GetComponent<CPlayerShild>();
        _CPlayerAniEvent = GetComponent<CPlayerAniEvent>();
        _CPlayerCountAttack = GetComponent<CPlayerCountAttack>();

        // 플레이어 스탯 설정
        m_fMoveSpeed = 6;
        m_fGravity = 20;
        //m_fPlayerMaxHp = 10000;
        m_fPlayerHp = m_fPlayerMaxHp;
        //m_fscyPlayerMaxHp = m_fPlayerMaxHp / 2;
        //m_fscyPlayerHp = m_fscyPlayerMaxHp;
        m_fPlayerMaxStm = 100;
        m_fPlayerStm = m_fPlayerMaxStm;
        m_fPlayerGauge = 100;
        m_nAttackCombo = 0;
        isDead = false;
        //_scytheGauge = 0.0f;

        m_bMove = true;
        m_bAnimator = true;
        m_isRotationAttack = true;
        isPlayerHorn = false;
    }

    void Update()
    {
        // 수치조절
        for (int i = 0; i < m_nPlayerShildHitDmg.Length; i++)
        {
            m_nPlayerShildHitDmg[i] = InspectorManager._InspectorManager.nDamgeShild[i];
        }
        for (int i = 0; i < m_nPlayerScytheHitDmg.Length; i++)
        {
            m_nPlayerScytheHitDmg[i] = InspectorManager._InspectorManager.nDamgeScythe[i];
        }

        EDITOR_ROTATIONSPEED = InspectorManager._InspectorManager.fRotation;
        EDITOR_MOVESPEED = InspectorManager._InspectorManager.fMoveSpeed;
        EDITOR_MOVEANGLE = InspectorManager._InspectorManager.fMoveAngle;

        m_fPlayerStm = Mathf.Clamp(m_fPlayerStm, 0, 100.0f);

        //if (m_fPlayerHp <= 0)
        //{
        //    gameObject.SetActive(false);
        //}

        if (m_bAttack)
        {
            EDITOR_ROTATIONSPEED = EDITOR_ROTATIONSPEED * 1000;
        }
        else
        {
            if (EDITOR_ROTATIONSPEED <= InspectorManager._InspectorManager.fRotation)
                EDITOR_ROTATIONSPEED = InspectorManager._InspectorManager.fRotation;
            else
                EDITOR_ROTATIONSPEED = (InspectorManager._InspectorManager.fRotation + EDITOR_ROTATIONSPEED) / 4;
        }

        PlayerRotationSave();
        PlayerRotation();
        PlayerHornOn();
        m_fPlayerHp = Mathf.Clamp(m_fPlayerHp, 0, m_fPlayerMaxHp);

        if (m_fPlayerHp <= 0)
        {
            m_fPlayerHp = 0.0f;

            if (_PlayerSwap._PlayerMode == PlayerMode.Shield)
                _PlayerAni_Contorl._PlayerAni_State_Shild = PlayerAni_State_Shild.Die;
            else
                _PlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.Die;

            isDead = true;
        }
    }

    // 플레이어 사망시
    public void PlayerDead()
    {
        //if (isDead == true)
        //{
        //    gameObject.SetActive(false);
        //    return;
        //}
    }
    // 플레이어 로테이션을 부드럽게 이동
    public void PlayerRotation()
    {
        if (isDead)
            return;

        if (_PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_Mode ||
            _PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeBack ||
            _PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle ||
            _PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Die ||
            _PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Die)
            return;

        if (!m_isRotationAttack)
            return;

        if (CCameraFind._instance.m_bCamera)
        {
            Vector3 Forward = _CPlayerMove.m_moveDir;
            vPlayerQuaternion = transform.rotation;

            if (Forward != Vector3.zero)
            {
                vPlayerQuaternion = Quaternion.RotateTowards(
                    vPlayerQuaternion,
                    Quaternion.LookRotation(Forward),
                    EDITOR_ROTATIONSPEED * Time.deltaTime);
                m_isRotation = true;
            }

            transform.rotation = vPlayerQuaternion;

            if (Vector3.Distance(Forward, transform.forward) <= EDITOR_MOVEANGLE)
            {
                m_isRotation = false;
            }
        }
    }

    // 플레이어 데미지 처리 
    public float PlayerHp(float shake = 0.0f, int type = 1, float sizeHp = 0)
    {
        if (isDead)
            return m_fPlayerHp;

        CCameraShake._instance.shake = shake;

        // type = 1  플레이어 / type = 2 방패
        if (type == 1)
        {
            //StartCoroutine(Co_Damage());
            SoundManager.I.PlaySound(transform, PlaySoundId.Hit_Pc);
            //if (!isPlayerHorn) // 플레이어가 무적상태가 아닐때
            //{
                // 플레이어가 검방패 모드일때
                //if (_PlayerSwap._PlayerMode == PlayerMode.Shield)
                //{
                // hp내림
                m_fPlayerHp -= sizeHp;
                //}
                //else // 낫 모드일때
                //{
                //    // hp 내림
                //    m_fscyPlayerHp -= sizeHp;
                //}
            //}

            // 플레이어가 흘리기 중일경우
            //if (_CPlayerAni_Contorl._isSweat)
            //{
            //    // 플레이어가 흘리기도중 반격을 할수있음
            //    //_CPlayerAni_Contorl.isSweatCount = true;
            //    // 플레이어 무적 시작
            //    PlayerHornOn();
            //    // 이펙트 호출
            //    // CPlayerAttackEffect._instance.Effect9(); 이펙트
            //}
        }

        // 방패일때 데미지안들어가~ 사실 반 깍임
        else if (type == 2)
        {
            SoundManager.I.PlaySound(transform, PlaySoundId.Defense_Shield);
            // 방패모드 맞았을때 hit 출력
            _CPlayerShild.m_bShildCollider = true;
            // 체력대신 스테미너 깎음 스태미너 없어 선우야
            //m_fPlayerStm -= sizeHp * InspectorManager._InspectorManager.fShildDamge;
            m_fPlayerHp -= sizeHp / 2;
        }

        //else
        //    m_fscyPlayerHp -= sizeHp;

        if (m_fPlayerHp <= 0)
        {
            m_fPlayerHp = 0.0f;

            if (_PlayerSwap._PlayerMode == PlayerMode.Shield)
                _PlayerAni_Contorl._PlayerAni_State_Shild = PlayerAni_State_Shild.Die;
            else
                _PlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.Die;

            isDead = true;
        }

        return m_fPlayerHp;
    }

    // 카메라 연출 줌,인 연출 함수
    public void PlayerHitCamera(float hitDitance, float shake = 0)
    {
        //CCameraRayObj._instance.MaxCamera(hitDitance);
        CCameraShake._instance.shake = shake;
    }
    public void PlayerHitCamera2(float hitDitance)
    {
        CCameraRayObj._instance.MaxCamera(hitDitance);
    }

    public void PlayerRotationSave()
    {
        if (m_bAttack)
        {
            if (fRotationSave != vPlayerQuaternion.y)
            {
                m_isRotationAttack = false;
            }
        }

        else
            return;
    }

    // 스왑 했을때 hp변경
    //public void SwapHpType(int type)
    //{
    //    // 2 흑화 -> 실드

    //    if (type == 1)
    //    {
    //        m_fPlayerHp = m_fscyPlayerHp * 2;
    //    }// 1 실드 -> 흑화
    //    else
    //    {
    //        m_fscyPlayerHp = m_fPlayerHp / 2;
    //    }
    //}

    // 쉴드 상태에서 n초간 카운터 어택 유지
    public void PlayerSound(int type)
    {
        if (CSoundManager._instance == null)
            return;

        CSoundManager._instance.PlaySoundType(type);
    }

    public void SoundStop()
    {
        CSoundManager._instance._AS_Audio.Stop();
    }

    public void StartShildCounter()
    {
        isCountAttack = true;
        StartCoroutine(CountAttackReturn());
    }

    // 카운트어택을 사용할 수있는 시간
    IEnumerator CountAttackReturn()
    {
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fCountAttackReturnTime);
        isCountAttack = false;
    }

    // 플레이어 무적상태호출
    public void PlayerHornOn()
    {
        if (!_CPlayerAni_Contorl._isSweat)
            return;

        // 흘리기도중
        if (_CPlayerAni_Contorl._isSweat)
        {
            // 무적 시작
            isPlayerHorn = true;
            // 일정시간뒤에 무적해제
            StartCoroutine("StartHorn");
        }
    }

    IEnumerator StartHorn()
    {
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fPlayerHornTime);
        isPlayerHorn = false; // 무적해제
    }

    private void PlayStandardAttackSound()
    {
        if (_PlayerSwap._PlayerMode == PlayerMode.Shield)
            SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Original);
        if (_PlayerSwap._PlayerMode == PlayerMode.Scythe)
            SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Scythe);
    }

    private void PlayFinishAttackSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Finish);
    }

    private void PlayCounterAttackSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Counter);
    }

    private void PlayWideCutSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Skill_ScytheWideCut);
    }

    private void PlayDashSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Tanker_Dash);
    }

    private void PlayQuickCutSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Dealer_QuickCut);
    }

    private void PlayChopSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Dealer_QuickCut);
    }

    private void DashStm()
    {
        m_PlayerStm -= InspectorManager._InspectorManager.fStmDash;
    }

    public int PlayerEffectOn(int type)
    {
        EffectManager.I.EventOnEffect(type);

        return type;
    }

    private void OnSkillCollider()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Scythe);
        SoundManager.I.PlaySound(transform, PlaySoundId.Skill_ScytheWideCut);
        //SkillSphereCollider.enabled = true;
        foreach (MonsterBase mon in
                MonsterManager.I.FindNearMonster(
                    transform.position,
                    InspectorManager._InspectorManager.ScytheSwapSkillDis))
        {
            PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.2f);
            mon.OnDamage(InspectorManager._InspectorManager.ScytheSwapSkillDamage);
            OnHitEffect(mon);
        }
    }

    private void OnChopSkillDamage()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.Attack_Scythe);
        foreach (MonsterBase mon in
                MonsterManager.I.FindNearMonster(
                    transform.position,
                    InspectorManager._InspectorManager.ScytheChopSkillDis))
        {
            PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.2f);
            mon.OnDamage(InspectorManager._InspectorManager.ScytheChopSkillDamage);
            OnHitEffect(mon);
        }
    }

    private void OnAttackNearMonster()
    {
        foreach (MonsterBase mon in
                MonsterManager.I.FindNearMonster(
                    transform.position,
                    InspectorManager._InspectorManager.ScytheAttackDis))
        {
            PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.2f);
            mon.OnDamage(InspectorManager._InspectorManager.nDamgeScythe[m_nAttackCombo]);
            OnHitEffect(mon);
        }
    }

    private void OffSkillCollider()
    {
        SkillSphereCollider.enabled = false;
        _CPlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.IdleRun;
    }

    private void OnIdleRun()
    {
        _CPlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.IdleRun;
    }

    private void OnHitEffect(MonsterBase other)
    {
        SoundManager.I.PlaySound(other.transform, PlaySoundId.Hit_StandardMonster);

        if (other.tag == "Guard")
        {
            if (other.GetComponent<GuardMushroom>().Stat.Hp > 0)
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<GuardMushroomEffect>().GuardMHitEffect();
                CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
            }
        }

        else if (other.tag == "Queen")
        {
            if (other.GetComponent<QueenMushroom>().Stat.Hp > 0)
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<QueenMushroomEffect>().QueenMHitEffect();
                CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
            }
        }

        else if (other.tag == "ShildMushroom")
        {
            if (other.GetComponent<ShildMushroom>().GroggyEnd == false && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
            {
                other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
                CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
            }

            else if (other.GetComponent<ShildMushroom>().GroggyEnd == true && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
            {
                other.GetComponent<ShildMushroomEffect>().DefenEffect();
                CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
            }

            // 2학기 방패엘리트 몬스터 변경사항 : 전방에서도 데미지가 들어가야 함. (임시 수정 1차)
            //if (other.GetComponent<ShildMushroom>().PlayerisFront == false && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
            //{
            //    other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
            //    CPlayerManager._instance.m_ScyPlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
            //    other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo], InspectorManager._InspectorManager.nGroggyScythe[nCombo]);
            //}

            //else if (other.GetComponent<ShildMushroom>().PlayerisFront == true && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
            //{
            //    other.GetComponent<ShildMushroomEffect>().DefenEffect();
            //}
        }
        else
        {
            other.GetComponent<WitchBossEffect>().OnScytheEffect(Random.Range(0, 3));
            CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
        }
    }

    IEnumerator Co_Damage()
    {
        if (_CPlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            if (_PlayerAni_Contorl._PlayerAni_State_Shild != PlayerAni_State_Shild.ShildRun &&
                _PlayerAni_Contorl._PlayerAni_State_Shild != PlayerAni_State_Shild.CountAttack &&
                _PlayerAni_Contorl._PlayerAni_State_Shild != PlayerAni_State_Shild.Stun)
                _PlayerAni_Contorl._PlayerAni_State_Shild = PlayerAni_State_Shild.Damage;
        }
        else
        {
            if (_PlayerAni_Contorl._PlayerAni_State_Scythe != PlayerAni_State_Scythe.Skill1 &&
                _PlayerAni_Contorl._PlayerAni_State_Scythe != PlayerAni_State_Scythe.Skill2)
                _PlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.Damage;
        }
        m_bMove = false;
        yield return new WaitForSeconds(0.01f);
    }

    private void DamageMoveTrue()
    {
        m_bMove = true;
    }

    private void ControllerOff()
    {
    }
}