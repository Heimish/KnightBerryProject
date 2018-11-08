using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShildMushroomState
{
    Idle = 0,
    Chase,
    Attack,
    Return,
    Groggy
}

[RequireComponent(typeof(MonsterStat))]
public class ShildMushroom : MonsterBase
{
    public Material _Mat; // 기본 매터리얼
    public Material _HitMat; // 피격 매터리얼
    public GameObject _ShildMat;

    public ShildMushroomState startState;
    public ShildMushroomState currentState;

    private Dictionary<ShildMushroomState, ShildMushroomStateBase> _states = new Dictionary<ShildMushroomState, ShildMushroomStateBase>();

    // 플레이어 위치
    private Transform _player;
    public Transform Player { get { return _player; } }

    // 몬스터 지정된 Home 값
    private Vector3 _home;
    public Vector3 Home { get { return _home; } }

    // 공격력
    private float _attackDamage;
    public float AttackDamage { set { _attackDamage = value; } get { return _attackDamage; } }

    // 공격 딜레이 속도
    private float _attackDelay;
    public float AttackDelay { set { _attackDelay = value; } get { return _attackDelay; } }

    // 공격 딜레이 시간
    private float _attackTimer;
    public float AttackTimer { set { _attackTimer = value; } get { return _attackTimer; } }

    // 내적 계산에 사용할 앵글
    private float _angle;
    public float Angle { set { _angle = value; } get { return _angle; } }

    // 그로기 수치
    private float _groggy;
    public float Groggy { set { _groggy = value; } get { return _groggy; } }

    // 그로기가 끝났는지
    private bool _groggyend;
    public bool GroggyEnd { set { _groggyend = value; } get { return _groggyend; } }

    // 그로기 수치 최대 값
    private float _maxgroggy;
    public float MaxGroggy { set { _maxgroggy = value; } get { return _maxgroggy; } }

    // 공격 상태 인 상황에서의 회전 속도
    [SerializeField]
    private float _attackrotangle;
    public float AttackRotAngle { set { _attackrotangle = value; } get { return _attackrotangle; } }

    // 플레이어 캐릭터가 전방에 있는지 후방에 있는지(처리 = 내적)
    private bool _playerisfront;
    public bool PlayerisFront { set { _playerisfront = value; } get { return _playerisfront; } }

    // 캐릭터 사망
    private bool _characterisDead;
    public bool CharacterisDead { set { _characterisDead = value; } get { return _characterisDead; } }
    
    public float rotAnglePerSecond = 200f;// 몬스터 초당 회전 속도
    public float GroggyPoint; // 그로기 수치 설정
    public bool isDead; // 죽었는지 체크

    private bool _isInit = false;
    private int _animParamID;

    public ShildMushroomStateBase GetCurrentState()
    {
        return _states[currentState];
    }

    public void SetState(ShildMushroomState newState)
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
        if (_characterisDead)
            return;

        base.OnDamage(damage);
        Stat.Hp = Mathf.Clamp(Stat.Hp, 0, Stat.MaxHp);
        StartCoroutine(ChangeMat());
    }

    public void AddGroggyValue(float value)
    {
        if (currentState == ShildMushroomState.Groggy)
            return;

        _groggy += value;
        _groggy = Mathf.Clamp(_groggy, 0, _maxgroggy);
    }

    public void GroggyCheck()
    {
        if (_groggy < 100f && !_groggyend)
            return;

        else if(_groggy >= 100f && _groggyend)
        {
            SetState(ShildMushroomState.Groggy);
            return;
        }
    }

    public void GroggySet()
    {
        _groggy -= (1f * Time.deltaTime);
        _groggy = Mathf.Clamp(_groggy, 0, _maxgroggy);
    }

    private IEnumerator ChangeMat()
    {
        _ShildMat.GetComponent<Renderer>().material = _HitMat;
        yield return new WaitForSeconds(0.2f);
        _ShildMat.GetComponent<Renderer>().material = _Mat;
    }

    public override void OnDead()
    {
        gameObject.SetActive(false);
    }

    public void PlayerisDead()
    {
        if (CPlayerManager._instance.isDead)
        {
            SetState(ShildMushroomState.Idle);
            return;
        }
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
        Stat.ChaseDistance = 20f;
        Stat.AttackDistance = 3f;
        Stat.MoveSpeed = 3f;
        _attackDamage = 20f;
        _attackDelay = 1.4f;
        _attackTimer = 0;
        _angle = 180f;
        _groggy = 0;
        _groggyend = true;
        _maxgroggy = 105f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _animParamID = Animator.StringToHash("CurrentState");
        isDead = false;

        ShildMushroomState[] stateValues = (ShildMushroomState[])Enum.GetValues(typeof(ShildMushroomState));
        foreach (ShildMushroomState s in stateValues)
        {
            Type FSMType = Type.GetType("ShildMushroom" + s.ToString("G"));
            ShildMushroomStateBase state = (ShildMushroomStateBase)GetComponent(FSMType);
            if (state == null)
                state = (ShildMushroomStateBase)gameObject.AddComponent(FSMType);

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
        FrontBackCheck();
        GroggySet();
        AttackTimer += Time.deltaTime;

        if (Stat.Hp <= 0)
        {
            _controller.enabled = false;
            isDead = true;
            _anim.SetBool("isDead", true);
        }
    }
}
