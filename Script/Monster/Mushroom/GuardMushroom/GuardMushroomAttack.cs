using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardMushroomAttack : GuardMushroomStateBase
{

    public override void BeginState()
    {
        Dltime = 0f;
        GuardMushroom.RADelay = Random.Range(1.2f, 1.3f);
    }

    public override void EndState()
    {
        base.EndState();
        GuardMushroom.AttackTimer = 0f;
        GuardMushroom.AttackDelay = GuardMushroom.RADelay;
    }

    public void AttackCheck()
    {
        if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.AttackDistance + 2f
            && GuardMushroom.PlayerisFront)
        {
            if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, GuardMushroom.AttackDamage);
                SoundManager.I.PlaySound(transform, PlaySoundId.GuardGoblin_AttackHit);
            }

            else
            {
                CPlayerManager._instance.PlayerHp(0.2f, 1, GuardMushroom.AttackDamage);
                SoundManager.I.PlaySound(transform, PlaySoundId.GuardGoblin_AttackHit);
            }
        }
    }

    public void GuardSwordSound()
    {
        SoundManager.I.PlaySound(transform, PlaySoundId.GuardGoblin_Sword);
    }

    void Update()
    {
        GuardMushroom.NowisHit();
        GuardMushroom.GoToPullPush();
        GuardMushroom.PlayerisDead();
        GuardMushroom.TurnToDestination();
        GuardMushroom.GetBerserkerMode();

        Dltime += Time.deltaTime;

        if (Dltime > 1.2f)
        {
            if (GuardMushroom.GetDistanceFromPlayer() > GuardMushroom.MStat.AttackDistance)
            {
                GuardMushroom.SetState(GuardMushroomState.Chase);
                return;
            }

            else
            {
                GuardMushroom.SetState(GuardMushroomState.Return);
                return;
            }
        }
    }
}
