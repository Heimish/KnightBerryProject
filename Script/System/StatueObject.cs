using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueObject : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    private GameObject _destroy;
    private GameObject _laser;
    [SerializeField]
    private GameObject _center;
    private float _destTime;
    private bool _dest;
    private bool _laserStart;
    private bool _laserCrash;
    private Vector3 _initPos;
    private CrystalObject _crystal;

    public GameObject DestroyEffect { get { return _destroy; } }
    public CrystalObject Crystal { get { return _crystal; } set { _crystal = value; } }
	void Start ()
    {
        _anim = GetComponentInChildren<Animator>();
        _laser = transform.Find("Laser").gameObject;
        _laser.gameObject.SetActive(false);
        _laser.GetComponentInChildren<StatueLaser>().Statue = this;
        _initPos = _laser.transform.position;
    }
	
	void Update ()
    {
        DestroyUpdate();
    }

    private void DestroyUpdate()
    {
        if (!_laserStart)
            return;

        _laser.transform.LookAt(_center.transform.position);
        Vector3 scale = _laser.transform.localScale;
        scale.z += 100.0f * Time.deltaTime;
        _laser.transform.localScale = scale;
        _laser.transform.position = _initPos + _laser.transform.forward * (_laser.transform.localScale.z / 2.0f) - _laser.transform.forward;
    }

    public void LaserCrash()
    {
        _laserCrash = true;
        _laserStart = false;
    }

    public void EndCamera()
    {
        _crystal.ViewCamera.gameObject.SetActive(false);

    }

    public void SetDestroyEffect()
    {
        _destroy.SetActive(true);
        StartCoroutine(Co_EffectOff());
        _dest = true;
        _anim.SetBool("On", true);
    }

    IEnumerator Co_EffectOff()
    {
        yield return new WaitForSeconds(1.5f);
        _destroy.SetActive(false);
        _laser.gameObject.SetActive(true);
        _laserStart = true;
    }
}
