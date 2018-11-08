using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterUI : MonoBehaviour
{
    private CPlayerMove _CPlayerMove = null;
    public CPlayerMove _PlayerMove { get { return _CPlayerMove; } }
    
    public float saveHP { get; set; }
    public float maxHP { get; set; }
    public float curHP { get; set; }
    public float maxSP { get; set; }
    public float curSP { get; set; }
    public float maxRush { get; set; }
    public float curRush { get; set; }
    public float maxDownward { get; set; }
    public float curDownward { get; set; }
    public float maxSwap { get; set; }
    public float curSwap { get; set; }
    public float maxScytheTime { get; set; }
    public float curScytheTime { get; set; }
    public float maxRoll { get; set; }
    public float curRoll { get; set; }
    public float maxBlink { get; set; }
    public float curBlink { get; set; }

    void Start()
    {
        InitParams();
    }

    public virtual void InitParams()
    {
        
    }
}

