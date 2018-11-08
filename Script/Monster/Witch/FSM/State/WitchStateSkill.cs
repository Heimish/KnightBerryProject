using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateSkill : WitchFSMStateBase
{
    public override void BeginState()
    {
        Witch.MoveStop();
    }

    void Update()
    {
        if (!Witch.SkillSys.CurrentSkill.IsOn)
        {
            Witch.Anim.speed = 1.0f;
            if (Witch.CloseTelCheck)
            {
                Witch.RotateToTarget(Witch.Target.transform.position);
                Witch.SetState(WitchState.Attack);
                return;
            }

            else
            {
                if (Witch.DistanceCheck(Witch.Stat.AttackDistance))
                {
                    Witch.SetState(WitchState.Attack);
                    return;
                }
            }

            Witch.SetState(WitchState.Chase);
            return;
        }
    }

    public override void EndState()
    {
    }
}
