using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WitchStateDie : WitchFSMStateBase
{
    public override void BeginState()
    {
    }
    void Update()
    {
    }

    public override void EndState()
    {
    }

    private void OnEnding()
    {
        SceneManager.LoadSceneAsync("ending", LoadSceneMode.Single);
    }
}
