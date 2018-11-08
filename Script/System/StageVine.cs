using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageVine : MonoBehaviour
{
    public float MaxGrowth;
    public float MinGrowth;

    private Material _mat;

    private float _growth;
    public float _upSpeed;
    public float _downSpeed;
    private bool _isOn;

    // properties
    public float Growth { get { return _growth; } }

    private void Awake()
    {
        _mat = GetComponent<MeshRenderer>().material;
        _growth = MaxGrowth;
        _isOn = false;
    }

	void Start ()
    {
	    	
	}
	
	void Update ()
    {
        GrowthOffUpdate();
        GrowthOnUpdate();

    }

    private void GrowthOffUpdate()
    {
        if (_isOn)
            return;

        _growth -= InspectorManager._InspectorManager.VineSpeed * Time.deltaTime;

        if (_growth <= MinGrowth)
            _growth = MinGrowth;

        _mat.SetFloat("_growth", _growth);
    }

    private void GrowthOnUpdate()
    {
        if (!_isOn)
            return;

        _growth += InspectorManager._InspectorManager.VineSpeed * Time.deltaTime;

        if (_growth >= MaxGrowth)
            _growth = MaxGrowth;

        _mat.SetFloat("_growth", _growth);
    }

    public void SetVineState(bool flag)
    {
        _isOn = flag;
    }
}
