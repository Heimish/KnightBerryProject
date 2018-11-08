using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CCameraFind : MonoBehaviour
{
    public Transform _obj = null;
    public static CCameraFind _instance = null;
    public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObj;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f; // 움직이는 속도값
    public float mouseX; // 현재 계속 받아오는 마우스X값
    public float mouseY; // 현재 계속 받아오는 마우스Y값
    public float finalInputX; // 마우스X값 저장
    public float finalInputZ; // 마우스Y값 저장
    private float rotY = 0.0f; // 카메라의 Y축값
    private float rotX = 0.0f; // 카메라의 X축값

    private float _zoomTime;
    private bool _isZoom;
    private float _zoomValue = 4.0f;
    private Vector3 _direction;

    // 캐릭터가 카메라를 바라보는지 
    public bool m_bCamera;
    // 카메라의 로테이션 y값을 저장하기 위해 사용
    public Quaternion _CameraRight = Quaternion.identity;

    public float m_fLerpSpeed;
    public float LerpSpeedValue = 500.0f;
    public bool isDash;
    private float fTime;

    public bool isLockOn; // 락온

    void Start()
    {
        CCameraFind._instance = this;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_bCamera = true;
        m_fLerpSpeed = LerpSpeedValue;
        _zoomValue = CCameraRayObj._instance.MaxDistanceValue;
    }
    void Update()
    {
        TargetLockOn(_obj);
        MouseRot();
        BlinkCamera();
        _direction = (CameraFollowObj.transform.position - transform.position).normalized;
        CameraUpdater();
        ZoomUpdate();
    }
    void MouseRot()
    {
        if (isLockOn)
            return;

        // 마우스 x, y값 받아오기
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // 설정됀 마우스의 값을 서로 더해줌 
        finalInputX = mouseX;
        finalInputZ = mouseY;

        // 마우스정보 * 지정속도 -> rotY(카메라 로테이션)값에 계속 더해줌
        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        // 카메라 각도 360' 회전하는걸 막아주며 지정됀 값범위 내에서만 확인가능
        rotX = Mathf.Clamp(rotX, -30, clampAngle);
        // 카메라 로테이션 돌려주기
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, Time.deltaTime * 15);
        //transform.rotation = localRotation;

        // 카메라 마우스 y값을 보간
        _CameraRight.eulerAngles = new Vector3(0, rotY, 0);
    }
    void LateUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.CapsLock))
        //{
        //    if (isLockOn)
        //    {
        //        isLockOn = false;
        //        //m_fLerpSpeed = 100;
        //    }
        //    else
        //        isLockOn = true;
        //}

    }
    void CameraUpdater()
    {
        // 플레이어 중심에 설정한 오브젝트 좌표값을 받아옴
        Transform target = CameraFollowObj.transform;
        transform.position = Vector3.Lerp(transform.position, target.position, m_fLerpSpeed * Time.deltaTime);
    }
    public void BlinkCameraOn()
    {
        fTime = 0.0f;
        m_fLerpSpeed = 0.0f;
        isDash = true;
    }
    void BlinkCamera()
    {
        if (!isDash)
        {
            m_fLerpSpeed = LerpSpeedValue;
            return;
        }

        //CCameraRayObj._instance.MaxCamera(3.0f);
        fTime += Time.deltaTime;
        m_fLerpSpeed += Time.deltaTime * 20.0f;
        //if (fTime > 0.6f)
        //{
        //    CCameraRayObj._instance.MaxCamera(3.5f);
        //}
        if (fTime > 0.8f)
        {
            isDash = false;
        }
    }

    public void TargetLockOn(Transform target)
    {
        if (!isLockOn)
            return;

        Vector3 curPos = target.position - transform.position;
        curPos.Normalize();

        if (CPlayerManager._instance.m_bAttack ||
            CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
        {
            CPlayerLockOnRot._instance.LockRot(target);
        }

        if (Vector3.Distance(transform.position, target.position) < 2.0f)
        {

        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(curPos), Time.deltaTime * InspectorManager._InspectorManager.fLockOnSpeed);
        }
    }

    public void SetZoom(float value)
    {
        if (value == 0.0f)
            return;

        _zoomTime = 1.0f;
        _zoomValue -= value;
        _isZoom = true;
        CCameraRayObj._instance.MaxCamera(_zoomValue);
    }

    private void ZoomUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, CameraFollowObj.transform.position + (_direction * _zoomValue), Time.deltaTime * 3.0f);
        //transform.position = CameraFollowObj.transform.position;

        if (!_isZoom)
            return;

        _zoomTime -= Time.deltaTime;

        if (_zoomTime > 0.0f)
            return;

        _isZoom = false;
        _zoomTime = 0.0f;
        _zoomValue = CCameraRayObj._instance.MaxDistanceValue;
        CCameraRayObj._instance.MaxCamera(_zoomValue);
    }
}
