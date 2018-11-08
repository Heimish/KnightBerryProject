using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer_AniChange : CPlayerBase
{
    // 컨트롤러 직업에 맞게 설정
    public RuntimeAnimatorController _Shythe;
    public RuntimeAnimatorController _Shild;

    // 머테리얼 직업에 맞게 설정
    public Material _ScytheMat;
    public Material _ShildMat;

    // 머테리얼 적용시킬 오브젝트
    public GameObject _ChangeMaterial;

    void Start()
    {
        // 애니메이터 처음 직업으로 설정
        GetComponent<Animator>().runtimeAnimatorController = _Shild;
    }
    private void Update()
    {
        ChangeAni();
    }
    public void ChangeAni()
    {
        // 현재 캐릭터의 직업이 검,방패 면 머테리얼, 컨트롤러 적용하기 ~ else if도 동일.
        if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            _ChangeMaterial.GetComponent<Renderer>().material = _ShildMat;
            GetComponent<Animator>().runtimeAnimatorController = _Shild;
        }
        else if(_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Scythe)
        {
            _ChangeMaterial.GetComponent<Renderer>().material = _ScytheMat;
            GetComponent<Animator>().runtimeAnimatorController = _Shythe;
        }
    }
}
