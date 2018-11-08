using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour {

    public VideoClip movie;
    public Camera camera;
    public Image Set;
    private RawImage Image;
    private VideoPlayer IntroMovie;
    private AudioSource IntroAudio;

    [Header("Skip")]
    public GameObject ifSkip;
    public Image fade;
    public bool Skip = false;
    private float fades = 1f;
    private float time = 0;
    private float SoundVolume = 0;
    private float _stack = 0;
    private bool fadeinfinish = false;

    void Awake()
    {
        Set.enabled = true;
        Image = GetComponent<RawImage>();
        IntroMovie = gameObject.AddComponent<VideoPlayer>();
        IntroAudio = gameObject.AddComponent<AudioSource>();
        IntroMovie.playOnAwake = false;
        //GetComponent<AudioSource>().playOnAwake = false;
        //GetComponent<AudioSource>().Pause();
        PlayVideo();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        IntroAudio.Play();
        IntroAudio.volume = 0f;
    }

    public void FadeInSet()
    {
        if (!fadeinfinish)
        {
            time += Time.deltaTime;
            if (fades >= 0f && time >= 0.1f)
            {
                IntroAudio.volume += 0.1f;
                fades -= 0.06f;
                fade.color = new Color(0, 0, 0, fades);
                time = 0;

                if (fades <= 0f)
                {
                    IntroAudio.volume = 1f;
                    fadeinfinish = true;
                }
            }
        }
    }

    public void FadeOutSet()
    {
        if (Skip)
        {
            time += Time.deltaTime;
            if (fades <= 1f && time >= 0.1f)
            {
                IntroAudio.volume -= 0.1f;
                fades += 0.1f;
                fade.color = new Color(0, 0, 0, fades);
                time = 0;
            }

            else if (fades >= 0.98f && _stack == 0)
            {
                _stack++;
                IntroAudio.volume = 0;
                SceneManager.LoadSceneAsync("NYRA");
            }
        }
    }

    public void PlayVideo()

    {
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()

    {
        IntroMovie.source = VideoSource.VideoClip;
        IntroMovie.clip = movie;
        IntroMovie.renderMode = VideoRenderMode.CameraNearPlane;
        IntroMovie.targetCamera = camera;
        IntroMovie.Prepare();
        WaitForSeconds waitTime = new WaitForSeconds(1.0f);
        while (!IntroMovie.isPrepared)
        {
            Debug.Log("동영상 준비중...");
            yield return waitTime;
        }
        Debug.Log("동영상이 준비가 끝났습니다.");
        //Image.texture = IntroMovie.texture;
        IntroMovie.Play();
        Set.enabled = false;
        Debug.Log("동영상이 재생됩니다.");
        while (IntroMovie.isPlaying)
        {
            Debug.Log("동영상 재생 시간 : " + Mathf.FloorToInt((float)IntroMovie.time));
            yield return null;
        }
        Debug.Log("영상이 끝났습니다.");

        if(!Skip)
            SceneManager.LoadSceneAsync("NYRA");
    }

    private void FixedUpdate()
    {
        //Skip할 경우 세팅
        FadeInSet();
        FadeOutSet();
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
        {
            IntroMovie.Pause();
            Skip = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
