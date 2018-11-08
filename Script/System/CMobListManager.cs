using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMobListManager : MonoBehaviour
{
    public List<GameObject> _GuardMobList = new List<GameObject>();
    public List<GameObject> _QueenMobList = new List<GameObject>();

    private int nMobNumber = 0;
    private float fSpeed = 3.0f;

    GameObject[] guardObj;
    GameObject[] queenObj;

    private float times;
    void Start ()
    {
        times = 0;
    }

    void Update()
    {
        MobSpawn();
        if (Input.GetKey(KeyCode.Q))
        {
             if(CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
                CCameraShake._instance.shake = 0.8f;
        }

        if (nMobNumber != 0)
            asdf();
    }

    void asdf()
    {
        Vector3 vPlayer = CPlayerManager._instance.transform.position;
        Vector3 vPlayerRot = CPlayerManager._instance.transform.rotation.eulerAngles;

        for (int i = 0; i < guardObj.Length; i++)
        {
            for (int q = 0; q < queenObj.Length; q++)
            {
                if (nMobNumber == 1)
                {
                    if (Vector3.Distance(vPlayer, guardObj[i].transform.position) < 10 ||
                        Vector3.Distance(vPlayer, queenObj[q].transform.position) < 10)
                    {
                        if (Input.GetKeyDown(KeyCode.Q) && CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
                        {
                            nMobNumber = 2;
                        }
                    }
                }
                if (nMobNumber == 2)
                {
                    guardObj[i].transform.position += guardObj[i].transform.forward * Time.deltaTime * fSpeed;
                    queenObj[q].transform.position += queenObj[q].transform.forward * Time.deltaTime * fSpeed;
                    if (Vector3.Distance(vPlayer, guardObj[i].transform.position) < 5 &&
                        Vector3.Distance(vPlayer, queenObj[q].transform.position) < 5)
                    {
                        fSpeed = 0;
                        nMobNumber = 3;
                    }
                }
                if (nMobNumber == 3)
                {
                    times += Time.deltaTime;
                    TimeScalManager._instance.TimeScal(times);
                    if (times >= 0.05f)
                    {
                        TimeScalManager._instance.TimeScal(times);
                    }
                    else if (times >= 0.1f)
                    {
                        TimeScalManager._instance.TimeScal(times);
                        
                    }

                    if (times >= 0.3f)
                    {
                        TimeScalManager._instance.TimeScal(1);
                        fSpeed = 0;
                        nMobNumber = 4;
                    }
                }
                if(nMobNumber == 4)
                {
                    if (Input.GetKeyDown(KeyCode.Q) && CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
                    {
                        times = 0.0f;
                        nMobNumber = 5;
                    }
                }
                if(nMobNumber == 5)
                {
                    fSpeed = 10;
                    guardObj[i].transform.position -= guardObj[i].transform.forward * Time.deltaTime * fSpeed;
                    queenObj[q].transform.position -= queenObj[q].transform.forward * Time.deltaTime * fSpeed;
                    times += Time.deltaTime;

                    
                    if (times > 2.0f)
                    {
                        fSpeed = 3;
                        nMobNumber = 0;
                        times = 0.0f;
                    }
                }
            }
        }
    }

    void MobSpawn()
    {
        if (nMobNumber == 2)
            return;

        if (nMobNumber == 0)
        {
            nMobNumber = 1;
        }
        if(nMobNumber == 1)
        {
            guardObj = GameObject.FindGameObjectsWithTag("Guard");
            queenObj = GameObject.FindGameObjectsWithTag("Queen");

            for (int i = 0; i < guardObj.Length; i++)
                _GuardMobList.Add(guardObj[i]);
            for (int i = 0; i < queenObj.Length; i++)
                _QueenMobList.Add(queenObj[i]);

        }
    }
}
