using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomHealing : QueenMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0;
        QueenMushroom.Stat.MoveSpeed = 0;
    }

    public override void EndState()
    {
        base.EndState();
        QueenMushroom.HealEffect.SetActive(false);
        QueenMushroom.Stat.MoveSpeed = 3.5f;
    }

    public void HealCheck()
    {
        QueenMushroom.HealStart = true;
        QueenMushroom.HealTimer = 0;
    }

    void Update()
    {
        QueenMushroom.EffectofHeal(transform.position);
        QueenMushroom.GoToPullPush();
        Dltime += Time.deltaTime;

        if (Dltime > 2f)
        {
            QueenMushroom.HealEffect.SetActive(false);
            QueenMushroom.SetState(QueenMushroomState.Chase);
            return;
        }
    }
}
