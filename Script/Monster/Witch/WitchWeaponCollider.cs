using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchWeaponCollider : MonoBehaviour
{
    private WitchBoss _witch;
    private BoxCollider _collider;

    public BoxCollider Collider { get { return _collider; } }

    private void Awake()
    {
        _witch = FindObjectOfType<WitchBoss>();
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
            {
                _witch.Target.PlayerHp(0.2f, 2, 10.0f);
                if (_witch.Target._PlayerShild._isShildCounter)
                {
                    _witch.SetState(WitchState.GuardAttack);
                    return;
                }
            }
            else
                _witch.Target.PlayerHp(0.2f, 1, 10.0f);
        }
    }
}
