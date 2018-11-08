using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBullet : MonoBehaviour
{
    protected QueenMushroom _queenMushroom;
    protected Vector3 _direction;
    private Vector3 _target;

    [SerializeField]
    protected float _speed;

    private float DeleteTime;

    void Start()
    {
        DeleteTime = 0;
    }

    void Update()
    {
        DeleteTime += Time.deltaTime;
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (DeleteTime >= 7f)
        {
            gameObject.SetActive(false);
            DeleteTime = 0;
        }
    }

    //public void HitEffect(Vector3 From)
    //{
    //    Instantiate(StunHitEffect, From, Quaternion.identity);
    //}

    public void HitEffect(Vector3 From)
    {
        for (int i = 0; i < BulletObjectPool._instance.StunBulletHitEffects.Length; i++)
        {
            if (!BulletObjectPool._instance.StunBulletHitEffects[i].activeInHierarchy)
            {
                BulletObjectPool._instance.StunBulletHitEffects[i].transform.position = From;
                BulletObjectPool._instance.StunBulletHitEffects[i].SetActive(true);
                return;
            }
        }
    }

    public void InitStunBullet(QueenMushroom queen, Vector3 from, Vector3 target)
    {
        _queenMushroom = queen;
        from.y += 0.9f;
        from.z += 0.3f;
        from.x += Random.Range(-2.4f, 2.4f);
        target.y = from.y;
        transform.position = from;
        _direction = (target - from).normalized;
        transform.Translate(_direction * _speed * Time.deltaTime * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MapObject" ||
              other.tag == "Player" ||
              other.tag == "Shild")
        {
            if (other.tag == "Player")
            {
                HitEffect(transform.position);
                CPlayerSturn._instance.isSturn = true;
                CPlayerManager._instance.PlayerHp(0.2f, 1, _queenMushroom.AttackDamage);
            }
            else if (other.tag == "Shild")
            {
                HitEffect(transform.position);
                CPlayerManager._instance.PlayerHp(0.2f, 2, _queenMushroom.AttackDamage);
            }
            gameObject.SetActive(false);//레도잉 파이팅
        }
    }
}