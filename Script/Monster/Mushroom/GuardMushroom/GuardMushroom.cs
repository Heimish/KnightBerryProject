using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuardMushroomState
{
    Idle = 0,
    Chase,
    Attack,
    Return,
    Berserker,
    BChase,
    BAttack,
    Sbombing,
    isHit,
    PP
}

[RequireComponent(typeof(MonsterStat))]
public class GuardMushroom : MonsterBase
{
    public Material _Mat; // 기본 매터리얼
    public Material _HitMat; // 피격 매터리얼
    public GameObject _GuardMat; // 몸체 오브젝트
    public GameObject HealEffect; // 힐 이펙트

    public GuardMushroomState startState;
    public GuardMushroomState currentState;
    bool _isInit = false;
    private Dictionary<GuardMushroomState, GuardMushroomStateBase> _states = new Dictionary<GuardMushroomState, GuardMushroomStateBase>();

    // 플레이어 위치
    private Transform _player;
    public Transform Player { get { return _player; } }

    // 몬스터 지정 위치 좌표 x + y + z를 합한 값
    private Vector3 _homePosition;
    public Vector3 HomePosition { get { return _homePosition; } }

    // 공격력
    private float _attackDamage;
    public float AttackDamage { set { _attackDamage = value; } get { return _attackDamage; } }

    // 공격 딜레이 속도
    private float _attackDelay;
    public float AttackDelay { set { _attackDelay = value; } get { return _attackDelay; } }

    // 공격 딜레이 속도 랜덤값 담을 변수
    private float _raDelay;
    public float RADelay { set { _raDelay = value; } get { return _raDelay; } }

    // 공격 딜레이 시간
    private float _attackTimer;
    public float AttackTimer { set { _attackTimer = value; } get { return _attackTimer; } }

    // 자폭 대기 시간
    private float _SbombTimer;
    public float SbombTimer { set { _SbombTimer = value; } get { return _SbombTimer; } }

    // 내적 계산에 사용할 앵글
    private float _angle;
    public float Angle { set { _angle = value; } get { return _angle; } }

    // 자폭 데미지
    private float _sbombdamage;
    public float SbombDamage { set { _sbombdamage = value; } get { return _sbombdamage; } }

    // 버서커 공격력
    private float _berserkerattackDamage;
    public float BerserkerAttackDamage { set { _berserkerattackDamage = value; } get { return _berserkerattackDamage; } }

    // 버서커 공격 딜레이 속도
    private float _berserkerattackDelay;
    public float BerserkerAttackDelay { set { _berserkerattackDelay = value; } get { return _berserkerattackDelay; } }

    // 버서커 이동 속도
    private float _berserkermovespeed;
    public float BerserkerMoveSpeed { set { _berserkermovespeed = value; } get { return _berserkermovespeed; } }

    // 공격 상태 인 상황에서의 회전 속도
    [SerializeField]
    private float _attackrotangle;
    public float AttackRotAngle { set { _attackrotangle = value; } get { return _attackrotangle; } }

    // 피격 상황에서의 회전 속도
    [SerializeField]
    private float _hitrotangle;
    public float HitRotAngle { set { _hitrotangle = value; } get { return _hitrotangle; } }

    // 버서커 모드 전환 대기 시간
    private float _berserkertime;
    public float BerserkerTime { set { _berserkertime = value; } get { return _berserkertime; } }

    // 버서커 모드 전환 대기 입력 시간
    [SerializeField]
    private float _berserkerctime;
    public float BerserkerCTime { set { _berserkerctime = value; } get { return _berserkerctime; } }

    // 버서커 모드 전환 대기 시간
    [SerializeField]
    private bool _gotoberserker;
    public bool GotoBerserker { set { _gotoberserker = value; } get { return _gotoberserker; } }

    // 버서커 모드가 끝났는지
    private bool _ifendberserker;
    public bool ifEndBerserker { set { _ifendberserker = value; } get { return _ifendberserker; } }

    // 버섯이 전방베기로 끌렸을 때, 일정 위치에서 멈추게 하기 위한 bool
    private bool _ppending;
    public bool PPEnding { set { _ppending = value; } get { return _ppending; } }

    // 플레이어 캐릭터가 전방에 있는지 후방에 있는지(처리 = 내적)
    private bool _playerisfront;
    public bool PlayerisFront { set { _playerisfront = value; } get { return _playerisfront; } }

    // 캐릭터 사망
    private bool _CharacterisDead;
    public bool CharacterisDead { set { _CharacterisDead = value; } get { return _CharacterisDead; } }

