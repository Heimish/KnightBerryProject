using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomReturn : ShildMushroomStateBase
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
        ShildMushroom.GroggyCheck();
        ShildMushroom.TurnToDestination();
        ShildMushroom.PlayerisDead();

        if (ShildMushroom.GetDistanceFromPlayer() < ShildMushroom.Stat.AttackDistance && ShildMushroom.AttackTimer > ShildMushroom.AttackDelay)
        {
                ShildMushroom.SetState(ShildMushroomState.Attack);
                return;
        }

        if (ShildMushroom.GetDistanceFromPlayer() < ShildMushroom.Stat.ChaseDistance && ShildMushroom.GetDistanceFromPlayer() > ShildMushroom.Stat.AttackDistance)
        {
            ShildMushroom.SetState(ShildMushroomState.Chase);
            return;
        }
    }
}
