using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    private PlayerBase _player;

    private void Awake()
    {
        _player = GetComponent<PlayerBase>();
    }

    private void MoveSoundPlay()
    {

    }

    private void NextAttack()
    {
        _player.NextAttack = true;
    }

    //private void EndAttackEvent()
    //{
    //    _player.IsAttacking = false;
    //}

    private void StartIdle()
    {
        Debug.Log("BBBBBBB");
        _player.AttackEnd();
        _player.SetAnimation(CharacterAnimState.IdleRun);
    }

    private void OnAttack()
    {
        _player.OnAttackHit();
    }

    private void EndAttack()
    {
        _player.OffAttackHit();
    }
}
