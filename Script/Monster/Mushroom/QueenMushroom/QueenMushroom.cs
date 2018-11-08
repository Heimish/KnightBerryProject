using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum QueenMushroomState
{
    Idle = 0,
    Chase,
    Attack,
    Attack2,
    Return,
    Healing,
    PP,
    isHit
}

[RequireComponent(typeof(MonsterStat))]
public class QueenMushroom : MonsterBase
{
    public Material _Mat; // 기본 매터리얼
    public Material _HitMat; // 피격 매터리얼
    public GameObject _QueenMat;
    public GameObject HealEffect;

    public QueenMushroomState startState;
    public QueenMushroomState currentState;
    bool _isInit = false;
    private Dictionary<QueenMushroomState, QueenMushroomStateBase> _states = new Dictionary<QueenMushroomState, QueenMushroomStateBase>();

    // 플레이어 위치
    private Transform _player;
    public Transform Player { get { return _player; } }

    // 몬스터 지정된 Home 값
    private Vector3 _home;
    public Vector3 Home { get { return _home; } }

    // 몬스터 지정 위치 좌표 x + y + z를 합한 값
    private Vector3 _homePosition;
    public Vector3 HomePosition { get { return _homePosition; } }

    // 투사체 방향에 사용할 변수
    private Transform _from;
    public Transform From { get { return _from; } }

    float _curTime;
    public float CurTime { set { _curTime = value; } get { return _curTime; } }

    float _maxTime;
    public float MaxTime { set { _maxTime = value; } get { return _maxTime; } }

    // 공격력
    float _attackDamage;
    public float AttackDamage { set { _attackDamage = value; } get { return _attackDamage; } }

    // 공격 딜레이 속도
    float _attackDelay;
    public float AttackDelay { set { _attackDelay = value; } get { return _attackDelay; } }

    // 공격 딜레이 시간
    float _attackTimer;
    public float AttackTimer { set { _attackTimer = value; } get { return _attackTimer; } }

    // 공격 횟수
    int _attackStack;
    public int AttackStack { set { _attackStack = value; } get { return _attackStack; } }

    // 힐 딜레이 시간
    float _hearTimer;
    public float HealTimer { set { _hearTimer = value; } get { return _hearTimer; } }

    // 버섯이 전방베기로 끌렸을 때, 일정 위치에서 멈추게 하기 위한 bool
    private bool _ppending;
    public bool PPEnding { set { _ppending = value; } get { return _ppending; } }

    // 공격 애니메이션 싱크로 체크
    private bool _anisynchro;
    public bool AniSynchro { set { _anisynchro = value; } get { return _anisynchro; } }

    // 캐릭터 사망
    private bool _CharacterisDead;
    public bool CharacterisDead { set { _CharacterisDead = value; } get { return _CharacterisDead; } }


    public float rotAnglePerSecond = 360.0f;// 몬스터 초당 회전 속도
    public float HealDelay; // 힐 쿨타임
    public float HealPoint; // 힐량
    public bool isDead; // 죽었는지 체크
    public bool HealTime; // 힐 하는 상황인지
    public bool HealStart; // 힐 시작
    
    // 몬스터 지정 위치 좌표 x + y + z
    public float GoHomePositionX;
    public float GoHomePositionY;
    public float GoHomePositionZ;

    public MonsterStat MStat { get { return _stat; } set { _stat = value; } }

    private int _animParamID;


    public QueenMushroomStateBase GetCurrentState()
    {
        return _states[currentState];
    }

