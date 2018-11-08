using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    private float _maxHp = 100;
    private float _hp;

	void Start ()
    {
        _hp = _maxHp;
	}
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Z))
        {
            ReceiveDamage();
        }
	}

    public void ReceiveDamage()
    {
        _hp -= 10.0f;
        StartCoroutine(Co_DamageMaterial());
        print("AAAA");
    }

    private IEnumerator Co_DamageMaterial()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color = Color.white;
    }
}
