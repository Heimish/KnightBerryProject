
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerShild : CPlayerBase
{
    public BoxCollider _BoxCollider;
    public bool m_bShildCollider;
    private float m_fShildTimer;

    [SerializeField]
    private bool isShildCounter;
    public bool _isShildCounter { get { return isShildCounter; } set { value = isShildCounter; } }


    void Start () {
        //_BoxCollider.enabled = false;
        m_bShildCollider = false;
        m_fShildTimer = 0;
    }
	
	
	void Update ()
    {
        ShildCheck(); 

        if (_PlayerManager._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
        {
            _BoxCollider.enabled = true;
        }
        else
            _BoxCollider.enabled = false;
    }

    void ShildCheck()
    {
        if (!m_bShildCollider) // hit 상태가 아닐때 초기화
        {
            ShildHitReset();
            return;
        }

       m_fShildTimer += Time.deltaTime;
       _PlayerManager._PlayerAni_Contorl.m_bDefenseBack = true; // 히트 애니출력
       if (m_fShildTimer >= 0.3f) // 일정시간뒤 히트 꺼줌
       {
           m_bShildCollider = false;
       }
    }

    void ShildHitReset()
    {
        m_fShildTimer = 0;
        _PlayerManager._PlayerAni_Contorl.m_bDefenseBack = false;
    }
}
