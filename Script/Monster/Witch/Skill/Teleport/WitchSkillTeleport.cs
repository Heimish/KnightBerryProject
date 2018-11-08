using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillTeleport : WitchSkillBase
{
    private WitchSkillTeleportObject _obj;
    private int _type;

    // properties
    public int Type { get { return _type; } }

    public WitchSkillTeleport(WitchBoss witch, float distance) : base(witch)
    {
        _distance = distance;
        _obj = GameObject.FindObjectOfType<WitchSkillTeleportObject>();
        _obj.gameObject.SetActive(false);
        _obj.Init(this);
    }

    public override void OnSkill(Transform target, int type = 0)
    {
        _obj.gameObject.SetActive(true);
        _obj.OnSkill(target, type);
    }
}
