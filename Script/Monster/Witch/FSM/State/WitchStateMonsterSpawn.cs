using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateMonsterSpawn : WitchFSMStateBase
{
    private bool _spawn;
    public override void BeginState()
    {
        Witch.SkillSys.OnTeleport(transform, 2);
        _spawn = false;
    }

    void Update ()
    {
		if (_spawn)
        {
            Witch.SetState(WitchState.Chase);
            return;
        }
	}

    public override void EndState()
    {
    }

    public void MonsterSpawn()
    {
        ABUGameManager.I.MonsterPhase.OnNextSpawn();
    }

    public void EndSpawn()
    {
        _spawn = true;
    }
}
