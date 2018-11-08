using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour {

    public Image fade;
    float fades = 1f;
    float time = 0;
    bool fadeinout = false;

    private void FadeSet()
    {
      if(fadeinout)
        {
            Fadeout();
        }

      else if (!fadeinout)
        {
            Fadein();
        }
    }

    private void Fadein()
    {
        time += Time.deltaTime;
        if(fades > 0.0f && time >= 0.1f)
        {
            fades -= 0.2f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;
        }

        else if (fades <= 0.0f)
        {
            fadeinout = !fadeinout;
        }
    }

    private void Fadeout()
    {
        time += Time.deltaTime;
        if (fades < 1f && time >= 0.1f)
        {
            fades += 0.15f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;
        }

        else if (fades >= 1f)
        {
            fadeinout = !fadeinout;
        }
    }

    public void AnyKeyDown()
    {
        if (Input.anyKey)
        {
           // SceneManager.LoadSceneAsync("LoaddingScene");
        }
    }
	
	void Update ()
    {
        FadeSet();
        AnyKeyDown();
    }
}
