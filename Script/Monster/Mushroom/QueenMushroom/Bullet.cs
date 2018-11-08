using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected QueenMushroom _queenMushroom;
    protected Vector3 _direction;
    private Vector3 _target;

    [SerializeField]
    protected float _speed;
    public GameObject[] BulletEffects;
    private Vector3 Bullet_Poket;

    private float DeleteTime;

    void Awake()
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
    //    GeneralHitEffect.SetActive(true);
    //    Instantiate(GeneralHitEffect, From, Quaternion.identity);
    //}

    public void HitEffect(Vector3 From)
    {
        for (int i = 0; i < BulletObjectPool._instance.BulletHitEffects.Length; i++)
        {
            if (!BulletObjectPool._instance.BulletHitEffects[i].activeInHierarchy)
            {
                BulletObjectPool._instance.BulletHitEffects[i].transform.position = From;
                BulletObjectPool._instance.BulletHitEffects[i].SetActive(true);
                return;
            }
        }
    }

    public void InitBullet(QueenMushroom queen, Vector3 from, Vector3 target)
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
                CPlayerManager._instance.PlayerHp(0.2f, 1, _queenMushroom.AttackDamage);
            }
            if (other.tag == "Shild")
            {
                HitEffect(transform.position);
                CPlayerManager._instance.PlayerHp(0.2f, 2, _queenMushroom.AttackDamage);
            }
            gameObject.SetActive(false);
        }
    }
}