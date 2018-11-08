using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClipEvent : StateMachineBehaviour
{
    private PlayerBase _player;
    private bool transIn = false; // 애니가 실행중인가
    private bool exited = false; // 애니가 끝났는가

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<PlayerBase>();
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("IdleRun"))
        {
            _player.SetAnimation(CharacterAnimState.IdleRun);
            _player.AttackEnd();
            _player.Action = CharacterAction.IdleRun;
        }
        else if (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack1") ||
            stateInfo.IsName("Attack1") || stateInfo.IsName("Attack1"))
        {
            _player.Action = CharacterAction.Attack;
        }
        else if (stateInfo.IsName("Dash"))
        {
            _player.Action = CharacterAction.Dash;
        }
        else if (stateInfo.IsName("Defense_Loop"))
        {
            _player.Action = CharacterAction.Defense;
        }

        transIn = animator.IsInTransition(layerIndex);
        exited = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!exited)
        {
            if (animator.IsInTransition(layerIndex))
            {
                if (!transIn)
                {

                    exited = true;
                }
            }
            else if (transIn)
            {
                transIn = false;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (stateInfo.normalizedTime >= 1.0f)
        {
            if (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack1") ||
                stateInfo.IsName("Attack1") || stateInfo.IsName("Attack1"))
            {
                Debug.Log("AAAAAA");
                _player.AttackEnd();
                _player.Action = CharacterAction.IdleRun;
                return;
            }
            else if (stateInfo.IsName("Dash"))
            {
                _player.Action = CharacterAction.IdleRun;
                return;
            }
        }
        if (stateInfo.IsName("Defense_Loop"))
        {
            _player.Action = CharacterAction.IdleRun;
        }
    }
}
