using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomReturn : GuardMushroomStateBase
{
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        GuardMushroom.NowisHit();
        GuardMushroom.TurnToDestination();
        GuardMushroom.GoToPullPush();
        GuardMushroom.PlayerisDead();
        GuardMushroom.GetBerserkerMode();

        if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.AttackDistance && GuardMushroom.AttackTimer > GuardMushroom.AttackDelay)
        {
            GuardMushroom.SetState(GuardMushroomState.Attack);
            return;
        }

        else if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.ChaseDistance && GuardMushroom.GetDistanceFromPlayer() > GuardMushroom.MStat.AttackDistance)
        {
            GuardMushroom.SetState(GuardMushroomState.Chase);
            return;
        }
    }
}
