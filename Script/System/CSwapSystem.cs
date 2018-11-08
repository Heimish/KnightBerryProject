using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSwapSystem : MonoBehaviour
{
    public GameObject BlinkIn;
    public GameObject BlinkOut;

    public static CSwapSystem _instance = null;

    public Avatar _ShildAvatar;
    public Avatar _ScytheAvatar;

    public RuntimeAnimatorController _ShildContorller;
    public RuntimeAnimatorController _ScytheContorller;

    public GameObject _ShildObj;
    public GameObject _ScytheObj;

    private Animator _Ani;
    private bool isCamera;

    private void Awake()
    {
        CSwapSystem._instance = this;

    }
    void Update()
    {
        if (isCamera)
        {
            //CPlayerManager._instance.PlayerHitCamera2(2.5f);
        }
    }
    public void ShildObjs()
    {
        _Ani = CPlayerManager._instance._PlayerAni_Contorl.PlayerAniFile;
        _Ani.avatar = _ShildAvatar;
        _Ani.runtimeAnimatorController = _ShildContorller;
        ObjSwap(true, false);
    }
    public void ScytheObjs(int type = 0)
    {
        if (type == 1)
        {
            isCamera = true;
            StartCoroutine("CameraJoom");
        }
        _Ani = CPlayerManager._instance._PlayerAni_Contorl.PlayerAniFile;
        _Ani.avatar = _ScytheAvatar;
        _Ani.runtimeAnimatorController = _ScytheContorller;
        ObjSwap(false, true);
    }
    IEnumerator CameraJoom()
    {
        yield return new WaitForSeconds(1f);
        //CPlayerManager._instance.PlayerHitCamera2(CCameraRayObj._instance.MaxDistanceValue);
        isCamera = false;
    }

    public void ObjSwap(bool shild, bool scythe)
    {
        _ShildObj.SetActive(shild);
        _ScytheObj.SetActive(scythe);
    }
    
}
