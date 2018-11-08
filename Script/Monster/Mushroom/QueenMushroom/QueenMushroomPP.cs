using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomPP : QueenMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0;
        QueenMushroom.HealTime = false;
        QueenMushroom.HealStart = false;
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        QueenMushroom.ModeChange();
        Dltime += Time.deltaTime;

        if (Dltime > 1.4f)
        {
            CPlayerManager._instance.isPush = false;
            CPlayerManager._instance.isPull = false;
            QueenMushroom.PPEnding = true;

            QueenMushroom.SetState(QueenMushroomState.Return);
            return;
        }
    }
}
