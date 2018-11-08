using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSwapMode
{
    Tanker = 0,
    Dealer,
}

public enum TankerAnimState
{
    Idle,
    Run,
    Attack,
    Dash,
    Defense_start,
    Defense_Loop,
    Defense_hit,
    CounterAttack,
}

public enum DealerAnimState
{
    Idlen,
    Run,
    Attack,
    Skill,
}

public enum CharacterAnimState
{
    IdleRun = 0,
    Attack,
    Skill,
    Dash,
    Defense_Start,
    Defense_Loop,
    Defemse_Hit,
    CounterAttack,
    Skill1,
    Skill2,
}

public enum CharacterAction
{
    IdleRun,
    Attack,
    Dash,
    Defense
}

[RequireComponent(typeof(PlayerStat))]
public class PlayerBase : MonoBehaviour
{
    public RuntimeAnimatorController TankerAnimator;
    public Avatar TankerAvatar;
    public RuntimeAnimatorController DealerAnimator;
    public Avatar DealerAvatar;

    public PlayerWeaponHit SwordHit;

    private PlayerStat _stat;

    private Animator _anim;
    private PlayerSwapMode _mode;
    private CharacterAnimState _animState;

    private CharacterController _controller;

    private GameObject[] _character;
    private float _horizontal;
    private float _vertical;
    private bool _isMoving;
    private bool _isRotation;
    private bool _isAttacking;
    private bool _nextAttack;
    private bool _isDash;
    private bool _isDefense;

    private Vector3 _destination;
    private Vector3 _moveDir;

    private int _attackIdx;

    private CharacterAction _action;

    // properties
    public float Horizontal { get { return _horizontal; } }
    public float Vertical { get { return _vertical; } }
    public bool NextAttack { get { return _nextAttack; } set { _nextAttack = value; } }
    public int AttackIdx { get { return _attackIdx; } set { _attackIdx = value; } }
    //public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    //public bool IsDash { get { return _isDash; } set { _isDash = value; } }
    //public bool IsDefense { get { return _isDefense; } set { _isDefense = value; } }
    public CharacterAction Action { get { return _action; } set { _action = value; } }

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();

        _character = new GameObject[2];
        _character[(int)PlayerSwapMode.Tanker] = transform.Find("Tanker").gameObject;
        _character[(int)PlayerSwapMode.Dealer] = transform.Find("Dealer").gameObject;
        _character[(int)PlayerSwapMode.Dealer].SetActive(false);

        _action = CharacterAction.IdleRun;
    }

    void Start()
    {
    }

    void Update()
    {
        Update_Movement();
        Update_MoveRotation();
        Update_Attack();
        Update_Dash();
        Update_Swap();
        Update_Defense();
    }

    private void Update_Movement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (_action != CharacterAction.IdleRun)
            return;

        if (_animState == CharacterAnimState.Attack)
            return;

        Vector3 horizontalPos = PlayerCamera.I.transform.right * _horizontal;
        Vector3 verticalPos = PlayerCamera.I.transform.forward * _vertical;

        _destination = transform.position + horizontalPos + verticalPos;
        _destination.y = transform.position.y;

        Vector3 direction = _destination - transform.position;
        _moveDir = direction.normalized;

        if (!Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.S))
        {
            _vertical = 0.0f;
        }

        if (!Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.D))
        {
            _horizontal = 0;
        }

        if (Mathf.Abs(_horizontal) > 0 || Mathf.Abs(_vertical) > 0)
        {
            SetAnimation(CharacterAnimState.IdleRun);
            _anim.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
            _controller.Move(_moveDir * Time.deltaTime * _stat.MoveSpeed);
            _isRotation = true;
        }
        else
        {
            SetAnimation(CharacterAnimState.IdleRun);
            _anim.SetFloat("Speed", 0.0f, 0.1f, Time.deltaTime);
        }
    }

    private void Update_MoveRotation()
    {
        //if (!_isMoving)
        //    return;

        if (!_isRotation)
            return;

        Vector3 Forward = _moveDir;
        if (Forward != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(Forward),
                (_stat.RotationSpeed + _stat.AddRotationSpeed) * Time.deltaTime);
        }

        if (Mathf.Abs(Vector3.Dot(transform.forward, Forward)) <= 0.05f)
        {
            _isRotation = false;
        }
    }

    private void Update_Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_attackIdx == 0 || _nextAttack)
            {
                AttackSystem();
            }
        }
    }

    private void Update_Dash()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    //_isDash = true;
        //    SetAnimation(CharacterAnimState.Dash);
        //}
    }

    private void Update_Swap()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (_mode)
            {
                case PlayerSwapMode.Tanker:
                    SwapCharacterNormal(PlayerSwapMode.Dealer);
                    return;

                case PlayerSwapMode.Dealer:
                    SwapCharacterNormal(PlayerSwapMode.Tanker);
                    return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (_mode)
            {
                case PlayerSwapMode.Tanker:
                    SwapCharacterSkill(PlayerSwapMode.Dealer);
                    return;
                case PlayerSwapMode.Dealer:
                    SwapCharacterNormal(PlayerSwapMode.Tanker);
                    return;
            }
        }
    }

    private void Update_Defense()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_action == CharacterAction.Dash)
                return;

            //_isDefense = true;
            SetAnimation(CharacterAnimState.Defense_Start);
        }
        else
        {
            //if (!_isDefense)
            //    return;

            if (_action != CharacterAction.Defense)
                return;

            //_isDefense = false;
            SetAnimation(CharacterAnimState.IdleRun);
        }
    }

    private void AttackSystem()
    {
        SetAnimation(CharacterAnimState.Attack);
        _anim.SetInteger("AttackIdx", _attackIdx);
        _attackIdx++;
        _nextAttack = false;
        //_isMoving = false;
        _isRotation = true;
        _stat.AddRotationSpeed = 5;

        Vector3 horizontalPos = PlayerCamera.I.transform.right * _horizontal;
        Vector3 verticalPos = PlayerCamera.I.transform.forward * _vertical;
        Vector3 dest = transform.position + horizontalPos + verticalPos;
        dest.y = transform.position.y;

        _moveDir = (dest - transform.position).normalized;
    }

    public void AttackEnd()
    {
        _attackIdx = 0;
        _nextAttack = false;
        //_isAttacking = false;
    }

    public void SetAnimation(CharacterAnimState state)
    {
        if (state == _animState)
            return;

        //if (_animState == CharacterAnimState.Attack)
        //{
        //    EndAttack();
        //}

        _animState = state;
        _anim.SetInteger("State", (int)_animState);
    }

    public void SwapCharacterNormal(PlayerSwapMode mode)
    {
        if (_mode == mode)
            return;

        _character[(int)_mode].SetActive(false);
        _mode = mode;
        _character[(int)_mode].SetActive(true);

        switch (mode)
        {
            case PlayerSwapMode.Tanker:
                _anim.runtimeAnimatorController = TankerAnimator;
                _anim.avatar = TankerAvatar;
                return;
            case PlayerSwapMode.Dealer:
                _anim.runtimeAnimatorController = DealerAnimator;
                _anim.avatar = DealerAvatar;
                return;
        }
    }

    public void SwapCharacterSkill(PlayerSwapMode mode)
    {
        SwapCharacterNormal(mode);

        if (mode == PlayerSwapMode.Dealer)
        {
            SetAnimation(CharacterAnimState.Skill1);
        }
    }

    public void OnAttackHit()
    {
        SwordHit.OnHitCheck();
    }

    public void OffAttackHit()
    {
        SwordHit.OffHitCheck();
    }
}
