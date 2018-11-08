using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBossScene : MonoBehaviour 
{
    private void Awake()
    {
        SceneManager.LoadScene("BossScene", LoadSceneMode.Additive);
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
