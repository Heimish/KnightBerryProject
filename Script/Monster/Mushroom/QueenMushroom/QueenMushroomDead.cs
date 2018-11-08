using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomDead : QueenMushroomStateBase
{
    private float DeadTime;
    private int Soundcount = 0;

    public override void BeginState()
    {
        DeadTime = 0;
        SoundManager.I.PlaySound(transform.position, PlaySoundId.Monster_Death);
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
        if (QueenMushroom.isDead)
        {
            DeadSound();
            QueenMushroom.rotAnglePerSecond = 0;
            QueenMushroom.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;
            QueenMushroom.CharacterisDead = true;

            if (DeadTime >= 1.2f)
            {
                QueenMushroom.OnDead();
                return;
            }
        }
    }
}
