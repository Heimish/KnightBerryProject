using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGateHandler : MonoBehaviour
{
    private StageGate[] _gates;

    private void Awake()
    {
        _gates = FindObjectsOfType<StageGate>();
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

    public void SetGateState(bool flag)
    {
        SoundManager.I.PlaySound(CPlayerManager._instance.transform, PlaySoundId.Vine_Fast);
        foreach (StageGate g in _gates)
        {
            g.SetGateState(flag);
        }
    }
}
