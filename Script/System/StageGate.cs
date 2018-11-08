using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGate : MonoBehaviour
{
    private StageVine[] _vines;
    private BoxCollider _collider;
    private bool _isOn;

    private void Awake()
    {
        _vines = transform.GetComponentsInChildren<StageVine>();
        _collider = GetComponent<BoxCollider>();

        _isOn = false;
    }

    void Start ()
    {
	    	
	}
	
	void Update ()
    {
        GateOffUpdate();
        GateOnUpdate();
    }

    private void GateOffUpdate()
    {
        if (_isOn)
            return;

        for (int i = 0; i < _vines.Length; i++)
        {
            if (i == 0)
            {
                _vines[i].SetVineState(false);
                continue;
            }

            if (_vines[i - 1].Growth > (_vines[i - 1].MaxGrowth / 2))
                continue;

            _vines[i].SetVineState(false);
        }
    }

    private void GateOnUpdate()
    {
        if (!_isOn)
            return;

        for (int i = 0; i < _vines.Length; i++)
        {
            if (i == 0)
            {
                _vines[i].SetVineState(true);
                continue;
            }

            if (_vines[i - 1].Growth < (_vines[i - 1].MaxGrowth / 2))
                continue;

            _vines[i].SetVineState(true);
        }
    }

    public void SetGateState(bool flag)
    {
        _collider.enabled = flag;
        _isOn = flag;
    }
}
