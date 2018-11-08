using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainSound : MonoBehaviour {

    [FMODUnity.EventRef]
    public string[] MyEvent1;

    FMOD.Studio.EventInstance bgmSound;
    FMOD.Studio.ParameterInstance bgmVolume;

    public float _Volume;

    void Awake()
    {
        //_Volume = 1;
        //SoundPlay(GetComponent<Transform>(), 0);
    }
    private void Update()
    {
        bgmVolume.setValue(_Volume);
    }

    public void SoundPlay(Transform Target, int SoundType)
    {
        bgmSound = FMODUnity.RuntimeManager.CreateInstance(MyEvent1[SoundType]);
        bgmSound.getParameter("Parameter 1", out bgmVolume);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bgmSound, Target, GetComponent<Rigidbody>());
        bgmSound.start();
    }
}
