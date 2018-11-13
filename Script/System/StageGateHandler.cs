using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGateHandler : MonoBehaviour
{
    private StageGate[] _gates = new StageGate[4];

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            StageGate obj = transform.GetChild(i).GetComponent<StageGate>();
            if (obj == null)
                continue;

            _gates[i] = obj;
            print(_gates[i].name);
        }
    }

    void Start ()
    {
        foreach (StageGate g in _gates)
        {
            g.SetGateState(true);
        }
    }

    void Update ()
    {
    }

    public void SetGateState(bool flag, int id = 0)
    {
        SoundManager.I.PlaySound(CPlayerManager._instance.transform, PlaySoundId.Vine_Fast);

        if (!flag)
        {
            if (id == 1)
            {
                for (int i = 0; i < _gates.Length; i++)
                {
                    if (i <= 1)
                        continue;

                    _gates[i].SetGateState(flag);
                }
            }
            else
            {
                foreach (StageGate g in _gates)
                {
                    g.SetGateState(flag);
                }
            }
        }
        else
        {
            foreach (StageGate g in _gates)
            {
                g.SetGateState(flag);
            }
        }
    }
}
