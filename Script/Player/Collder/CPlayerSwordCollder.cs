using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPlayerSwordCollder : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen" || other.tag == "ShildMushroom" || other.tag == "EliteShaman")
        {
            int nCombo = CPlayerManager._instance.m_nAttackCombo - 1;
            if (nCombo == -1) nCombo = 1;

            if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
            {
                CPlayerManager._instance.ScytheGauge = Mathf.Clamp(CPlayerManager._instance.ScytheGauge+5, 0, InspectorManager._InspectorManager.SwapMaxGauge);
                if (other.tag == "Guard")
                {
                    if (other.GetComponent<GuardMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<MonsterBase>().isHit = true;
                        other.GetComponent<GuardMushroomEffect>().GuardMHitEffect();
                        other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo]);
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.Hit_StandardMonster);
                    }
                }

                else if (other.tag == "Queen")
                {
                    if (other.GetComponent<QueenMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<MonsterBase>().isHit = true;
                        other.GetComponent<QueenMushroomEffect>().QueenMHitEffect();
                        other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo]);
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.Hit_StandardMonster);
                    }
                }

                else if (other.tag == "ShildMushroom")
                {
                    if (other.GetComponent<ShildMushroom>().GroggyEnd == false && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo], InspectorManager._InspectorManager.nGroggyShild[nCombo]);
                        other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.ShildGoblin_Hit);
                    }

                    else if (other.GetComponent<ShildMushroom>().GroggyEnd == true && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo], InspectorManager._InspectorManager.nGroggyShild[nCombo]);
                        other.GetComponent<ShildMushroomEffect>().DefenEffect();
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.ShildGoblin_Hit);
                    }

                    // 2학기 방패엘리트 몬스터 변경사항 : 전방에서도 데미지가 들어가야 함. (임시 수정 1차)
                    //if (other.GetComponent<ShildMushroom>().PlayerisFront == false && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    //{
                    //    other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo], InspectorManager._InspectorManager.nGroggyShild[nCombo]);
                    //    other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
                    //}

                    //else if(other.GetComponent<ShildMushroom>().PlayerisFront == true && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    //{
                    //    other.GetComponent<ShildMushroomEffect>().DefenEffect();
                    //}
                }

                else
                {
                    other.GetComponent<WitchBossEffect>().OnShieldEffect(nCombo);
                    other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo], InspectorManager._InspectorManager.nGroggyShild[nCombo]);
                }
            }
            else
            {
                if (other.tag == "Guard")
                {
                    if (other.GetComponent<GuardMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<MonsterBase>().isHit = true;
                        other.GetComponent<GuardMushroomEffect>().GuardMHitEffect();
                        other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo]);
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
                        other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo]);
                        CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.Hit_StandardMonster);
                    }
                }

                else if (other.tag == "ShildMushroom")
                {
                    if (other.GetComponent<ShildMushroom>().GroggyEnd == false && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
                        CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                        other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo], InspectorManager._InspectorManager.nGroggyScythe[nCombo]);
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.ShildGoblin_Hit);
                    }

                    else if (other.GetComponent<ShildMushroom>().GroggyEnd == true && other.GetComponent<ShildMushroom>().Stat.Hp > 0)
                    {
                        other.GetComponent<ShildMushroomEffect>().DefenEffect();
                        CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                        other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo], InspectorManager._InspectorManager.nGroggyScythe[nCombo]);
                        SoundManager.I.PlaySound(other.transform, PlaySoundId.ShildGoblin_Hit);
                    }
                }
                else
                {
                    other.GetComponent<WitchBossEffect>().OnScytheEffect(nCombo);
                    CPlayerManager._instance.m_PlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                    other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo], InspectorManager._InspectorManager.nGroggyScythe[nCombo]);
                }
            }
            if (nCombo == 0) CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.1f);
            if (nCombo == 1) CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.1f);
            if (nCombo == 2) CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.1f);
        }
    }
}