using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomAttack : ShildMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0f;
    }

    public override void EndState()
    {
        base.EndState();
        ShildMushroom.AttackTimer = 0f;
    }

    public void ShildAttackCheck()
    {
        if (ShildMushroom.GetDistanceFromPlayer() < ShildMushroom.Stat.AttackDistance + 2f
            && ShildMushroom.PlayerisFront)
        { 
            if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, ShildMushroom.AttackDamage);
                SoundManager.I.PlaySound(transform, PlaySoundId.ShildGoblin_AttackHit);
            }

            else
            {
                CPlayerManager._instance.PlayerHp(0.2f, 1, ShildMushroom.AttackDamage);
                SoundManager.I.PlaySound(transform, PlaySoundId.ShildGoblin_AttackHit);
            }
        }
    }

    public void ShildSwordSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.ShildGoblin_Sword);
    }

    void Update()
    {
        ShildMushroom.TurnToDestination();
        ShildMushroom.GroggyCheck();
        ShildMushroom.PlayerisDead();
        Dltime += Time.deltaTime;

        if (Dltime > 1.4f)
        {
            if (ShildMushroom.GetDistanceFromPlayer() > ShildMushroom.Stat.AttackDistance)
            {
                ShildMushroom.SetState(ShildMushroomState.Chase);
                return;
            }

            else
            {
                ShildMushroom.SetState(ShildMushroomState.Return);
                return;
            }
        }
    }
}
