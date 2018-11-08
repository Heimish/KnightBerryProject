using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomReturn : QueenMushroomStateBase
{
    public float _attackpattern;

    public override void BeginState()
    {
        base.BeginState();
        _attackpattern = Random.Range(0, 10f);
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        QueenMushroom.NowisHit();
        QueenMushroom.TurnToDestination();
        QueenMushroom.GoToPullPush();
        QueenMushroom.PlayerisDead();
        QueenMushroom.TimeToHeal();

        if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.MStat.AttackDistance)
        {
            if (QueenMushroom.AttackTimer > QueenMushroom.AttackDelay && _attackpattern > 4f)
            {
                QueenMushroom.SetState(QueenMushroomState.Attack);
                return;
            }

            else if (QueenMushroom.AttackTimer > QueenMushroom.AttackDelay && _attackpattern < 4f)
            {
                QueenMushroom.SetState(QueenMushroomState.Attack2);
                return;
            }
        }

        if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.MStat.ChaseDistance && QueenMushroom.GetDistanceFromPlayer() > QueenMushroom.MStat.AttackDistance)
        {
            QueenMushroom.SetState(QueenMushroomState.Chase);
            return;
        }
    }
}
