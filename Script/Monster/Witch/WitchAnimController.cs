using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimController : MonoBehaviour
{
    private WitchBoss _witch;

    [SerializeField]
    private GameObject _trailRenderer;
    [SerializeField]
    private GameObject _closeAttackEffect;

    private void Awake()
    {
        _witch = GetComponent<WitchBoss>();
    }

    private void BeginAttack()
    {
        _witch.Collider.Collider.enabled = true;
    }

    private void OnTrail()
    {
        if (_witch.AttackIdx == 0)
            _trailRenderer.SetActive(true);
    }

    private void EndTrail()
    {
        _trailRenderer.SetActive(false);
    }

    private void OnAttack()
    {
        float DotValue = Vector3.Dot(_witch.transform.forward, (_witch.Target.transform.position - transform.position).normalized);
        if (_witch.DistanceCheck(3.0f))
        {
            if (DotValue > 1.0f || DotValue <= 0.0f)
                return;

            if (_witch.Target._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
                _witch.Target.PlayerHp(0.2f, 2, WitchValueManager.I.AttackDamge[_witch.AttackIdx]);
            else
                _witch.Target.PlayerHp(0.2f, 1, WitchValueManager.I.AttackDamge[_witch.AttackIdx]);
            if (_witch.Target._PlayerShild._isShildCounter)
            {
                _witch.SetState(WitchState.GuardAttack);
                return;
            }
        }
        else
        {
            if (!_witch.DistanceCheck(WitchValueManager.I.AttackDistance[_witch.AttackIdx]))
                return;

            if (DotValue > WitchValueManager.I.AttackMaxAngle[_witch.AttackIdx] ||
                DotValue < WitchValueManager.I.AttackMinAngle[_witch.AttackIdx])
                return;

            if (_witch.Target._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
                _witch.Target.PlayerHp(0.2f, 2, WitchValueManager.I.AttackDamge[_witch.AttackIdx]);
            else
                _witch.Target.PlayerHp(0.2f, 1, WitchValueManager.I.AttackDamge[_witch.AttackIdx]);

            if (_witch.Target._PlayerShild._isShildCounter)
            {
                _witch.SetState(WitchState.GuardAttack);
                return;
            }
        }

        //_witch.Collider.Collider.enabled = true;
        //if (_witch.AttackIdx == 1)
        //    _closeAttackEffect.SetActive(true);
        //if (_witch.DistanceCheck(_witch.Stat.AttackDistance))
        //{
        //    _witch.Target.PlayerHp(0.2f, 2, 10.0f);
        //    if (_witch.Target._PlayerShild._isShildCounter)
        //    {
        //        _witch.Target._PlayerShild.isCounterTimer = true;
        //        _witch.SetState(WitchState.GuardAttack);
        //    }
        //}
    }

    private void EndAttackTime()
    {
        _witch.Collider.Collider.enabled = false;
    }

    private void EndAttackEvent()
    {
        _witch.EndAttack();
        _trailRenderer.SetActive(false);
        _closeAttackEffect.SetActive(false);
    }

    private void TeleportPoint()
    {
        if (!_witch.TelAttack)
            return;

        _witch.CloseTelCheck = true;
        _witch.Anim.speed = 0.0f;
    }

    private void OnRelease()
    {
        if (_witch.State == WitchState.GroggyRelease)
        {
            ((WitchStateGroggyRelease)_witch.StateSystem.CurrentState).OnRelease();
        }
        else if (_witch.State == WitchState.AttackRelease)
        {
            ((WitchStateAttackRelease)_witch.StateSystem.CurrentState).OnRelease();
        }
    }

    private void EndGuardAttack()
    {
        if (_witch.State != WitchState.GuardAttack)
            return;

        ((WitchStateGuardAttack)_witch.StateSystem.CurrentState).EndAnimation();
    }

    private void EndReleaseAnim()
    {
        if (_witch.State == WitchState.GroggyRelease)
        {
            ((WitchStateGroggyRelease)_witch.StateSystem.CurrentState).EndReleaseAnim();
        }
        else if (_witch.State == WitchState.AttackRelease)
        {
            ((WitchStateAttackRelease)_witch.StateSystem.CurrentState).EndReleaseAnim();
        }
    }

    private void EndCasting()
    {
        if (_witch.State == WitchState.MonsterSpawn)
        {
            ((WitchStateMonsterSpawn)_witch.StateSystem.CurrentState).EndSpawn();
        }
    }

    private void OnCasting()
    {
        if (_witch.State == WitchState.MonsterSpawn)
        {
            ((WitchStateMonsterSpawn)_witch.StateSystem.CurrentState).MonsterSpawn();
        }
    }

    private void OnAttack1Effect()
    {
        EffectManager.I.OnEffect(EffectType.Witch_Attack1, _witch.transform, 1.0f);
    }

    private void OnAttack2Effect()
    {
        EffectManager.I.OnEffect(EffectType.Witch_Attack2, _witch.transform, 1.5f);
        //if (_witch.TelAttack)
        //    _witch.Attack2Anim = EffectManager.I.OnEffect(EffectType.Witch_Attack2, _witch.transform, 5.0f).GetComponent<Animator>();
        //else
        //    _witch.Attack2Anim = EffectManager.I.OnEffect(EffectType.Witch_Attack2, _witch.transform, 2.8f).GetComponent<Animator>();
    }

    private void OnAttack3Effect()
    {
        Vector3 pos = _witch.transform.position;
        pos.y -= 1.0f;
        EffectManager.I.OnEffect(EffectType.Witch_Attack3, pos, _witch.transform.rotation, 3.0f, 0);
    }

    private void SpeedSet()
    {
        _witch.Anim.speed = 0.0f;
    }
}
