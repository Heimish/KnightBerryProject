using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomStateBase : MonoBehaviour
{
    protected float Dltime;
    protected GuardMushroom _GuardMushroom;
    protected GuardMushroom GuardMushroom { get { return _GuardMushroom; } set { _GuardMushroom = value; } }

    private void Awake()
    {
        _GuardMushroom = GetComponent<GuardMushroom>();
    }

    public virtual void BeginState()
    {
    }

    public virtual void EndState()
    {
    }
}
