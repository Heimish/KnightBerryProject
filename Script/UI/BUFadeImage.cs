using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUFadeImage : MonoBehaviour
{
    public BossSceneManager Manager;

    private void InitFade()
    {
        Manager.Anim.SetInteger("Fade", 0);
    }

    private void InEvent()
    {
        Manager.Action1();
    }
}
