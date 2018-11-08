using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCounterAttack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen" || other.tag == "ShildMushroom")
        {
            CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.2f);
            if (other.tag == "Guard")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.fCountAttackDamge);
            }
            else if (other.tag == "Queen")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.fCountAttackDamge);
            }

            else if (other.tag == "ShildMushroom")
            {
                other.GetComponent<ShildMushroom>().AddGroggyValue(105f);
                other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.fCountAttackDamge);
            }

            else
            {
                other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.fCountAttackDamge, InspectorManager._InspectorManager.fCountGroggy);
            }
        }
    }
}
