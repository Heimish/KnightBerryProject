using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour
{
    public static CGameManager _instance;
    public Transform _PlayerPos;

	void Start () {
        CGameManager._instance = this;

    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    // 거리재서 비교하기
    public bool PlayDistance(Vector3 vStart, Vector3 vEnd, float f)
    {
        if(Vector3.Distance(vStart, vEnd) < f)
        {
            return true;
        }
        return false;
    }
}
