using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerAniEvent : CPlayerBase
{
    private bool m_bMoveAttack;
    private bool m_bMove2Attack;

    public float m_fStartTime;
    public float m_fEndTime;
    public float m_fEnd2Time;
    public float m_fforwordSpeed;
    public float m_fforword2Speed;


    // 플레이어 로테이션
    public void RotationFalse()
    {
        _PlayerManager.m_isRotationAttack = false;
    }
    public void RotationTrue()
    {
        _PlayerManager.m_isRotationAttack = true;
    }
    // 플레이어 움직임 막기
    public int MoveTypes(int type)
    {
        if (type == 1)
        {
            _PlayerManager.m_bAttack = true;
            _PlayerManager.m_bMove = false;
            _PlayerManager.fRotationSave = _PlayerManager.vPlayerQuaternion.y;
        }
        if (type == 2)
        {
            _PlayerManager.m_bAttack = false;
            _PlayerManager.m_bMove = true;
            _PlayerManager.EDITOR_ROTATIONSPEED = 500.0f;
        }
        return type;
    }
    public void MoveOn()
    {
        MoveTypes(2);
        RotationTrue();
    }
    public void MoveOff()
    {
        MoveTypes(1);
        RotationFalse();
    }

    public int CameraType(int type)
    {
        if (type == 1) CCameraFind._instance.m_bCamera = true;
        if (type == 2) CCameraFind._instance.m_bCamera = false;

        return type;
    }

    public void AttackComboAdd()
    {
        _PlayerManager.m_nAttackCombo++;
    }
    public void AttackComboReset()
    {
        _PlayerManager.m_nAttackCombo = 0;
    }

    public int AttackMove(int type)
    {
        //if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            if (type == 1)
            {
                m_fStartTime = 0;
                m_bMoveAttack = true;
            }
            if (type == 2)
            {
                m_fStartTime = 0;
                m_bMove2Attack = true;
            }
        }
        return type;
    }

    private void Update()
    {
        //if(m_bMoveAttack)
        //{
        //    _PlayerManager._PlayerMove.fVertical = 0.0f;
        //    _PlayerManager._PlayerMove.fHorizontal = 0.0f;
        //    m_fStartTime += Time.deltaTime;
        //    _PlayerManager._PlayerController.Move(transform.forward * Time.deltaTime * m_fforwordSpeed);
        //    if (m_fStartTime >= m_fEndTime)
        //    {
        //        m_fStartTime = 0;
        //        m_bMoveAttack = false;
        //    }
        //}

        //if(m_bMove2Attack)
        //{
        //    m_fStartTime += Time.deltaTime;
        //    _PlayerManager._PlayerController.Move(transform.forward * Time.deltaTime * m_fforword2Speed);
        //    if (m_fStartTime >= m_fEnd2Time)
        //    {
        //        m_fStartTime = 0;
        //        m_bMove2Attack = false;
        //    }
        //}
    }
}

