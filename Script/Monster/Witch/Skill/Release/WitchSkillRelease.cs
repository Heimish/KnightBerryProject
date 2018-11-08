using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillRelease : MonoBehaviour
{
    private bool _on;
    private SphereCollider _collider;
    private float _time;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _on = false;
        _time = 0.0f;
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!_on)
            return;

        _time += Time.deltaTime;

        if (_time < 0.3f)
            return;

        _collider.enabled = true;

        if (_time < 1.0f)
            return;

        _collider.enabled = false;

        if (_time < 3.6f)
            return;
        
        _time = 0.0f;
        _on = false;
        gameObject.SetActive(false);
	}

    public void OnRelease()
    {
        _collider.enabled = false;
        _on = true;
        _time = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CPlayerManager._instance.PlayerHp(0.4f, 1, WitchValueManager.I.ReleaseDamage);
        }
    }
}
