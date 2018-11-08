using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateGuardAttack : WitchFSMStateBase
{
    private bool _endAnim;
    public override void BeginState()
    {
        _endAnim = false;
    }

    void Update()
    {
        if (!_endAnim)
            return;

        if (Witch.DistanceCheck(Witch.Stat.AttackDistance))
        {
            Witch.SetState(WitchState.Attack);
            return;
        }
        else
        {
            Witch.SetState(WitchState.Chase);
            return;
        }
    }

    public override void EndState()
    {
    }

    public void EndAnimation()
    {
        _endAnim = true;
    }
}

