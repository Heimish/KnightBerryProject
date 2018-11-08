using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillArrow : WitchSkillBase
{
    private WitchSkillArrowObject _obj;

    public WitchSkillArrow(WitchBoss witch, float distance) : base(witch)
    {
        _distance = distance;
        _obj = GameObject.FindObjectOfType<WitchSkillArrowObject>();
        _obj.gameObject.SetActive(false);
        _obj.Init(this);
    }

    public override void OnSkill(Transform target)
    {
        _obj.gameObject.SetActive(true);
        _obj.OnSkill(target);
    }
}
