using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CPlayerMove : CPlayerBase
{
    public Vector3 m_moveDir = Vector3.zero;
    public Vector3 m_destination = Vector3.zero;

    private int m_nHStopFream = 0;
    private int m_nVStopFream = 0;
    public int m_nMaxFream = 3;

    public float fHorizontal;
    public float fVertical;



    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (_PlayerManager.isDead)
            return;

        fHorizontal = Input.GetAxisRaw("Horizontal");
        fVertical = Input.GetAxisRaw("Vertical");

        Vector3 horizontalPos = CCameraFind._instance.transform.right * fHorizontal;
        Vector3 verticalPos = CCameraFind._instance.transform.forward * fVertical;

        m_destination = transform.position + horizontalPos + verticalPos;
        m_destination.y = transform.position.y;

        Vector3 direction = m_destination - transform.position;
        m_moveDir = direction.normalized;

        if (!Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.S))
        {
            fVertical = 0.0f;
        }

        if (!Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.D))
        {
            fHorizontal = 0;
        }

        if (Mathf.Abs(fHorizontal) > 0 || Mathf.Abs(fVertical) > 0)
        {
            if (_PlayerManager.m_bMove)
            {
                if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                    _PlayerManager._PlayerAni_Contorl._PlayerAni_State_Shild = PlayerAni_State_Shild.IdleRun;
                else if (!CPlayerManager._instance._PlayerSwap.m_bSwapAttack)
                    _PlayerManager._PlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.IdleRun;

                //if (_PlayerManager._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Dash)
                //    return;

                _PlayerManager._PlayerController.Move((m_destination - transform.position).normalized * Time.deltaTime * _PlayerManager.EDITOR_MOVESPEED);
            }
        }

        if (Mathf.Abs(fHorizontal) == 0f &&
            Mathf.Abs(fVertical) == 0f)
        {
            if (!Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.D) &&
            !Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.S) &&
            _PlayerManager.m_bMove)
            {

                if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                    _PlayerManager._PlayerAni_Contorl._PlayerAni_State_Shild = PlayerAni_State_Shild.IdleRun;
                else if (!CPlayerManager._instance._PlayerSwap.m_bSwapAttack)
                    _PlayerManager._PlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.IdleRun;
            }
        }
    }

    public void SpeedReset()
    {
        _PlayerManager.m_bMove = false;
        _PlayerManager.m_isRotationAttack = true;
        _PlayerManager.m_nAttackCombo = 0;
        fHorizontal = 0;
        fVertical = 0;
    }
}

