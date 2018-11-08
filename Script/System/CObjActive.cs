using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjActive : MonoBehaviour
{
    public static CObjActive _instance = null;
    private bool m_bObjActive;
    private float m_fObjTime;
    private GameObject _Obj = null;

    void Start ()
    {
        CObjActive._instance = this;

    }
    
    public void ActiveObj(GameObject obj, float time)
    {
        _Obj = obj;
        _Obj.SetActive(false);
        m_bObjActive = true;
        m_fObjTime = time;
        StartCoroutine("StartTime");
    }

    void ResetObj()
    {
        _Obj.SetActive(true);
        m_bObjActive = false;
        _Obj = null;
        m_fObjTime = 0;
    }

    IEnumerator StartTime()
    {
        yield return new WaitForSeconds(m_fObjTime);
        ResetObj();
    }
}
