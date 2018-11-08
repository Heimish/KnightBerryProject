using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodTest : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string _path;

	void Start ()
    {
		
	}
	
	void Update ()
    {
	    if (Input.GetMouseButtonDown(0))
        {
            FMODUnity.RuntimeManager.PlayOneShot(_path, transform.position);
        }
	}
}
