using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomChase : QueenMushroomStateBase
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
        QueenMushroom.GoToPullPush();
        QueenMushroom.PlayerisDead();
        QueenMushroom.TimeToHeal();

        QueenMushroom.GoToDestination(QueenMushroom.Player.position, QueenMushroom.MStat.MoveSpeed, QueenMushroom.rotAnglePerSecond);

        if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.MStat.AttackDistance && QueenMushroom.AttackTimer > QueenMushroom.AttackDelay)
        {
            if (QueenMushroom.AttackTimer > QueenMushroom.AttackDelay && _attackpattern > 4f)
            {
                QueenMushroom.SetState(QueenMushroomState.Attack);
                return;
            }

            else if(QueenMushroom.AttackTimer > QueenMushroom.AttackDelay && _attackpattern < 4f)
            {
                QueenMushroom.SetState(QueenMushroomState.Attack2);
                return;
            }
        }

        else if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.MStat.AttackDistance && QueenMushroom.AttackTimer < QueenMushroom.AttackDelay)
        {
            QueenMushroom.SetState(QueenMushroomState.Return);
            return;
        }
    }
}
