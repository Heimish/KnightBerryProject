using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomBChase : GuardMushroomStateBase
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
        GuardMushroom.GoToPullPush();
        GuardMushroom.NowisHit();
        GuardMushroom.PlayerisDead();

        GuardMushroom.GoToDestination(GuardMushroom.Player.position, GuardMushroom.BerserkerMoveSpeed, GuardMushroom.rotAnglePerSecond);

        if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.AttackDistance && GuardMushroom.AttackTimer > GuardMushroom.BerserkerAttackDelay)
        {
                GuardMushroom.SetState(GuardMushroomState.BAttack);
                return;
        }
    }
}
