using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomBerserker : GuardMushroomStateBase
{
    GuardMushroomEffect _guardeffect;
    Vector3 SavePosition;
    private int RandomeMode;

    public override void BeginState()
    {
        base.BeginState();
        RandomeMode = Random.Range(0, 2);
        _guardeffect = GetComponent<GuardMushroomEffect>();      
        SavePosition = transform.position;
        Dltime = 0;

        if (GuardMushroom.imsi)
        _guardeffect.BerserkerModeEffect(transform.position);
    }

    public override void EndState()
    {
        base.EndState();
        GuardMushroom.imsi = false;
        GuardMushroom.ifEndBerserker = true;
    }

    void Update()
    {
        Dltime += Time.deltaTime;
        GuardMushroom.GoToDestination(SavePosition, 0, 0);

        if (Dltime > 1.85f)
        {
            GuardMushroom.ifEndBerserker = true;
            if (RandomeMode <= 2)
            {
                GuardMushroom.QueenisAllDead = true;
                GuardMushroom.SetState(GuardMushroomState.BChase);
                return;
            }

            else
            {
                GuardMushroom.SetState(GuardMushroomState.Sbombing);
                return;
            }
        }
    }
}
