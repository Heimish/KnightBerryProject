using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBullet : MonoBehaviour
{
    protected QueenMushroom _queenMushroom;
    protected Vector3 _direction;
    private Transform _target;
    private Transform _from;

    [SerializeField]
    protected float _speed;

    private float DeleteTime;

    void Awake()
    {
        DeleteTime = 0;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        DeleteTime += Time.deltaTime;
        _from = _queenMushroom.From;

        transform.Translate(_from.forward * _speed * Time.deltaTime);

        if (DeleteTime >= 7f)
        {
            gameObject.SetActive(false);
            DeleteTime = 0;
        }
    }

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

    public void InitSBullet(QueenMushroom queen, Vector3 from, float rotate)
    {
        _queenMushroom = queen;
        transform.position = from;
        transform.rotation = Quaternion.Euler(0, -45f + rotate, 0);
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