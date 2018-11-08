using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkillCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen" || other.tag == "ShildMushroom")
        {
            int nCombo = CPlayerManager._instance.m_nAttackCombo - 1;
            if (nCombo == -1) nCombo = 1;

            if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
            {

                if (other.tag == "Guard")
                {
                    if (other.GetComponent<GuardMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<MonsterBase>().isHit = true;
                        other.GetComponent<GuardMushroomEffect>().GuardMHitEffect();
                        other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge);
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.Hit_StandardMonster);
                    }
                }

                else if (other.tag == "Queen")
                {
                    if (other.GetComponent<QueenMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<MonsterBase>().isHit = true;
                        other.GetComponent<QueenMushroomEffect>().QueenMHitEffect();
                        other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge);
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.Hit_StandardMonster);
                    }
                }

                else if (other.tag == "ShildMushroom")
                {
                    if (other.GetComponent<ShildMushroom>().PlayerisFront == false && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge, InspectorManager._InspectorManager.nGroggyShild[nCombo]);
                        other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
                    }

                    else if (other.GetComponent<ShildMushroom>().PlayerisFront == true && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<ShildMushroomEffect>().DefenEffect();
                    }
                }

                //else
                //{
                //    other.GetComponent<WitchBossEffect>().OnShieldEffect(nCombo);
                //    other.GetComponent<WitchBoss>().OnDamage(100000.0f, InspectorManager._InspectorManager.nGroggyShild[nCombo]);
                //}
            }
            else
            {
                if (other.tag == "Guard")
                {
                    if (other.GetComponent<GuardMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<MonsterBase>().isHit = true;
                        other.GetComponent<GuardMushroomEffect>().GuardMHitEffect();
                        other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge);
                        CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.Hit_StandardMonster);
                    }
                }

                else if (other.tag == "Queen")
                {
                    if (other.GetComponent<QueenMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<MonsterBase>().isHit = true;
                        other.GetComponent<QueenMushroomEffect>().QueenMHitEffect();
                        other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge);
                        CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.Hit_StandardMonster);
                    }
                }

                else if (other.tag == "ShildMushroom")
                {
                    if (other.GetComponent<ShildMushroom>().PlayerisFront == false && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
                        CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                        other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.fScytheSkill2Damge, InspectorManager._InspectorManager.nGroggyScythe[nCombo]);
                    }

                    else if (other.GetComponent<ShildMushroom>().PlayerisFront == true && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<ShildMushroomEffect>().DefenEffect();
                    }
                }
            }
            if (nCombo == 0) CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.1f);
            if (nCombo == 1) CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.1f);
            if (nCombo == 2) CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.1f);
            //조재원 바보 전성윤 바보 주강한 바보 이윤지 마스코트 나유리 라이팅 화이팅!
        }
    }
}
