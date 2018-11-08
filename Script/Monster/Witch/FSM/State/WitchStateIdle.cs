using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateIdle : WitchFSMStateBase
{
    private float _distance;

    public override void BeginState()
    {
    }

    void Update ()
    {
        if (Witch.Target.isDead)
            return;

		if (Witch.DistanceCheck(15.0f))
        {

            Witch.SetState(WitchState.Chase);
            return;
        }
	}

    public override void EndState()
    {
    }
}
