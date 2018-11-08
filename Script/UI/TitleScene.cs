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

    [Header("Anykey")]
    public Image Anyfade;
    float Anyfades = 1f;
    float Anytime = 0;
    bool fadeinout = false;

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

    private void AnyFadeSet()
    {
        if (fadeinout)
        {
            AnyFadeout();
        }

        else if (!fadeinout)
        {
            AnyFadein();
        }
    }

    private void AnyFadein()
    {
        Anytime += Time.deltaTime;
        if (Anyfades > 0.0f && Anytime >= 0.1f)
        {
            Anyfades -= 0.1f;
            Anyfade.color = new Color(255, 255, 255, Anyfades);
            Anytime = 0;
        }

        else if (Anyfades <= 0.0f)
        {
            fadeinout = !fadeinout;
        }
    }

    private void AnyFadeout()
    {
        Anytime += Time.deltaTime;
        if (Anyfades < 1f && Anytime >= 0.1f)
        {
            Anyfades += 0.1f;
            Anyfade.color = new Color(255, 255, 255, Anyfades);
            Anytime = 0;
        }

        else if (Anyfades >= 0.98f)
        {
            fadeinout = !fadeinout;
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
        AnyFadeSet();

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
