using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomChase : GuardMushroomStateBase
{
    public override void BeginState()
    {
        base.BeginState();
        GuardMushroom.GotoBerserker = true;
        GuardMushroom.AttackTStart = true;
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        GuardMushroom.NowisHit();
        GuardMushroom.GoToPullPush();
        GuardMushroom.ModeChange();
        GuardMushroom.PlayerisDead();
        GuardMushroom.GetBerserkerMode();

        GuardMushroom.GoToDestination(GuardMushroom.Player.position, GuardMushroom.MStat.MoveSpeed, GuardMushroom.rotAnglePerSecond);

        if (GuardMushroom.GetDistanceFromPlayer() <= GuardMushroom.MStat.AttackDistance && GuardMushroom.AttackTimer > GuardMushroom.AttackDelay)
        {
                GuardMushroom.SetState(GuardMushroomState.Attack);
                return;
        }

        else if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.AttackDistance && GuardMushroom.AttackTimer < GuardMushroom.AttackDelay)
        {
            GuardMushroom.SetState(GuardMushroomState.Return);
            return;
        }
    }
}
