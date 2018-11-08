using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoScene: MonoBehaviour
{
    public Image CKLogo;
    public Image NexonGTLogo;
    public Image RedWingLogo;
    private int _logonum = 0;
    Color LColor = new Color(1f, 1f, 1f, 0f);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(StartLogo(CKLogo, 0));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    IEnumerator StartLogo(Image _image, int _num)
    {
        do
        {
            LColor.a += Time.deltaTime * 1.7f;
            _image.color = LColor;
            yield return 0;
        } while (LColor.a < 1f);

        yield return new WaitForSeconds(1.5f);

        do
        {
            LColor.a -= Time.deltaTime * 1.7f;
            _image.color = LColor;
            yield return 0;
        } while (LColor.a > 0f);
        LColor.a = 0f;
        _image.color = LColor;

        yield return new WaitForSeconds(0.8f);

        _logonum++;
        if (_num == 0)
            StartCoroutine(StartLogo(NexonGTLogo, _logonum));

        else if (_num == 1)
            StartCoroutine(StartLogo(RedWingLogo, _logonum));

        else if (_num == 2)
            SceneManager.LoadSceneAsync("Title");

        yield return _logonum;
    }
}