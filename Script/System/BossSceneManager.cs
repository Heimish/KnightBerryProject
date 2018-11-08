using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour
{
    public GameObject Point;
    public GameObject Delete;
    public GameObject ViewCamera;
    public GameObject FadeUI;

    private float _time;
    private bool _play;
    private bool _play2;
    private bool _flag;
    private GameObject _ui;
    public Animator Anim;
    private BUFadeImage _fade;

    private int _actionRoot1 = 0;

    private void Awake()
    {
        _play = false;
        _play2 = false;
        _time = 0.0f;
    }

    void Start()
    {
        ViewCamera.SetActive(false);
        Point.SetActive(true);
        Delete.SetActive(false);
        //FadeUI.SetActive(false);
        Anim = FadeUI.transform.GetChild(0).GetComponent<Animator>();
        _fade = FadeUI.transform.GetChild(0).GetComponent<BUFadeImage>();
        _ui = GameObject.Find("UI").gameObject;
        _fade.Manager = this;
    }

    void Update()
    {
        PlayUpdate();
        DirectingUpdate();
        if (MonsterWaveManager.I.AllClear && !_flag)
        {
            _play = true;
            _flag = true;
        }
    }

    private void PlayUpdate()
    {
        if (!_play)
            return;

        FadeUI.SetActive(true);
        Anim.SetInteger("Fade", 1);
        _play = false;
    }

    private void DirectingUpdate()
    {
        if (!_play2)
            return;

        _time += Time.deltaTime;

        if (_time <= 0.5f)
            return;

        Point.SetActive(false);
        Delete.SetActive(true);

        if (_time <= 4.0f)
            return;

        _play2 = false;
        Anim.SetInteger("Fade", 1);
    }

    public void Action1()
    {
        switch (_actionRoot1)
        {
            case 0:
                ViewCamera.SetActive(true);
                _ui.SetActive(false);
                _play2 = true;
                _actionRoot1++;
                break;
            case 1:
                Point.SetActive(false);
                Delete.SetActive(false);
                ViewCamera.SetActive(false);
                _ui.SetActive(true);
                _actionRoot1++;
                break;
        }
    }
}