    // Y값 고정
    private Vector3 _gravity;
    public Vector3 Gravity { set { _gravity = value; } get { return _gravity; } }

    // Y값 고정 해제
    private bool _exitgravity;
    public bool ExitGravity { set { _exitgravity = value; } get { return _exitgravity; } }

    // AttackTimer 시작
    private bool _attackTstart;
    public bool AttackTStart { set { _attackTstart = value; } get { return _attackTstart; } }

    // 힐 이팩트 꺼야하는 상황
    private bool _exithealEffect;
    public bool ExitHealEffect { set { _exithealEffect = value; } get { return _exithealEffect; } }

    public float rotAnglePerSecond;// 몬스터 초당 회전 속도
    public bool isDead; // 죽었는지 체크
    public bool QueenisAllDead; // 공주 버섯이 전부 죽었는지 체크해서 버서커 모드로 보내기 위한 bool
    public bool SBombing; // 공주 버섯이 전부 죽었는지 체크해서 자폭 모드로 보내기 위한 bool
    public bool imsi = true; // ㅎㅎ임시

    // 몬스터 지정 위치 좌표 x + y + z
    public float GoHomePositionX;
    public float GoHomePositionY;
    public float GoHomePositionZ;

    public MonsterStat MStat { get { return _stat; } set { _stat = value; } }

    private int _animParamID;

    public GuardMushroomStateBase GetCurrentState()
    {
        return _states[currentState];
    }

    public void SetState(GuardMushroomState newState)
    {
        if (_isInit)
        {
            _states[currentState].enabled = false;
            _states[currentState].EndState();
        }
        currentState = newState;
        _states[currentState].BeginState();
        _states[currentState].enabled = true;
        _anim.SetInteger(_animParamID, (int)currentState);
    }

    public override void InitMonster(Vector3 homePos)
    {
        isDead = false;
        _stat.Hp = _stat.MaxHp;
        GoHomePositionX = homePos.x;
        GoHomePositionY = homePos.y;
        GoHomePositionZ = homePos.z;
    }

    public void MonsterAbs(int type, Transform Target, float minDistance, float maxDistance,
        float backDistance, float fSpeed = 20, float fBackSpeed = 20)
    {
        transform.LookAt(Target);
        if (type == 1)
        {
            if (Vector3.Distance(transform.position, Target.position) <= minDistance)
            {
                type = 0;
            }

            else if (Vector3.Distance(transform.position, Target.position) < maxDistance)
            {
                _controller.Move(transform.forward * fSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, Target.position) <= minDistance)
                {
                    type = 0;
                }
            }

            else
            {
                type = 0;
            }
        }
        else if (type == 2)
        {
            if (Vector3.Distance(transform.position, Target.position) < backDistance)
            {
                _controller.Move(transform.forward * -fBackSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, Target.position) > backDistance)
                {
                    type = 0;
                }
            }