    public void SetState(QueenMushroomState newState)
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
        /*if (currentState == MonsterState.Dead)
        {
            _isDead = true;
        }*/
    }

    public override void InitMonster(Vector3 homePos)
    {
        isDead = false;
        _stat.Hp = _stat.MaxHp;
        GoHomePositionX = homePos.x;
        GoHomePositionY = homePos.y;
        GoHomePositionZ = homePos.z;
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
            Time.deltaTime * rotAnglePerSecond);
    }

    public void TurnToHitDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Player.position - transform.position);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation,
            Time.deltaTime * 1000f);
    }

    public void MoveToDestination()
    {
        _controller.Move(transform.forward * _stat.MoveSpeed * Time.deltaTime);
    }

    public void MonsterAbs(int type, Transform Target, float minDistance, float maxDistance,
        float backDistance, float fSpeed = 20, float fBackSpeed = 20)
    {
        transform.LookAt(Target);
        if (type == 1)
        {
            if (Vector3.Distance(transform.position, Target.position) < maxDistance)
            {
                //transform.position += transform.forward * fSpeed * Time.deltaTime;
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
                //transform.position -= transform.forward * fBackSpeed * Time.deltaTime;
                _controller.Move(transform.forward * -fBackSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, Target.position) > backDistance)
                {
                    type = 0;
                }
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
            SetState(QueenMushroomState.PP);
            return;
        }
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

    public void NowisHit()
    {
        if (_isHit)
        {
            SetState(QueenMushroomState.isHit);
            _isHit = false;
            return;
        }
    }

    private IEnumerator ChangeMat()
    {
        _QueenMat.GetComponent<Renderer>().material = _HitMat;
        yield return new WaitForSeconds(0.2f);
        _QueenMat.GetComponent<Renderer>().material = _Mat;
    }

    public override void OnDead()
    {
        if (isDead)
        {
            gameObject.SetActive(false);
        }
    }

    public void PlayerisDead()
    {
        if (CPlayerManager._instance.isDead)
        {
            SetState(QueenMushroomState.Idle);
            return;
        }
    }

    public void TimeToHeal()
    {
        if(HealTime && _hearTimer > HealDelay)
        {
            if (currentState == QueenMushroomState.Attack || currentState == QueenMushroomState.Attack2)
                return;

            else
            {
                SetState(QueenMushroomState.Healing);
                return;
            }
        }
    }

    public void EffectofHeal(Vector3 From)
    {
        //From.y += 1f;
        HealEffect.SetActive(true);
    }

    public void SetMonsterHeal(float Heal)
    {
        Stat.Hp += Heal;
        Stat.Hp = Mathf.Clamp(Stat.Hp, 0, Stat.MaxHp);
        SoundManager.I.PlaySound(transform.position, PlaySoundId.Goblin_Heal);
    }

    protected override void Awake()
    {
        base.Awake();
        _homePosition = (new Vector3(GoHomePositionX, GoHomePositionY, GoHomePositionZ));
        _stat.ChaseDistance = 20f;
        _stat.AttackDistance = 15f;
        _stat.MoveSpeed = 3.5f;
        _attackDamage = 10f;
        _attackDelay = 4f;
        _attackTimer = 0;
        _attackStack = 0;
        _hearTimer = 0;
        HealDelay = 10f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _from = transform.Find("From");
        _curTime = 0;
        _home = transform.position;
        _animParamID = Animator.StringToHash("CurrentState");
        isDead = false;
        HealTime = false;
        HealStart = false;
        _ppending = true;
        HealEffect.SetActive(false);

        QueenMushroomState[] stateValues = (QueenMushroomState[])Enum.GetValues(typeof(QueenMushroomState));
        foreach (QueenMushroomState s in stateValues)
        {
            Type FSMType = Type.GetType("QueenMushroom" + s.ToString("G"));
            QueenMushroomStateBase state = (QueenMushroomStateBase)GetComponent(FSMType);
            if (state == null)
                state = (QueenMushroomStateBase)gameObject.AddComponent(FSMType);

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
        _attackTimer += Time.deltaTime;
        _hearTimer += Time.deltaTime;

        if (_stat.Hp <= 0)
        {
            _controller.enabled = false;
            isDead = true;
            _anim.SetBool("isDead", true);
        }
    }
}

