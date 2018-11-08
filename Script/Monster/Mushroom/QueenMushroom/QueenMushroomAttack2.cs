using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomAttack2 : QueenMushroomStateBase
{
    private float RandomNum;
    private float _rotate;
    private int _bulletcount;
    private bool _stun;
    private bool _notstun;
    private bool _exitattack;
    public GameObject _from;

    public override void BeginState()
    {
        _bulletcount = 0;
        _rotate = 0;
        RandomNum = Random.Range(0, 9f);
        QueenMushroom.AttackStack++;
    }

    public override void EndState()
    {
        base.EndState();
        _bulletcount = 0;
        _stun = false;
        _notstun = false;
        _exitattack = false;
        QueenMushroom.AniSynchro = false;
        QueenMushroom.AttackTimer = 0f;
    }

    public void StopCor()
    {
        if (_bulletcount > 4)
        {
            _exitattack = true;
        }
    }

    public void SetAttack()
    {
        if (QueenMushroom.AniSynchro && _stun && _bulletcount < 5)
        {
            for(int i = 0; i < 5; i++)
            {
                Stage1.I.BulletPool.SetSStunBullet(_QueenMushroom, _from.transform.position, _rotate);
                _rotate += 22.5f;
                _bulletcount++;
                SoundManager.I.PlaySound(transform.position, PlaySoundId.Goblin_Missile);
            }
            StopCor();
        }

        else if (QueenMushroom.AniSynchro && _notstun && _bulletcount < 5)
        {
            for(int i = 0; i < 5; i++)
            {
                Stage1.I.BulletPool.SetSBullet(_QueenMushroom, _from.transform.position, _rotate);
                _rotate += 22.5f;
                _bulletcount++;
                SoundManager.I.PlaySound(transform.position, PlaySoundId.Goblin_Missile);
            }
            StopCor();
        }
    }

    public void StartSAttack()
    {       
        if (QueenMushroom.AttackStack > 2 && RandomNum > 2f)
        {
            _stun = true;
            QueenMushroom.AttackStack = 0;
            QueenMushroom.AniSynchro = true;
        }

        else
        {
            _notstun = true;
            QueenMushroom.AniSynchro = true;
        }
    }

    void Update()
    {
        SetAttack();
        QueenMushroom.NowisHit();
        QueenMushroom.GoToPullPush();
        QueenMushroom.PlayerisDead();
        QueenMushroom.TurnToDestination();
        

        Dltime += Time.deltaTime;

        if (_exitattack && Dltime > 1.5f)
        {
            if (QueenMushroom.GetDistanceFromPlayer() > QueenMushroom.MStat.AttackDistance)
            {
                QueenMushroom.SetState(QueenMushroomState.Chase);
                return;
            }

            else
            {
                QueenMushroom.SetState(QueenMushroomState.Return);
                return;
            }
        }
    }
}

