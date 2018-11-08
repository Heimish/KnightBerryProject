using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorManager : MonoBehaviour
{
    public static InspectorManager _InspectorManager = null;

    public float fStmAuto;
    public float fStmAutoTime;
    public float fStmDash;

    public float fRotation;
    public float fMoveSpeed;
    public float fMoveAngle;

    public int[] nDamgeShild = new int[5];
    public int[] nDamgeScythe = new int[3];
    public float[] nGroggyShild = new float[5];
    public float[] nGroggyScythe = new float[3];

    public float fShildDamge;
    public float fSwapCoolTime;

    public float fCountGroggy;
    public float fCountAttackDamge;
    public float fCountAttackReturnTime;

    public float fScytheTime;
    public float fScytheTimeHpDown;

    public float fScytheTimeHpExponential;
    public float fScytheAttackHpAdd;

    public float fScytheStartSkillDamge;
    public float fScytheSkill2Damge;

    public float fSturnTime;

    public float fPlayerAttackStiff;
    public float fSweatStm;
    public float fShildRunDamge;
    public float fShildRunStm;

    public float fPlayerHornTime;
    public int nPlayerHitAddPower;
    public float fPlayerSweatCountTime; // 흘리기후 몇초동안 반격할수있는 시간을 유지하는지
    public float fLockOnSpeed;

    public float[] TankerAttackZoom;
    public float[] DealerAttackZoom;

    public float AfterImageDeadTime;

    public float ScytheSwapSkillDis = 4.0f;
    public float ScytheSwapSkillDamage = 50.0f;

    public float ScytheChopSkillDis = 4.0f;
    public float ScytheChopSkillDamage = 100.0f;

    public float SwapMaxGauge = 100.0f;

    public float ScytheSkillCoolTime = 6.0f;
    public float ShieldSkillCoolTime = 6.0f;

    public float ScytheShiftCoolTime = 4.0f;
    public float ShieldShiftCoolTime = 3.0f;

    public float VineSpeed = 1.7f;
    public float ScytheAttackDis = 3.0f;

    void Awake()
    {
        _InspectorManager = this;
        StartCoroutine(StartAutoStm());
    }

    IEnumerator StartAutoStm()
    {
        while (true)
        {
            yield return new WaitForSeconds(fStmAutoTime);
            if (CPlayerManager._instance.m_PlayerStm < 100)
            {
                CPlayerManager._instance.m_PlayerStm += fStmAuto;
            }
        }

    }
}
