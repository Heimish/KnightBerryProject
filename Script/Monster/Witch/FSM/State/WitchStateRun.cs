using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateRun : WitchFSMStateBase
{
    private float _delayTime;
    public override void BeginState()
    {
        _delayTime = 0.0f;
        Witch.CloseTelCheck = false;
        Witch.TelAttack = false;
    }

    void Update()
    {
        if (Witch.DistanceCheck(Witch.Stat.AttackDistance) && !Witch.TelAttack)
        {
            Witch.SetState(WitchState.Attack);
            return;
        }

        if (Witch.CloseTelCheck)
        {
            Witch.SkillSys.OnTeleport(Witch.Target.transform, 1);
            Witch.SetState(WitchState.Skill);
            return;
        }

        if (!Witch.DistanceCheck(Witch.CloseDistance))
        {
            if (!Witch.TelAttack)
            {
                Witch.RotateToTarget(Witch.Target.transform.position);
                Witch.SetAttack();
                Witch.MoveStop();
                Witch.TelAttack = true;
                return;
            }
        }
        if (!Witch.TelAttack)
        {
            Witch.MoveToTargetLookAt(Witch.Target.transform.position);
        }
    }

    public override void EndState()
    {
        Witch.TelAttack = false;
        Witch.MoveStop();
    }
}
