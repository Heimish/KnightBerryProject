using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMoveCheack
{
    Grass,
    Rock
}
public class CPlayerMoveSound : CPlayerBase
{
    public PlayerMoveCheack _PlayerMoveCheack = PlayerMoveCheack.Grass;


    private void Start()
    {
        _PlayerMoveCheack = PlayerMoveCheack.Grass;
    }
    private void Update()
    {        
    }
    //public void SoundType(int type)
    //{
    //    SoundManager.I.PlaySound(transform, (PlaySoundId)type);
    //}
    public void MoveSoundPlay()
    {
        if (Mathf.Abs(CPlayerManager._instance._PlayerMove.fHorizontal) <= 0.05f &&
            Mathf.Abs(CPlayerManager._instance._PlayerMove.fVertical) <= 0.05f)
            return;

        if (_PlayerMoveCheack == PlayerMoveCheack.Rock)
        {
            if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                SoundManager.I.PlaySound(transform, PlaySoundId.Walk_TankerStone);
            else
                SoundManager.I.PlaySound(transform, PlaySoundId.Walk_DealerStone);
        }
        else if (_PlayerMoveCheack == PlayerMoveCheack.Grass)
        {
            if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                SoundManager.I.PlaySound(transform, PlaySoundId.Walk_TankerGrass);
            else
                SoundManager.I.PlaySound(transform, PlaySoundId.Walk_DealerGrass);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            _PlayerMoveCheack = PlayerMoveCheack.Rock;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            _PlayerMoveCheack = PlayerMoveCheack.Grass;
        }
    }
}
