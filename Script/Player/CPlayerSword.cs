using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSword : CPlayerBase
{
    static public CPlayerSword I = null;

    public BoxCollider _ScytheCollder;
    public BoxCollider _SowrdCollder;
    public BoxCollider _shieldCollider;
    public BoxCollider _CounteCollder;
    public BoxCollider _StartScytheSkillCollder;
    public BoxCollider _ShildRunCollder;

    private bool m_bCollder;
    private float m_fCollderTimer;
    

    void Start()
    {
        CPlayerSword.I = this;
        _ScytheCollder.enabled = false;
        _SowrdCollder.enabled = false;

        m_bCollder = false;
        m_fCollderTimer = 0;
    }
    void Update()
    {
        // 콜리더 켜지면 일정 시간뒤에 꺼짐
        //if(m_bCollder)
        //{
        //    m_fCollderTimer += Time.deltaTime;

        //    if(m_fCollderTimer >= 0.3f)
        //        return;

        //    m_fCollderTimer = 0;
        //    m_bCollder = false;
        //    CollderFalse();
        //}
    }
    // 충돌 부분에 알맞게 함수 호출

    // 낫모드 공격
    public void ScytheTrue()
    {
        _ScytheCollder.enabled = true;
        m_bCollder = true;
    }
    // 검방모드 공격
    public void SowrdTrue()
    {
        _SowrdCollder.enabled = true;
        m_bCollder = true;
    }

    public void ShieldTrue()
    {
        _shieldCollider.enabled = true;
        m_bCollder = true;
    }

    // 검방모드 카운터 공격
    public void CounterTrue()
    {
        _CounteCollder.enabled = true;
        m_bCollder = true;
    }
    // 낫모드 스킬1 공격
    public void ScytheSkill1()
    {
        _StartScytheSkillCollder.enabled = true;
        m_bCollder = true;
    }
    // 검방모드 방패돌진 공격
    public void ShildRun(int type)
    {
        if (type == 1)
            _ShildRunCollder.enabled = true;
        else if (type == 2)
            _ShildRunCollder.enabled = false;
    }
    public void CollderFalse()
    {
        _SowrdCollder.enabled = false;
        _ScytheCollder.enabled = false;
        _CounteCollder.enabled = false;
        _StartScytheSkillCollder.enabled = false;
        _ShildRunCollder.enabled = false;
    }

    private void AttackEnd()
    {
        _SowrdCollder.enabled = false;
        _ScytheCollder.enabled = false;
        _shieldCollider.enabled = false;
        _CounteCollder.enabled = false;
    }

    private void DamageAllEnd()
    {
        AttackEnd();
        ShildRun(2);
    }
}
