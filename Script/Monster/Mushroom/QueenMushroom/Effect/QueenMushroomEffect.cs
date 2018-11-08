using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomEffect : MonoBehaviour
{
    public GameObject[] ShildHitEffects;
    public GameObject[] ScytheHitEffects;
    public GameObject PCSwapEffect;
    public GameObject EffectPosition;
    public float[] ShildHitTime;
    public float[] ScytheHitTime;
    public float SwapTime;

    private Vector3 _home;

    public void QueenSwapEffect()
    {
        _home.y += 2.3f;
        PCSwapEffect.transform.position = _home;
        PCSwapEffect.SetActive(true);

        if (SwapTime > 0.7f)
        {
            PCSwapEffect.SetActive(false);
            SwapTime = 0;
        }
    }

    public void QueenSwapCheck()
    {
        if (PCSwapEffect.activeInHierarchy)
            SwapTime += Time.deltaTime;
    }

    public void QueenMHitEffect()
    {
        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
        {
            _home.y += 2f;

            for (int i = 0; i < 3; i++)
            {
                ScytheHitEffects[i].transform.position = ScytheHitEffects[i].transform.position;
            }

            if (CPlayerManager._instance.m_nAttackCombo == 0 ||
                CPlayerManager._instance.m_nAttackCombo == 1)
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
            _home.y -= 1f;
            _home.z += -1f;

            for (int i = 0; i < 5; i++)
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
        for (int i = 0; i < 5; i++)
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

        for (int i = 0; i < 3; i++)
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
    }

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            ScytheHitTime[i] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            ShildHitTime[i] = 0;
        }
        SwapTime = 0;
    }
    void Update()
    {
        _home = transform.position;
        SetHitEffect();
        QueenSwapCheck();
    }
}
