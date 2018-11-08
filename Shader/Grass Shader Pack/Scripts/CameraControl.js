#pragma strict

import UnityStandardAssets.ImageEffects;

	var scale : float = 1;
	var maxDist : float = 10;
	var minDist : float = 0.5;
	var cam : Camera;

	function Update () 
	{
	
		/*var hit : RaycastHit;
		var ray = Physics.Raycast (cam.transform.position, transform.TransformDirection (Vector3.forward), hit);*/
	
		if (Input.GetAxis ("Fire2"))
		{
		    transform.Rotate (0, Input.GetAxis ("Mouse X") * 3, 0, Space.World);
			transform.Rotate (Vector3.left * Input.GetAxis ("Mouse Y") * 3);
		}
		
		/*if (scale >= 0.5)
		{
			scale -= Input.GetAxis ("Scroll") * Mathf.Pow (scale - 0.4, 2);
		}
		else
		{
			scale = 0.5;
		}*/
		
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && scale > minDist)
		{
			scale -= 0.1 * Mathf.Pow (scale - scale * 0.5, 2);
		}
		else if (Input.GetAxis ("Mouse ScrollWheel") < 0 && scale < maxDist)
		{
			scale += 0.1 * Mathf.Pow (scale - scale * 0.5, 2);
		}
		
		transform.localScale = Vector3 (1, 1, scale);
		
	}