using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchBossEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _shieldEffect;
    private float[] _shieldEffectTime;

    [SerializeField]
    private GameObject[] _scytheEffect;
    private float[] _scytheEffectTime;

    void Start ()
    {
        _shieldEffectTime = new float[_shieldEffect.Length];
        _scytheEffectTime = new float[_scytheEffect.Length];

        foreach (GameObject obj in _shieldEffect)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in _scytheEffect)
        {
            obj.SetActive(false);
        }
	}
	
	void Update ()
    {
        for (int i = 0; i < _shieldEffect.Length; i++)
        {
            if (!_shieldEffect[i].activeInHierarchy)
                continue;

            _shieldEffectTime[i] += Time.deltaTime;

            if (_shieldEffectTime[i] < 0.5f)
                continue;

            _shieldEffect[i].SetActive(false);
            _shieldEffectTime[i] = 0.0f;
        }

        for (int i = 0; i < _scytheEffect.Length; i++)
        {
            if (!_scytheEffect[i].activeInHierarchy)
                continue;

            _scytheEffectTime[i] += Time.deltaTime;

            if (_scytheEffectTime[i] < 0.5f)
                continue;

            _scytheEffect[i].SetActive(false);
            _scytheEffectTime[i] = 0.0f;
        }
    }

    public void OnScytheEffect(int idx)
    {
        _scytheEffectTime[idx] = 0;
        if (idx > 0)
        {
            _scytheEffect[idx - 1].SetActive(false);
            _scytheEffectTime[idx - 1] = 0.0f;
        }

        if (_scytheEffect[idx].activeInHierarchy)
        {
            StartCoroutine(Co_ScytheOn(idx));
            return;
        }

        _scytheEffect[idx].SetActive(true);
    }

    public void OnShieldEffect(int idx)
    {
        _shieldEffectTime[idx] = 0;
        if (idx > 0)
        {
            _shieldEffect[idx - 1].SetActive(false);
            _shieldEffectTime[idx - 1] = 0.0f;
        }

        if (_shieldEffect[idx].activeInHierarchy)
        {
            StartCoroutine(Co_ShieldOn(idx));
            return;
        }

        _shieldEffect[idx].SetActive(true);
    }

    private IEnumerator Co_ScytheOn(int idx)
    {
        _scytheEffect[idx].SetActive(false);
        yield return new WaitForFixedUpdate();
        _scytheEffect[idx].SetActive(true);
    }

    private IEnumerator Co_ShieldOn(int idx)
    {
        _shieldEffect[idx].SetActive(false);
        yield return new WaitForFixedUpdate();
        _shieldEffect[idx].SetActive(true);
    }
}
