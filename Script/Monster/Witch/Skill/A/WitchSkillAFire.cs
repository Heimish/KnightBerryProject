using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillAFire : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _target;
    private Vector3 _moveTarget;
    private int _move;
    private float _speed;
    private Vector3 _readyPos;
    private float _deadTime;
    private float _readyTime;

    // properties
    public int MoveState { get { return _move; } }
    public Vector3 ReadyPos { get { return _readyPos; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        ReadyMove();
        DeadCheck();
        _moveTarget = _target.position;
        _moveTarget.y += 1.21f;
    }

    private void Move()
    {
        if (_move != 2)
            return;

        transform.LookAt(_moveTarget);
        _rigidbody.velocity = transform.forward * WitchValueManager.I.OrbSpeed * Time.deltaTime;
    }

    private void ReadyMove()
    {
        if (_move != 1)
            return;

        _readyTime += Time.deltaTime;

        if (_readyTime < WitchValueManager.I.OrbDealyTime)
            return;

        _move = 2;

        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, _readyPos, 5.0f * Time.deltaTime);
    }

    public void Ready(Vector3 readyPos, Transform target)
    {
        _move = 1;
        _readyPos = readyPos;
        _target = target;
        _moveTarget = _target.position;
        _moveTarget.y += 1.21f;
        _deadTime = 0.0f;
        _readyTime = 0.0f;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _readyPos;
    }

    //public void FireInit(Transform target, float speed = 30.0f)
    //{
    //    _move = 2;
    //    _target = target;
    //    _speed = speed;
    //    transform.LookAt(_target);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MapObject" ||
            other.tag == "Player" || 
            other.tag == "Shild")
        {
            if (other.tag == "Player")
            {
                CPlayerManager._instance.PlayerHp(0.2f, 1, WitchValueManager.I.OrbDamage);
                EffectManager.I.OnEffect(EffectType.Witch_OrbHit, transform.position, transform.rotation, 0.3f, 1);
                //gameObject.SetActive(false);
            }
            if (other.tag == "Shild")
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, WitchValueManager.I.OrbDamage);
                EffectManager.I.OnEffect(EffectType.Witch_OrbHit, transform.position, transform.rotation, 0.3f, 1);
                //gameObject.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }

    private void DeadCheck()
    {
        _deadTime += Time.deltaTime;

        if (_deadTime < WitchValueManager.I.OrbLifeTime)
            return;

        _deadTime = 0.0f;
        gameObject.SetActive(false);
    }
}
