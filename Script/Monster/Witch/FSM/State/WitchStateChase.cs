using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateChase : WitchFSMStateBase
{
    private bool _teleport = false;
    private float _teleportTime;

    public override void BeginState()
    {
        Witch.FootholdOnSkill();
    }

    void Update()
    {

        if (_teleport)
        {
            _teleportTime += Time.deltaTime;

            if (_teleportTime >= 1.0f)
            {
                Witch.SkillSys.OnSkill(Witch.Target.transform);
                Witch.SetState(WitchState.Skill);
                _teleport = false;
                return;
            }
        }
        else
        {
            //if (Witch.ReceiveDamage2 / Witch.Stat.MaxHp >= 0.2f)
            //{
            //    if (!Witch.On60)
            //    {
            //        print("60");
            //        ABUGameManager.I.MonsterPhase.OnNextSpawn();
            //        Witch.On60 = true;
            //    }

            //    if (Witch.ReceiveDamage2 / Witch.Stat.MaxHp >= 0.4f)
            //    {
            //        if (!Witch.On80)
            //        {
            //            print("80");
            //            ABUGameManager.I.MonsterPhase.OnNextSpawn();
            //            Witch.On80 = true;
            //        }
            //    }
            //    //Witch.ReceiveDamage = 0.0f;
            //    //Witch.SetState(WitchState.MonsterSpawn);
            //}

            if (Witch.ReceiveDamage >= WitchValueManager.I.TeleportDamage)
            {
                _teleport = true;
                Witch.SkillSys.OnTeleport(Witch.Target.transform, 0);
                Witch.ReceiveDamage = 0.0f;
                Witch.SetState(WitchState.Skill);
                _teleportTime = 0.0f;
                return;
            }

            if (!Witch.DistanceCheck(Witch.Stat.AttackDistance))
            {
                Witch.SetState(WitchState.Run);
                return;
            }
            else
            {
                Witch.SetState(WitchState.Attack);
                return;
            }
        }
        //    if (Vector3.Distance(Witch.Target.transform.position,
        //transform.position) > 15.0f)
        //        return;

        //    if (Witch.SkillSys.CurrentSkill != null)
        //    {
        //        if (!Witch.SkillSys.CurrentSkill.IsOn)
        //        {
        //            _delayTime += Time.deltaTime;

        //            if (_delayTime < 1.5f)
        //                return;

        //            Witch.SkillSys.OnSkill(Witch.Target.transform);
        //            Witch.SetState(WitchState.Skill);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        Witch.SkillSys.OnSkill(Witch.Target.transform);
        //        Witch.SetState(WitchState.Skill);
        //        return;
        //    }
    }

    public override void EndState()
    {
    }
}
