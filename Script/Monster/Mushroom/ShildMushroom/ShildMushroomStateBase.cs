using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomStateBase : MonoBehaviour {

    protected float Dltime;
    protected ShildMushroom _ShildMushroom;
    protected ShildMushroom ShildMushroom { get { return _ShildMushroom; } set { _ShildMushroom = value; } }

    private void Awake()
    {
        _ShildMushroom = GetComponent<ShildMushroom>();
    }

    public virtual void BeginState()
    {
    }

    public virtual void EndState()
    {
    }
}
