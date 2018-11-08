using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomEffect : MonoBehaviour {

    private ShildMushroom _shildmushroom;

    public GameObject GroggyEffect;
    public GameObject[] ShildHitEffects;
    public GameObject[] ScytheHitEffects;
    public GameObject EffectPosition;
    public GameObject[] DefenseEffects;

    private Vector3 EffectPo;
    private float RandomeX;
    private float RandomeY;

    public float[] DfEstart;
    public bool[] isDfEstart;
    public float[] ShildHitTime;
    public float[] ScytheHitTime;

    private Vector3 _home;

    public void Groggy()
    {
        if (!_shildmushroom.isDead)
            GroggyEffect.SetActive(true);

        else if (_shildmushroom.isDead)
            GroggyEffect.SetActive(false);
    }

    public IEnumerator DefenseObjectPool()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < DefenseEffects.Length; i++)
        {
            if(DefenseEffects[i].activeInHierarchy == true && DfEstart[i] > 1f)
            {
                DefenseEffects[i].SetActive(false);
                isDfEstart[i] = false;
                DfEstart[i] = 0;
            }
        }
    }

    public void DefenEffect()
    {
        for(int i = 0; i < DefenseEffects.Length; i++)
        {
            StartCoroutine(DefenseObjectPool());

            if (DefenseEffects[i].activeInHierarchy == false)
            {
                EffectPo = EffectPosition.transform.position;
                RandomeX = Random.Range(-0.51f, 0.5f);
                RandomeY = Random.Range(-0.31f, 0.4f);
                EffectPo.x += RandomeX;
                EffectPo.y += RandomeY;

                DefenseEffects[i].transform.position = EffectPo;
                DefenseEffects[i].SetActive(true);
                isDfEstart[i] = true;
                return;
            }
        }
    }

    public void ShildMHitEffect()
    {
        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
        {
            _home.y += 2f;

            for (int i = 0; i < ScytheHitEffects.Length; i++)
            {
                ScytheHitEffects[i].transform.position = ScytheHitEffects[i].transform.position;
            }

            if (CPlayerManager._instance.m_nAttackCombo == 0
                || CPlayerManager._instance.m_nAttackCombo == 1)
            {
                ScytheHitEffects[0].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 2)
            {
                ScytheHitEffects[1].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 3)
            {
                ScytheHitEffects[2].SetActive(true);
            }
        }

        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            _home.y -= 0.5f;
            _home.z += 1.2f;

            for (int i = 0; i < ShildHitEffects.Length; i++)
            {
                ShildHitEffects[i].transform.position = ShildHitEffects[i].transform.position;
            }

            if (CPlayerManager._instance.m_nAttackCombo == 0) 
            {
                ShildHitEffects[0].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 1)
            {
                ShildHitEffects[1].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 2)
            {
                ShildHitEffects[2].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 3)
            {
                ShildHitEffects[3].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 5)
            {
                ShildHitEffects[4].SetActive(true);
            }
        }
    }

    public void SetHitEffect()
    {
        for (int i = 0; i < ShildHitEffects.Length; i++)
        {
            if (ShildHitEffects[i].activeInHierarchy)
            {
                ShildHitTime[i] += Time.deltaTime;
                if (ShildHitTime[i] > 0.7f)
                {
                    ShildHitEffects[i].SetActive(false);
                    ShildHitTime[i] = 0;
                }
            }
        }

        for (int i = 0; i < ScytheHitEffects.Length; i++)
        {
            if (ScytheHitEffects[i].activeInHierarchy)
            {
                ScytheHitTime[i] += Time.deltaTime;
                if (ScytheHitTime[i] > 0.3f)
                {
                    ScytheHitEffects[i].SetActive(false);
                    ScytheHitTime[i] = 0;
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (isDfEstart[i])
                DfEstart[i] += Time.deltaTime;
        }
    }

    private void TimeNvalueSet()
    {
        RandomeX = 0;
        RandomeY = 0;
    }

    void Start()
    {
        _shildmushroom = GetComponent<ShildMushroom>();
        TimeNvalueSet();
    }

    void Update()
    {
        _home = transform.position;
        SetHitEffect();
    }
}
