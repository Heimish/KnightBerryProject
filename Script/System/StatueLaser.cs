using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueLaser : MonoBehaviour
{
    private StatueObject _statue;
    private bool _laserEnd;
    private float _laserTime;

    // properties
    public StatueObject Statue { get { return _statue; } set { _statue = value; } }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!_laserEnd)
            return;
        _laserTime += Time.deltaTime;

        if (_laserTime < 2.0f)
            return;

        _statue.EndCamera();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "StatueObject")
            return;
        if (other.name != "StatueCenter")
            return;

        other.GetComponent<CenterStatue>().LaserAdd();
        _statue.LaserCrash();
        _laserEnd = true;
    }
}
