using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueenMushroomAttack : QueenMushroomStateBase
{
    private float RandomNum;
    private float _imsitime;
    private int _bulletcount;
    private bool _stun;
    private bool _notstun;
    private bool _exitattack;

    public override void BeginState()
    {
        Dltime = 0;
        _bulletcount = 0;
        _imsitime = 0;
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

    /*
    private IEnumerator Bulletset()
    {
        for(int i = 0; i < 1; i++)
        {
            Stage1.I.BulletPool.SetBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
        }
        yield return new WaitForSeconds(0.2f);
        _bulletcount++;
    }

    private IEnumerator StunBulletset()
    {
        yield return new WaitForSeconds(0.2f);
        Stage1.I.BulletPool.SetStunBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
        _bulletcount++;
    }*/

    public void StopCor()
    {
        if (_bulletcount > 4)
        {
            //StopCoroutine(StunBulletset());
            //StopCoroutine(Bulletset());
            _exitattack = true;
        }
    }

    public void SetAttack()
    {
        StopCor();
        _imsitime += Time.deltaTime;
        if (QueenMushroom.AniSynchro && _stun && _bulletcount < 5)
        {
            //StartCoroutine(StunBulletset());
            if (_imsitime > 0.2f)
            {
                Stage1.I.BulletPool.SetStunBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
                SoundManager.I.PlaySound(transform, PlaySoundId.Goblin_Missile);
                _bulletcount++;
                _imsitime = 0;
            }
        }

        else if(QueenMushroom.AniSynchro && _notstun && _bulletcount < 5)
        {
            //StartCoroutine(Bulletset());
            if (_imsitime > 0.2f)
            {
                Stage1.I.BulletPool.SetBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
                SoundManager.I.PlaySound(transform, PlaySoundId.Goblin_Missile);
                _bulletcount++;
                _imsitime = 0;
            }
        }
    }

    public void StartAttack()
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

    void FixedUpdate()
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