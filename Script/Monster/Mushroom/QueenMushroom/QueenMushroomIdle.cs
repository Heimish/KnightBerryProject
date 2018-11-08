using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomIdle : QueenMushroomStateBase
{
    public override void BeginState()
    {
        CPlayerManager._instance.isPush = false;
        CPlayerManager._instance.isPull = false;
    }

    public override void EndState()
    {
        CPlayerManager._instance.isPush = false;
        CPlayerManager._instance.isPull = false;
    }

    void Start()
    {
        QueenMushroom.AttackTimer = 0f;
    }

    void Update()
    {
        QueenMushroom.NowisHit();
        QueenMushroom.GoToPullPush();

        if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.Stat.ChaseDistance && CPlayerManager._instance.isDead == false)
        {
            QueenMushroom.GoToDestination(QueenMushroom.Player.position, QueenMushroom.Stat.MoveSpeed, QueenMushroom.rotAnglePerSecond);
            QueenMushroom.SetState(QueenMushroomState.Chase);
            return;
        }
    }
}
