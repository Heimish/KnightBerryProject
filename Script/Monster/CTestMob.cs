using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobState
{
    Idle,
    Come,
    Back
}
public class CTestMob : MonoBehaviour
{
    public MobState _MobState = MobState.Idle;
    public float m_fComeSpeed;
    public float m_fBackSpeed;
    public float m_fDistance;
    public float m_fMaxDistance;

    void Update ()
    {
       //transform.LookAt(CGameManager._instance._PlayerPos);
       //if(Input.GetKeyDown(KeyCode.Q))
       //{
       //    if(CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
       //    {
       //        _MobState = MobState.Come;
       //    }
       //    else
       //    {
       //        _MobState = MobState.Back;
       //    }
       //}
       //State();
    }

    void State()
    {
        switch(_MobState)
        {
            case MobState.Idle:
                {
                    Idle();
                }
                break;
            case MobState.Come:
                {
                    Come();
                }
                break;
            case MobState.Back:
                {
                    Back();
                }
                break;
        }
    }

    void Idle()
    {
    }
    void Come()
    {
        transform.position += transform.forward * m_fComeSpeed * Time.deltaTime;

        if (Vector3.Distance(CGameManager._instance._PlayerPos.position, transform.position) < m_fDistance + 0.5f)
        {
            
            CPlayerManager._instance._PlayerSwap.m_bSwapAttack = true;
            TimeScalManager._instance.m_fTimeScal = 0.1f;
            if (Vector3.Distance(CGameManager._instance._PlayerPos.position, transform.position) < m_fDistance)
            {
                _MobState = MobState.Idle;
                //CPlayerManager._instance.PlayerHitCamera2(5.0f);
            }
        }
    }
    void Back()
    {
        transform.position -= transform.forward * m_fBackSpeed * Time.deltaTime;
        if (Vector3.Distance(CGameManager._instance._PlayerPos.position, transform.position) > m_fMaxDistance)
        {
            _MobState = MobState.Idle;
        }
    }
}
