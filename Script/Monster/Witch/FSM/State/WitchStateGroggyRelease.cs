using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateGroggyRelease : WitchFSMStateBase
{
    private GameObject _release;
    private Vector3 _pos;

    public override void BeginState()
    {
        if (_release == null)
            _release = GameObject.Find("WitchSkillRelease");
        _release.SetActive(false);
        _pos = Witch.transform.position;
        Witch.Controller.enabled = false;
    }

    void Update()
    {
    }

    public override void EndState()
    {
        Witch.Controller.enabled = true;
        _release.transform.position = new Vector3(10000.0f, 0.0f, 0.0f);
    }

    public void OnRelease()
    {
        Vector3 pos = Witch.transform.position;
        pos.y = pos.y - 1.12f;
        _release.transform.position = pos;
        _release.SetActive(true);
        _release.GetComponent<WitchSkillRelease>().OnRelease();
        SoundManager.I.PlaySound(transform, PlaySoundId.Boss_Release);
    }

    public void EndReleaseAnim()
    {
        
        Witch.SetState(WitchState.Chase);
        return;
    }
}
