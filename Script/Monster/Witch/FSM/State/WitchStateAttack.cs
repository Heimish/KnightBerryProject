using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateAttack : WitchFSMStateBase
{
    private bool _randTel;
    private float _rand;
    public override void BeginState()
    {
        if (!Witch.CloseTelCheck)
        {
            Witch.SetAttack();
        }

        Witch.RotateToTarget(Witch.Target.transform.position);
        Witch.MoveStop();
        _randTel = false;
    }

    void Update()
    {
        if (Witch.TelAttack)
        {
            if (!Witch.CloseTelCheck)
                return;

            Witch.SkillSys.OnTeleport(Witch.Target.transform, 1);
            Witch.TelAttack = false;
            Witch.SetState(WitchState.Skill);
            return;
        }

        if (!Witch.DistanceCheck(Witch.Stat.AttackDistance) && !Witch.IsAttacking)
        {
            Witch.SetState(WitchState.Chase);
            return;
        }

        else if (!Witch.IsAttacking)
        {
            Witch.TelAttack = false;

            //if (Witch.TelAttack)
            //    return;
            _rand = Random.Range(0.0f, 100.0f);

            if (_rand >= 50)
            {
                _randTel = true;

                if (_randTel)
                {
                    Witch.CloseTelCheck = false;
                    Witch.RotateToTarget(Witch.Target.transform.position);
                    Witch.SetAttack();
                    Witch.TelAttack = true;
                    return;
                }

            }
            //else if (_rand >= 50.0f)
            //{
            //    Witch.SetState(WitchState.AttackRelease);
            //    return;
            //}

            Witch.SetAttack();

            Witch.RotateToTarget(Witch.Target.transform.position);
        }
    }

    public override void EndState()
    {
    }
}
