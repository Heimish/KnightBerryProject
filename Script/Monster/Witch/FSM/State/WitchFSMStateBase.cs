using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchFSMStateBase : MonoBehaviour
{
    private WitchBoss _witch;

    // properties
    public WitchBoss Witch { get { return _witch; } set { _witch = value; } }

    public virtual void BeginState()
    {

    }

    public virtual void EndState()
    {

    }
}
