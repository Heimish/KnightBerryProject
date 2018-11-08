using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterStatueLaser : MonoBehaviour
{
    private CenterStatue _statue;

    public CenterStatue Statue { get { return _statue; } set { _statue = value; } }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss")
        {
            Statue.IsLaserOn = false;
            Statue.CenterCamera.gameObject.SetActive(false);
            other.GetComponent<WitchBoss>().IsGravity = true;
        }
    }
}
