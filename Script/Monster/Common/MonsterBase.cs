using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterStat))]
public class MonsterBase : MonoBehaviour
{
    protected CharacterController _controller; // 캐릭터 컨트롤러
    protected Animator _anim; // 애니메이션
    protected MonsterStat _stat; // 스탯 
    protected Vector3 _destination; // 목적지
    protected Vector3 _rotateTarget;
    protected bool _isMoving; // 현재 이동중인지
    protected bool _isRotate; // 현재 회전중인지

    protected Renderer _renderer; // 렌더러
    protected bool _isDead; // 죽었는가
    protected bool _isHit; // 공격을 받은 상태인가

    [SerializeField]
    protected Renderer _modelRenderer;
    [SerializeField]
    protected Renderer _gearRenderer;

    protected bool _isSpawn;
    protected float _noiseValue;

    // properties
    public CharacterController Controller { get { return _controller; } }
    public Animator Anim { get { return _anim; } }
    public MonsterStat Stat { get { return _stat; } }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }
    public bool isHit { get { return _isHit; } set { _isHit = value; } }
    public bool IsSpawn { get { return _isSpawn; } set { _isSpawn = value; } }

    protected virtual void Awake()
    {
        _controller = GetComponentInChildren<CharacterController>();
        _renderer = _controller.gameObject.GetComponent<Renderer>();
        _anim = GetComponentInChildren<Animator>();
        _stat = GetComponent<MonsterStat>();
    }

    void Start()
    {

    }

    protected virtual void Update()
    {
        if (name == "BossMonster")
        {
            MoveUpdate();
            RotateUpdate();
        }
        Update_SpawnNoise();
    }
    
    /// <summary>
    /// 움직임을 처리하는 함수
    /// </summary>
    protected virtual void MoveUpdate()
    {
        if (!_isMoving)
            return;

        //if (m_isRotate)
        //    return;
        _controller.Move((_destination - transform.position).normalized * _stat.MoveSpeed * Time.deltaTime);
    }

    private void RotateUpdate()
    {
        if (!_isRotate)
            return;

        _rotateTarget.y = transform.position.y;
        Vector3 Forward = (_rotateTarget - transform.position).normalized;
        if (Forward != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(Forward),
                _stat.RotateSpeed * Time.deltaTime);
        }

        if (Vector3.Dot(Forward, transform.forward) >= 0.9f)
        {
            _isRotate = false;
        }
    }

    /// <summary>
    /// 움질일 때 회전을하는 함수
    /// </summary>
    public void RotateToTarget(Vector3 target)
    {
        _rotateTarget = target;
        _isRotate = true;
    }

    /// <summary>
    /// 움직이는 함수 (방향전환은 하지 않음)
    /// </summary>
    /// <param name="target"> 목적지</param>
    public void MoveToTarget(Vector3 target)
    {
        _isMoving = true;
        _destination = target;
        _destination.y = transform.position.y;
    }

    /// <summary>
    /// 움직이는 함수 (방향전환)
    /// </summary>
    /// <param name="target"> 몾적지 </param>
    public void MoveToTargetLookAt(Vector3 target)
    {
        MoveToTarget(target);
        RotateToTarget(target);
    }

    public virtual void OnDamage(float damage)
    {
        _stat.Hp -= damage;
    }

    public virtual void OnDamage(float damage, float value)
    {
        OnDamage(damage);
    }

    public virtual void OnDead()
    {
    }

    public virtual void MoveStop()
    {
        _isMoving = false;
        _destination = transform.position;
    }

    public virtual void InitMonster(Vector3 homePos)
    {

    }

    protected void Update_SpawnNoise()
    {
        if (!_isSpawn)
            return;

        _noiseValue += 0.08f;

        foreach (Renderer render in transform.GetComponentsInChildren<Renderer>())
        {
            render.material.SetFloat("_Hide", Mathf.Clamp(_noiseValue, -1.0f, 1.0f));
        }

        if (_noiseValue >= 1.0f)
            _isSpawn = false;
    }

    public void OnSpawn()
    {
        _isSpawn = true;
        _noiseValue = -1.0f;
        foreach (Renderer render in transform.GetComponentsInChildren<Renderer>())
        {
            render.material.SetFloat("_Hide", Mathf.Clamp(_noiseValue, -1.0f, 1.0f));
        }
    }
}