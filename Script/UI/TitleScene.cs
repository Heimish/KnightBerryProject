using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour {

    [Header("Title")]
    public Image fade;
    float fades = 1.0f;
    float time = 0;
    float SoundVolume = 0;
    float _stack = 0;
    bool isinputanykey = false;
    bool fadeinfinish = false;

    AudioSource TitleAudio;

    private void TitleStartFadein()
    {
        if (!fadeinfinish)
        {
            time += Time.deltaTime;
            if (fades >= 0f && time >= 0.1f)
            {
                SoundVolume += 0.01f;
                fades -= 0.1f;
                fade.color = new Color(0, 0, 0, fades);
                time = 0;
            }

            if (fades <= 0f)
            {
                SoundVolume = 0;
                fadeinfinish = true;
            }
        }
    }

    private void TitleExitFadeOut()
    {
        if (isinputanykey)
        {
            time += Time.deltaTime;
            if (fades <= 1f && time >= 0.1f)
            {
                SoundVolume += 0.01f;
                fades += 0.1f;
                fade.color = new Color(0, 0, 0, fades);
                time = 0;
            }

            if (fades >= 0.91f && _stack == 0)
            {
                _stack++;
                SceneManager.LoadSceneAsync("Intro");
            }
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        TitleAudio = GetComponent<AudioSource>();
        TitleAudio.Play();
        TitleAudio.volume = 0;
    }

    void FixedUpdate()
    {
        // Fade in / Out Set
        TitleStartFadein();
        TitleExitFadeOut();

        // SoundVolume
        if (!isinputanykey)
            TitleAudio.volume += SoundVolume;

        else if (isinputanykey)
            TitleAudio.volume -= SoundVolume;
    }

    void Update ()
    {
        if (Input.anyKey)
        {
            isinputanykey = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
