using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterStatue : MonoBehaviour
{
    private int _laserCnt;
    [SerializeField]
    private Camera _centerCamera;
    [SerializeField]
    private GameObject _laserObj;
    private float _cameraSwapTime;
    private bool _laserOn;
    private Animator _anim;
    private Vector3 _initPos;

    public bool IsLaserOn { get { return _laserOn; } set { _laserOn = value; } }
    public Camera CenterCamera { get { return _centerCamera; } }

	void Start ()
    {
        _anim = GetComponent<Animator>();
        _laserCnt = 0;
        _cameraSwapTime = 0.0f;
        _initPos = _laserObj.transform.position;
        _laserObj.GetComponentInChildren<CenterStatueLaser>().Statue = this;
    }

	void Update ()
    {
        CameraSwapUpdate();
        LaserOn();
        if (Input.GetKeyDown(KeyCode.F2))
        {
            _laserCnt = 4;
        }
    }

    private void CameraSwapUpdate()
    {
        if (_laserCnt < 4)
            return;

        if (_laserOn)
            return;

        _cameraSwapTime += Time.deltaTime;

        if (_cameraSwapTime < 1.9f)
            return;

        _centerCamera.gameObject.SetActive(true);
        _laserCnt--;

        _laserOn = true;
    }

    private void LaserOn()
    {
        if (!_laserOn)
            return;

        Vector3 scale = _laserObj.transform.localScale;
        scale.z += 20.0f * Time.deltaTime;
        _laserObj.transform.localScale = scale;
        _laserObj.transform.position = _initPos + _laserObj.transform.forward * (_laserObj.transform.localScale.z / 2.0f) - _laserObj.transform.forward;
    }

    public void LaserAdd()
    {
        _laserCnt++;

        if (_laserCnt >= 4)
            _anim.SetBool("On2", true);
    }
}
