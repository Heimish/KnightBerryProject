using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomDead : ShildMushroomStateBase
{
    ShildMushroomEffect _groggy;
    private float DeadTime;
    private int Soundcount = 0;

    public override void BeginState()
    {
        _groggy = GetComponent<ShildMushroomEffect>();
        ShildMushroom.CharacterisDead = true;
        DeadTime = 0;
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
        if (ShildMushroom.isDead)
        {
            DeadSound();
            ShildMushroom.rotAnglePerSecond = 0;
            ShildMushroom.AttackRotAngle = 0;
            ShildMushroom.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;

            if (DeadTime >= 1.6f)
            {
                ShildMushroom.OnDead();
                return;
            }
        }
    }
}
