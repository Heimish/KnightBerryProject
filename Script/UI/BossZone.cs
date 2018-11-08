using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{ 
    public GameObject BossHP;
    public GameObject Player;
    public GameObject TipOff;

    private float _WaitTime;
    private int _Stack;
    WitchBoss _Boss;

    private bool isTriggerEnter;
    private bool delay;

    void Start()
    {
        _WaitTime = 0;
        _Stack = 0;
    }

    void Update()
    {
        _WaitTime += Time.deltaTime;
        if (_WaitTime > 7f)
        {
            if (_Boss == null)
                _Boss = FindObjectOfType<WitchBoss>().GetComponent<WitchBoss>();

            if (isTriggerEnter || _Boss.IsDead)
            {
                if (_Stack < 2)
                    SoundManager.I.BossSoundPlay(Player.transform);
                _Stack++;
                isTriggerEnter = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {          
            BossHP.SetActive(true);
            TipOff.SetActive(false);

            if (!delay)
            {
                isTriggerEnter = true;
                delay = true;
            }
        }
    }
}
