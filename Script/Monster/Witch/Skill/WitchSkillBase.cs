using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillBase
{
    protected WitchBoss _witch;
    protected float _distance;
    protected bool _isOn;

    // properties
    public WitchBoss Witch { get { return _witch; } }
    public float Distance { get { return _distance; } }
    public bool IsOn { get { return _isOn; } set { _isOn = value; } }

    public WitchSkillBase() {   }
    public WitchSkillBase(WitchBoss witch)
    {
        _witch = witch;
    }

    public virtual void OnSkill(Transform target)
    {

    }

    public virtual void OnSkill(Transform target, int type = 0)
    {

    }
}
