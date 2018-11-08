using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomDead : GuardMushroomStateBase
{
    private float DeadTime;
    private int Soundcount = 0;

    public override void BeginState()
    {
        DeadTime = 0;
        GuardMushroom.ExitGravity = true;  
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void DeadSound()
    {
        if (Soundcount <= 0)
        {
            SoundManager.I.PlaySound(transform, PlaySoundId.Monster_Death);
            Soundcount++;
        }
    }

    void Update()
    {
        if (GuardMushroom.isDead)
        {
            DeadSound();
            GuardMushroom.rotAnglePerSecond = 0;
            GuardMushroom.AttackRotAngle = 0;
            GuardMushroom.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;
            GuardMushroom.CharacterisDead = true;

            if (DeadTime >= 1.2f)
            {
                GuardMushroom.OnDead();
                return;
            }
        }
    }
}
