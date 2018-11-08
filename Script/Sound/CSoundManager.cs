using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundManager : MonoBehaviour
{
    public static CSoundManager _instance = null;

    private AudioSource AS_Audio;
    public AudioSource _AS_Audio { get { return AS_Audio; } set { value = AS_Audio; } }

    public AudioClip[] AC_Sound;

	void Start ()
    {
        CSoundManager._instance = this;

        AS_Audio = this.gameObject.AddComponent<AudioSource>();
        AS_Audio.clip = this.AC_Sound[AC_Sound.Length - 1];
        AS_Audio.loop = false;
	}
	
    public int PlaySoundType(int type)
    {
        AS_Audio.PlayOneShot(AC_Sound[type]);
        return type;
    }
}
