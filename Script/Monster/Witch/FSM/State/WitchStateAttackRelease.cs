using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateAttackRelease : WitchFSMStateBase
{
    private GameObject _release;

    public override void BeginState()
    {
        //if (_release == null)
        //    _release = GameObject.Find("WitchSkillRelease");

        //_release.SetActive(false);
    }

    void Update()
    {
    }

    public override void EndState()
    {
        //_release.transform.position = new Vector3(10000.0f, 0.0f, 0.0f);
    }

    public void OnRelease()
    {
        //Vector3 pos = Witch.transform.position;
        //_release.transform.position = pos;
        //_release.SetActive(true);
        //SoundManager.I.PlaySound(transform, PlaySoundId.Boss_Release);
    }

    public void EndReleaseAnim()
    {
        //Witch.SetState(WitchState.Chase);
        //return;
    }
}
