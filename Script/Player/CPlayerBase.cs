using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerBase : MonoBehaviour
{
    [HideInInspector]
    public CPlayerManager _PlayerManager;

    void Awake()
    {
        _PlayerManager = GetComponent<CPlayerManager>();
    }

}

