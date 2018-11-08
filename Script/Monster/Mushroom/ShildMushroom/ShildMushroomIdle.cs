using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomIdle : ShildMushroomStateBase
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

        if (ShildMushroom.GetDistanceFromPlayer() < ShildMushroom.Stat.ChaseDistance && (CPlayerManager._instance.isDead == false))
        {
            ShildMushroom.GoToDestination(ShildMushroom.Player.position, ShildMushroom.Stat.MoveSpeed, ShildMushroom.rotAnglePerSecond);
            ShildMushroom.SetState(ShildMushroomState.Chase);
            return;
        }
    }
}
