using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHit : MonoBehaviour
{
    private PlayerBase _player;
    private Transform[] _hitPoint;
    private bool _isChecking;
    private Vector3[] _oldPos;
    private List<TestMonster> _hitMonsers = new List<TestMonster>();

    private void Awake()
    {
        _player = transform.root.GetComponent<PlayerBase>();
        _hitPoint = new Transform[transform.childCount];

        int idx = 0;
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t == transform)
                continue;

            _hitPoint[idx] = t;
            idx++;
        }

        _oldPos = new Vector3[_hitPoint.Length];

        _isChecking = false;
    }

    void Start ()
    {

		
	}
	
	void Update ()
    {
        Update_Hit();

    }

    private void Update_Hit()
    {
        if (!_isChecking)
            return;

        for (int i = 0; i < _oldPos.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Linecast(_oldPos[i], _hitPoint[i].position, out hit))
            {
                TestMonster monster = hit.transform.GetComponent<TestMonster>();

                if (monster == null)
                    return;

                if (_hitMonsers.Contains(monster))
                    return;

                _hitMonsers.Add(monster);
            }
        }
    }

    public void OnHitCheck()
    {
        _isChecking = true;

        _hitMonsers.Clear();

        for (int i = 0; i < _oldPos.Length; i++)
        {
            _oldPos[i] = _hitPoint[i].position;
        }
    }

    public void OffHitCheck()
    {
        _isChecking = false;

        foreach (TestMonster m in _hitMonsers)
        {
            m.ReceiveDamage();
        }
    }
}
