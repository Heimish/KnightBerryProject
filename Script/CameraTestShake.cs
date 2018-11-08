using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestShake : MonoBehaviour
{
    public float ShakeTime = 0.02f;
    public float ShakeDuration = 1.0f;

    private Vector3 _initPos;
    private bool _flag;
    [SerializeField]
    private float _power = 2.0f;
    private float _time;
    private float _time2;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Update_Shake();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _flag = true;
            _initPos = transform.position;
            _time = ShakeDuration;
        }
	}

    private void Update_Shake()
    {
        if (!_flag)
            return;

        ShakeUpdate();

        _time -= Time.deltaTime;

        if (_time <= 0.0f)
        {
            _flag = false;
        }
    }

    private void ShakeUpdate()
    {
        _time2 += Time.deltaTime;

        if (_time2 < ShakeTime)
            return;

        transform.position = _initPos + Random.insideUnitSphere * _power;

        _time2 = 0.0f;
    }
}
