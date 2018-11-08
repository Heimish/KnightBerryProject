using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomSbombing : GuardMushroomStateBase
{
    GuardMushroomEffect _guardeffect;

    public override void BeginState()
    {
        base.BeginState();
        _guardeffect = GetComponent<GuardMushroomEffect>();
    }

    public override void EndState()
    {
        base.EndState();
    }

    //public void EffectofSbomb(Vector3 From)
    //{
    //    From.y += 1f;
    //    transform.position = From;
    //    Instantiate(SbombEffect, transform.position, Quaternion.identity);
    //}

    void Update()
    {
        GuardMushroom.GoToDestination(GuardMushroom.Player.position, GuardMushroom.BerserkerMoveSpeed, GuardMushroom.rotAnglePerSecond);

        if (GuardMushroom.SbombTimer > 6f)
        {
            _guardeffect.BombEffect(transform.position);
            if (GuardMushroom.GetDistanceFromPlayer() < 4f)
            {
                CPlayerManager._instance.PlayerHp(0.2f, 1, GuardMushroom.SbombDamage);
            }
            GuardMushroom.isDead = true;
            GuardMushroom.OnDead();
        }
    }
}
