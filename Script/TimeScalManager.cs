using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScalManager : MonoBehaviour
{
    public static TimeScalManager _instance = null;
    public float m_fTimeScal;
    private bool m_bTimeAttack;
    public float m_fBackTime;

    private void Awake()
    {
        TimeScalManager._instance = this;
        m_fTimeScal = 1.0f;
        m_bTimeAttack = false;
    }
    void Update ()
    {
        Time.timeScale = m_fTimeScal;
    }

    public float TimeScal(float time)
    {
        m_fTimeScal = time;
        return time;
    }
}
