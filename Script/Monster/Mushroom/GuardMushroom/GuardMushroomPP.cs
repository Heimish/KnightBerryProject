using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomPP : GuardMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0;
    }

    public override void EndState()
    {
        base.EndState();
        GuardMushroom.AttackTimer = 0;
    }

    void Update()
    {
        GuardMushroom.ModeChange();
        Dltime += Time.deltaTime;

        if (Dltime > 1.4f)
        {
            CPlayerManager._instance.isPush = false;
            CPlayerManager._instance.isPull = false;
            GuardMushroom.PPEnding = true;

            if (GuardMushroom.QueenisAllDead)
            {
                GuardMushroom.SetState(GuardMushroomState.BChase);
                return;
            }

            else if (!GuardMushroom.QueenisAllDead)
            {
                GuardMushroom.SetState(GuardMushroomState.Return);
                return;
            }
        }
    }
}
