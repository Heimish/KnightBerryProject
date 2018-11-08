using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomisHit : QueenMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0;
        QueenMushroom.isHit = true;
    }

    public override void EndState()
    {
        base.EndState();
        QueenMushroom.AttackTimer = 1.3f;
    }

    void Update()
    {
        QueenMushroom.TurnToHitDestination();
        QueenMushroom.NowisHit();
        QueenMushroom.GoToPullPush();

        Dltime += Time.deltaTime;

        if (Dltime > 0.25f)
        {
            QueenMushroom.SetState(QueenMushroomState.Return);
            return;
        }
    }
}
