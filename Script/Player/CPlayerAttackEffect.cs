using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerAttackEffect : MonoBehaviour
{
    /*
    public static CPlayerAttackEffect _instance = null;

    CPlayerManager m_manager;

    public GameObject[] _AttackEffect;
    public GameObject[] _ScytheAttackEffect;
    public GameObject[] _attackEffect;

    private void OnAttackEffect()
    {
        EffectType attackEnum = (EffectType)System.Enum.Parse(typeof(EffectType), "Hero_Tanker_Attack" + (m_manager.m_nAttackCombo + 1));
        EffectManager.I.OnEffect(attackEnum, transform, 0.3f);
    }

    private void Start()
    {
        CPlayerAttackEffect._instance = this;
        m_manager = GetComponent<CPlayerManager>();
    }
    private void Update()
    {
        //if (m_manager.m_nAttackCombo == 0 && !m_manager.m_bAttack)
        //{
        //    EffectOff();
        //}
        //if (m_manager._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.IdleRun)
        //{
        //    _AttackEffect[6].SetActive(false);
        //}
    }

    public void ShildAttackEffcet(bool one, bool two, bool three)
    {
        _AttackEffect[0].SetActive(one);
        _AttackEffect[1].SetActive(two);
        _AttackEffect[2].SetActive(three);
    }

    public void EffectOff()
    {
        if (m_manager._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            for (int i = 3; i < _AttackEffect.Length - 1; i++)
                _AttackEffect[i].SetActive(false);
            for (int i = 0; i < _attackEffect.Length; i++)
                _attackEffect[i].SetActive(false);
        }
        else
        {
            for (int i = 0; i < _ScytheAttackEffect.Length; i++)
                _ScytheAttackEffect[i].SetActive(false);
        }
    }
    //public void HitEffect1Off()
    //{
    //    _AttackEffect[0].SetActive(false);
    //}
    
    public void Effect1()
    {
        if (m_manager._PlayerSwap._PlayerMode == PlayerMode.Shield)
            ShildAttackEffcet(true, false, false);
        else
            _ScytheAttackEffect[0].SetActive(true);
    }
    public void Effect2()
    {
        if (m_manager._PlayerSwap._PlayerMode == PlayerMode.Shield)
            ShildAttackEffcet(false, true, false);
        else
            _ScytheAttackEffect[1].SetActive(true);
    }
    public void Effect3()
    {
        if (m_manager._PlayerSwap._PlayerMode == PlayerMode.Shield)
            ShildAttackEffcet(false, false, true);
        else
            _ScytheAttackEffect[2].SetActive(true);
    }
    public void Effect4()
    {
        _AttackEffect[3].SetActive(true);
    }
    public void Effect5()
    {
        _ScytheAttackEffect[3].SetActive(true);
    }
    public void Effect6()
    {
        _ScytheAttackEffect[4].SetActive(true);
    }
    public void Effect7()
    {
        _AttackEffect[4].SetActive(true);
    }
    public void Effect8()
    {
        _AttackEffect[5].SetActive(true);
    }
    public void Effect9(int type = 1)
    {
        if (type == 1)
        {
            _AttackEffect[6].SetActive(false);
            _AttackEffect[6].SetActive(true);
        }
        if (type == 2)
        {
            _AttackEffect[6].SetActive(false);
        }
    }
    */
}
