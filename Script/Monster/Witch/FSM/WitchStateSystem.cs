using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateSystem : MonoBehaviour
{
    private WitchBoss _witch;
    private Dictionary<WitchState, WitchFSMStateBase> _states = new Dictionary<WitchState, WitchFSMStateBase>();

    // properties
    public WitchBoss Witch { get { return _witch; } set { _witch = value; } }
    public Dictionary<WitchState, WitchFSMStateBase> States { get { return _states; } }

    public WitchFSMStateBase CurrentState
    {
        get
        {
            return _states[_witch.State];
        }
    }

    private void Awake()
    {
        _witch = GetComponent<WitchBoss>();
        WitchState[] states = Enum.GetValues(typeof(WitchState)) as WitchState[];
        foreach (WitchState state in states)
        {
            Type t = Type.GetType("WitchState" + state.ToString());
            WitchFSMStateBase s = GetComponent(t) as WitchFSMStateBase;
            if (s == null)
                s = gameObject.AddComponent(t) as WitchFSMStateBase;

            s.Witch = _witch;
            s.enabled = false;
            _states.Add(state, s);
        }

        _states[WitchState.Groggy].enabled = true;
        _states[WitchState.Groggy].BeginState();
    }

    void Start ()
    {
        Witch.Anim.SetInteger("State", (int)WitchState.Groggy);

    }

    void Update ()
    {
		
	}

    public void SetState(WitchState state)
    {
        Witch.Anim.SetInteger("State", (int)state);

        _states[_witch.State].EndState();
        _states[_witch.State].enabled = false;
        _witch.State = state;
        _states[_witch.State].enabled = true;
        _states[_witch.State].BeginState();
    }
}
