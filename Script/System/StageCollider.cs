using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCollider : MonoBehaviour
{
    private StageGateHandler _handler;
    private bool _clear;

    private void Awake()
    {
        _handler = transform.parent.GetComponent<StageGateHandler>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_clear)
                return;

            _handler.SetGateState(true);
            MonsterWaveManager.I.AddStage();
            MonsterWaveManager.I.StartWave();

            SetClear();
        }
    }

    public void SetClear()
    {
        _clear = true;
    }
}
