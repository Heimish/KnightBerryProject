using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public static BulletObjectPool _instance = null;

    private List<Bullet> _bullets = new List<Bullet>();
    private List<StunBullet> _stunBullets = new List<StunBullet>();
    private List<SBullet> _sbullets = new List<SBullet>();
    private List<SStunBullet> _sstunBullets = new List<SStunBullet>();

    [Header("BulletHitEffect")]
    public GameObject[] BulletHitEffects;
    public GameObject[] StunBulletHitEffects;
    public float[] BHETime;
    public float[] SBHETime;
    private Vector3 Reset;

    void Awake()
    {
        BulletObjectPool._instance = this;

        foreach (Bullet b in GetComponentsInChildren<Bullet>())
        {
            b.gameObject.SetActive(false);
            _bullets.Add(b);
        }

        foreach (StunBullet s in GetComponentsInChildren<StunBullet>())
        {
            s.gameObject.SetActive(false);

            _stunBullets.Add(s);
        }

        foreach (SBullet sb in GetComponentsInChildren<SBullet>())
        {
            sb.gameObject.SetActive(false);
            _sbullets.Add(sb);
        }

        foreach (SStunBullet ss in GetComponentsInChildren<SStunBullet>())
        {
            ss.gameObject.SetActive(false);

            _sstunBullets.Add(ss);
        }
    }

    public Bullet SetBullet(QueenMushroom queen, Vector3 from, Vector3 target)
    {
        Bullet bullet = GetBullet();
        bullet.InitBullet(queen, from, target);

        return bullet;
    }

    public StunBullet SetStunBullet(QueenMushroom queen, Vector3 from, Vector3 target)
    {
        StunBullet stunbullet = GetStunBullet();
        stunbullet.InitStunBullet(queen, from, target);

        return stunbullet;
    }

    public SBullet SetSBullet(QueenMushroom queen, Vector3 from, float rotate)
    {
        SBullet sbullet = GetSBullet();
        sbullet.InitSBullet(queen, from, rotate);

        return sbullet;
    }

    public SStunBullet SetSStunBullet(QueenMushroom queen, Vector3 from, float rotate)
    {
        SStunBullet sstunbullet = GetSStunBullet();
        sstunbullet.InitSStunBullet(queen, from, rotate);

        return sstunbullet;
    }

    private Bullet GetBullet()
    {
        Bullet bullet = null;

        foreach (Bullet b in _bullets)
        {
            if (b.gameObject.activeInHierarchy)
                continue;

            bullet = b;
        }

        if (bullet == null)
            bullet = CreateBullet();

        bullet.gameObject.SetActive(true);

        return bullet;
    }

    private StunBullet GetStunBullet()
    {
        StunBullet stunbullet = null;

        foreach (StunBullet s in _stunBullets)
        {
            if (s.gameObject.activeInHierarchy)
                continue;

            stunbullet = s;
        }

        if (stunbullet == null)
            stunbullet = CreateStunBullet();

        stunbullet.gameObject.SetActive(true);

        return stunbullet;
    }

    private SBullet GetSBullet()
    {
        SBullet sbullet = null;

        foreach (SBullet sb in _sbullets)
        {
            if (sb.gameObject.activeInHierarchy)
                continue;

            sbullet = sb;
        }

        if (sbullet == null)
            sbullet = CreateSBullet();

        sbullet.gameObject.SetActive(true);

        return sbullet;
    }

    private SStunBullet GetSStunBullet()
    {
        SStunBullet sstunbullet = null;

        foreach (SStunBullet ss in _sstunBullets)
        {
            if (ss.gameObject.activeInHierarchy)
                continue;

            sstunbullet = ss;
        }

        if (sstunbullet == null)
            sstunbullet = CreateSStunBullet();

        sstunbullet.gameObject.SetActive(true);

        return sstunbullet;
    }

    private StunBullet CreateStunBullet()
    {
        StunBullet stunbullet = null;
        GameObject stunbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/StunBullet");
        stunbullet = Instantiate(stunbulletObject).GetComponent<StunBullet>();
        stunbullet.transform.SetParent(transform);
        stunbullet.gameObject.SetActive(false);
        _stunBullets.Add(stunbullet);

        return stunbullet;
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = null;
        GameObject bulletObject = (GameObject)Resources.Load("Prefabs/Bullets/Bullet");
        bullet = Instantiate(bulletObject).GetComponent<Bullet>();
        bullet.transform.SetParent(transform);
        bullet.gameObject.SetActive(false);
        _bullets.Add(bullet);

        return bullet;
    }

    private SStunBullet CreateSStunBullet()
    {
        SStunBullet sstunbullet = null;
        GameObject sstunbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/SStunBullet");
        sstunbullet = Instantiate(sstunbulletObject).GetComponent<SStunBullet>();
        sstunbullet.transform.SetParent(transform);
        sstunbullet.gameObject.SetActive(false);
        _sstunBullets.Add(sstunbullet);

        return sstunbullet;
    }

    private SBullet CreateSBullet()
    {
        SBullet sbullet = null;
        GameObject sbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/SBullet");
        sbullet = Instantiate(sbulletObject).GetComponent<SBullet>();
        sbullet.transform.SetParent(transform);
        sbullet.gameObject.SetActive(false);
        _sbullets.Add(sbullet);

        return sbullet;
    }

    public void SetBHEffect()
    {
        Reset = transform.position;
        Reset.y += -20f;

        for (int i = 0; i < BulletHitEffects.Length; i++)
        {
            if (BulletHitEffects[i].activeInHierarchy)
            {
                BHETime[i] += Time.deltaTime;
                if (BHETime[i] > 0.7f)
                {
                    BulletHitEffects[i].transform.position = Reset;
                    BulletHitEffects[i].SetActive(false);
                    BHETime[i] = 0;
                }
            }
        }

        for (int i = 0; i < StunBulletHitEffects.Length; i++)
        {
            if (StunBulletHitEffects[i].activeInHierarchy)
            {
                SBHETime[i] += Time.deltaTime;
                if (SBHETime[i] > 0.7f)
                {
                    StunBulletHitEffects[i].transform.position = Reset;
                    StunBulletHitEffects[i].SetActive(false);
                    SBHETime[i] = 0;
                }
            }
        }
    }

    void Update()
    {
        // Bullet Hit 이펙트 세팅
        SetBHEffect();
    }
}