using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateGroggy : WitchFSMStateBase
{
    private float _delayTime;
    private bool _init;

    public override void BeginState()
    {
        if (!_init)
            return;

        _delayTime = 0.0f;
        Witch.Anim.speed = 1.0f;
        Witch.CloseTelCheck = false;
        SoundManager.I.PlaySound(transform, PlaySoundId.Boss_Groggy);
    }

    void Update()
    {
        InitGroggyUpdate();
        NonInitGroggyUpdate();
    }

    public override void EndState()
    {

    }

    private void InitGroggyUpdate()
    {
        if (_init)
            return;

        if (Witch.DistanceCheck(Witch.Stat.ChaseDistance))
        {
            _init = true;
            Witch.SetState(WitchState.GroggyRelease);
            return;
        }
    }

    private void NonInitGroggyUpdate()
    {
        if (!_init)
            return;

        _delayTime += Time.deltaTime;

        if (_delayTime >= WitchValueManager.I.GroggyDuration)
        {
            _delayTime = 0.0f;
            Witch.SetState(WitchState.GroggyRelease);
            return;
        }
    }
}

