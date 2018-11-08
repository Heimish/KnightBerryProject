using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect_State
{
    None,
    Attack1,
    Attack2,
    Attack3,
}
public class CEffectManager : MonoBehaviour
{
    // 싱글턴
    public static CEffectManager _instance = null;

    // Enum값으로 이펙트 설정
    Effect_State _Effect_State = Effect_State.None;

    // 이펙트 
    public GameObject[] _SkillObject = null;
    private int nResetTimer;
    private void Awake()
    {
        // 싱글턴
        CEffectManager._instance = this;

    }

    void Update()
    {
        
    }

    // 이펙트 불러올 키값,좌표
    public GameObject EffectCreate(int nID, float timer)
    {
        if (_SkillObject[nID].activeInHierarchy == false)
        {
            _SkillObject[nID].SetActive(true);
            StartCoroutine("ResetActive");
        }

        return _SkillObject[nID];
    }

 
}
