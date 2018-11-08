using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomisHit : GuardMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0;
        GuardMushroom.isHit = false;
    }

    public override void EndState()
    {
        base.EndState();
        GuardMushroom.AttackTimer = 0.7f;
    }

    void Update()
    {
        GuardMushroom.TurnToHitDestination();
        GuardMushroom.NowisHit();
        GuardMushroom.GoToPullPush();
        GuardMushroom.GetBerserkerMode();

        Dltime += Time.deltaTime;

        if (Dltime > 0.25f)
        {
            if (GuardMushroom.QueenisAllDead)
            {
                GuardMushroom.SetState(GuardMushroomState.BChase);
                return;
            }

            else
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
}
