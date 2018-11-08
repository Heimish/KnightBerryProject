using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillArrowSubArrow : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _target;
    private bool _move;
    private bool _ready;
    private float _speed;
    private float _deadTime;
    private Vector3 _readyPos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        ReadyRotate();
        DeadCheck();
    }

    private void Move()
    {
        if (_ready)
            return;

        if (!_move)
            return;

        _rigidbody.velocity = transform.forward * _speed * Time.deltaTime;
    }

    private void ReadyRotate()
    {
        if (!_ready)
            return;

        Vector3 pos = _target.position;
        pos.y += 1.0f;

        transform.LookAt(pos);
    }

    public void Ready(Transform target)
    {
        _ready = true;
        _move = false;
        _target = target;
        _rigidbody.velocity = Vector3.zero;
        _deadTime = 0.0f;
    }

    public void OnArrow(float speed = 30.0f)
    {
        _move = true;
        _ready = false;
        _speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MapObject" ||
            other.tag == "Player" ||
            other.tag == "Shild")
        {
            if (other.tag == "Player")
            {
                CPlayerManager._instance.PlayerHp(0.2f, 1, WitchValueManager.I.ArrowDamage);
            }
            if (other.tag == "Shild")
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, WitchValueManager.I.ArrowDamage);
            }
            gameObject.SetActive(false);
        }
    }

    private void DeadCheck()
    {
        if (!_move)
            return;

        _deadTime += Time.deltaTime;

        if (_deadTime <= 5.0f)
            return;

        gameObject.SetActive(false);
    }
}
