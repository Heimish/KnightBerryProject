using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomChase : ShildMushroomStateBase
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
        ShildMushroom.PlayerisDead();

        ShildMushroom.GoToDestination(ShildMushroom.Player.position, ShildMushroom.Stat.MoveSpeed, ShildMushroom.rotAnglePerSecond);

        if (ShildMushroom.GetDistanceFromPlayer() < ShildMushroom.Stat.AttackDistance)
        {
            ShildMushroom.GoToDestination(ShildMushroom.Player.position, 0, ShildMushroom.rotAnglePerSecond);
            if (ShildMushroom.AttackTimer > ShildMushroom.AttackDelay)
            {
                ShildMushroom.SetState(ShildMushroomState.Attack);
                return;
            }
        }

        if (ShildMushroom.AttackTimer < ShildMushroom.AttackDelay && ShildMushroom.GetDistanceFromPlayer() <= ShildMushroom.Stat.AttackDistance)
        {
            ShildMushroom.SetState(ShildMushroomState.Return);
            return;
        }
    }
}
