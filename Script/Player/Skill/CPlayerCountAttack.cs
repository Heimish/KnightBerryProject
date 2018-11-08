using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCountAttack : CPlayerBase
{
    public bool isCount;

    private void Start()
    {
        isCount = false;
    }
    void Update ()
    {
        CountDirect();
    }

    void CountDirect()
    {
        if (!isCount)
            return;

    }



}