            else
            {
                type = 0;
            }
        }

        if (type == 0)
        {
            _ppending = false;
        }
    }

    public void ModeChange()
    {
        if (CPlayerManager._instance.isPull && _ppending)
        {
            MonsterAbs(1, Player, 3.5f, 20f, 15f);
        }

        else if (CPlayerManager._instance.isPush && _ppending)
        {
            MonsterAbs(2, Player, 3.5f, 20f, 15f);
        }
    }

    public void GoToPullPush()
    {
        if (CPlayerManager._instance.isPull || CPlayerManager._instance.isPush)
        {
            SetState(GuardMushroomState.PP);
            return;
        }
    }

    public float GetDistanceFromPlayer() // Player 캐릭터와 거리를 되돌려줄 함수
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        return distance;
    }

    public void TurnToDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Player.position - transform.position);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation,
            Time.deltaTime * _attackrotangle);
    }

    public void TurnToHitDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Player.position - transform.position);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation,
            Time.deltaTime * _hitrotangle);
    }
   
    public void MoveToDestination()
    {
        _controller.Move(transform.forward * _stat.MoveSpeed * Time.deltaTime);
    }

    // 움직이는 함수 (속도 입력이 포함 되어있음)
    public void GoToDestination(Vector3 target, float moveSpeed, float turnSpeed)
    {
        Transform t = _controller.transform;
        Vector3 Forward = target - t.position;
        Forward.y = 0.0f;
        if (Forward != Vector3.zero)
        {
            t.rotation = Quaternion.RotateTowards(
                t.rotation,
                Quaternion.LookRotation(Forward),
                turnSpeed * Time.deltaTime);
        }

        Vector3 nextPos = Vector3.MoveTowards(
            t.position,
            target,
            moveSpeed * Time.deltaTime);

        Vector3 deltaMove = nextPos - t.position;
        deltaMove.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(deltaMove);
    }

    public override void OnDamage(float damage)
    {
        if (_CharacterisDead)
            return;

        base.OnDamage(damage);
        Stat.Hp = Mathf.Clamp(Stat.Hp, 0, Stat.MaxHp);
        StartCoroutine(ChangeMat());
    }

    private IEnumerator ChangeMat()
    {
        _GuardMat.GetComponent<Renderer>().material = _HitMat;
        yield return new WaitForSeconds(0.2f);
        _GuardMat.GetComponent<Renderer>().material = _Mat;
    }

    public void SetMonsterHeal(float Heal)
    {
        Stat.Hp += Heal;
        Stat.Hp = Mathf.Clamp(Stat.Hp, 0, Stat.MaxHp);
        HealingEffect();
        SoundManager.I.PlaySound(transform.position, PlaySoundId.Goblin_Heal);
    }

    public void HealingEffect()
    {
        HealEffect.SetActive(false);
        HealEffect.SetActive(true);
    }

    public override void OnDead()
    {
        if (isDead)
        {
            gameObject.SetActive(false);
        }
    }

    public void NowisHit()
    {
        if (_isHit)
        {
            SetState(GuardMushroomState.isHit);
            _isHit = false;
            return;
        }
    }

    public void GetBerserkerMode()
    {
        if (!_ifendberserker)
        {
            if (_berserkertime > _berserkerctime)
            {
                SetState(GuardMushroomState.Berserker);
                return;
            }
        }
    }

    public void BerserkerTimeStart()
    {
        if(_gotoberserker)
        {
            _berserkertime += Time.deltaTime;
        }
    }

    public void PlayerisDead()
    {
        if (CPlayerManager._instance.isDead)
        {
            SetState(GuardMushroomState.Idle);
            return;
        }
    }

    public void CharicterGravity(Vector3 From)
    {
        if (!_exitgravity)
        {
            From.y = Mathf.Clamp(_gravity.y, 0, 0.5f);
            transform.position = From;
        }
    }

    public void AttackTimerStart()
    {
        if(_attackTstart)
            AttackTimer += Time.deltaTime;

    }

    public void FrontBackCheck()
    {
        float Dot = Vector3.Dot(transform.forward, (_player.position - transform.position).normalized);

        if (Dot >= Mathf.Cos(Mathf.Deg2Rad * _angle * 0.5f))
        {
            _playerisfront = true;
        }

        else
        {
            _playerisfront = false;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _homePosition = (new Vector3(GoHomePositionX, GoHomePositionY, GoHomePositionZ));
        Stat.ChaseDistance = 30f;
        Stat.AttackDistance = 2.5f;
        Stat.MoveSpeed = 2f;
        _attackDamage = 10f;
        _attackDelay = 2f;
        _raDelay = 0;
        _attackTimer = 0;
        _sbombdamage = 50f;
        _SbombTimer = 0;
        _berserkermovespeed = 4f;
        _berserkerattackDamage = 20f;
        _berserkerattackDelay = 2f;
        _angle = 180f;
        _attackrotangle = 200f;
        _hitrotangle = 1000f;
        rotAnglePerSecond = 360f; 
        _berserkertime = 0;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _animParamID = Animator.StringToHash("CurrentState");
        isDead = false;
        QueenisAllDead = false;
        SBombing = false;
        _ppending = true;
        _gravity = transform.position;
        _exithealEffect = true;

        GuardMushroomState[] stateValues = (GuardMushroomState[])Enum.GetValues(typeof(GuardMushroomState));
        foreach (GuardMushroomState s in stateValues)
        {
            Type FSMType = Type.GetType("GuardMushroom" + s.ToString("G"));
            GuardMushroomStateBase state = (GuardMushroomStateBase)GetComponent(FSMType);
            if (state == null)
                state = (GuardMushroomStateBase)gameObject.AddComponent(FSMType);

            state.enabled = false;
            _states.Add(s, state);
        }

        SetState(startState);
        _isInit = true;
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            SetState(startState);
            _isInit = true;
        }
    }

    protected override void Update()
    {
        base.Update();
        //CharicterGravity(transform.position);
        FrontBackCheck();
        BerserkerTimeStart();
        AttackTimerStart();

        if (currentState == GuardMushroomState.Sbombing)
        {
            SbombTimer += Time.deltaTime;
        }

        if (Stat.Hp <= 0)
        {
            _controller.enabled = false;
            isDead = true;
            _anim.SetBool("isDead", true);
        }
    }
}
