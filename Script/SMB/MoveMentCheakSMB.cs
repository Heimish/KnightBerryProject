using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMentCheakSMB : StateMachineBehaviour
{

    private bool transIn = false; // 애니가 실행중인가
    private bool exited = false; // 애니가 끝났는가

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transIn = animator.IsInTransition(layerIndex);
        exited = false;
        // 플레이어 이동 & 예외처리상태 OFF
        animator.GetComponent<CPlayerAniEvent>().MoveTypes(1);
        animator.GetComponent<CPlayerManager>().m_isRotationAttack = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!exited)
        {
            if (animator.IsInTransition(layerIndex))
            {
                if (!transIn)
                {
                    // 플레이어 이동 & 예외처리상태 ON
                    animator.GetComponent<CPlayerAniEvent>().MoveTypes(2);
                    animator.GetComponent<CPlayerManager>().m_isRotationAttack = true;
                    exited = true;
                }
            }
            else if (transIn)
            {
                transIn = false;
            }
        }
    }
}
