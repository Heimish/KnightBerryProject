using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerScytheStart : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //CPlayerManager._instance._PlayerAni_Contorl.InteractionOn
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen")
        {
            CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.2f);
            float SkillDamge1 = InspectorManager._InspectorManager.fScytheStartSkillDamge;
            float SkillDamge2 = InspectorManager._InspectorManager.fScytheSkill2Damge;


            if (other.tag == "Guard")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<GuardMushroomEffect>().GuardSwapEffect();

                if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<GuardMushroom>().OnDamage(SkillDamge2);
                else
                    other.GetComponent<GuardMushroom>().OnDamage(SkillDamge1);
            }

            else if (other.tag == "Queen")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<QueenMushroomEffect>().QueenSwapEffect();

                if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<QueenMushroom>().OnDamage(SkillDamge2);
                else
                    other.GetComponent<QueenMushroom>().OnDamage(SkillDamge1);
            }

            else
            {
                if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<WitchBoss>().OnDamage(SkillDamge2);
                else
                    other.GetComponent<WitchBoss>().OnDamage(SkillDamge1);
            }

            CPlayerManager._instance._PlayerAni_Contorl.AniStiff();
        }
    }
}

