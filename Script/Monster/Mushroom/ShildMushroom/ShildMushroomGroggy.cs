using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomGroggy : ShildMushroomStateBase
{
    Vector3 SavePosition;
    ShildMushroomEffect _groggy;

    public override void BeginState()
    {
        _groggy = GetComponent<ShildMushroomEffect>();
        Dltime = 0f;
        SavePosition = transform.position;
        ShildMushroom.GroggyEnd = false;
    }

    public override void EndState()
    {
        _groggy.GroggyEffect.SetActive(false);
        ShildMushroom.GroggyEnd = true;
        ShildMushroom.Groggy = 0;
    }


    void Update()
    {
        _groggy.Groggy();
        Dltime += Time.deltaTime;
        ShildMushroom.GoToDestination(SavePosition, 0, 0);

        if (Dltime > 7f)
        {
            ShildMushroom.SetState(ShildMushroomState.Return);
            return;
        }
    }
}
