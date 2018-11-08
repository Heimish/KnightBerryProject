using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillAObject : MonoBehaviour
{
    private WitchBoss _witch;
    private WitchSkillBase _skill;
    private Transform _target;
    private List<WitchSkillAFire> _fires = new List<WitchSkillAFire>();
    private int _state;
    private int _fireIdx;
    private float _fireTime;
    private float _readyTime;

    public void Init(WitchSkillBase witchSkill)
    {
        _witch = witchSkill.Witch;
        _skill = witchSkill;

        foreach (WitchSkillAFire c in GetComponentsInChildren<WitchSkillAFire>())
        {
            c.gameObject.SetActive(false);
            _fires.Add(c);
        }
    }

    public void OnSkill(Transform target)
    {
        _target = target;
        _state = 1;
        _fireIdx = 0;
        _fireTime = 0.0f;
        _readyTime = 0.0f;
        _skill.IsOn = true;

        Vector3 witchPos = _witch.transform.position;
        witchPos.y += 2.7f;

        transform.position = witchPos;
        transform.rotation = _witch.transform.rotation;
        //foreach (WitchSkillAFire f in _fires)
        //{
        //    f.transform.localPosition = Vector3.zero;
        //    f.gameObject.SetActive(true);
        //}
    }

    void Update()
    {
        switch (_state)
        {
            case 1:
                InitFiresPosition();
                break;

            case 2:
                //MoveFires();
                break;

            case 3:
                //EndCheck();
                break;
        }
    }

    private void InitFiresPosition()
    {
        //ReadyMove();
        if (_fireIdx != 0)
        {

            _readyTime += Time.deltaTime;

            if (_readyTime < WitchValueManager.I.OrbNextTime)
                return;

            if (_fireIdx >= _fires.Count)
            {
                _state = 2;
                _fireIdx = 0;
                _skill.IsOn = false;
                return;
            }
        }

        _fires[_fireIdx].gameObject.SetActive(true);
        _fires[_fireIdx].Ready(transform.position,
                               _witch.Target.transform);
        SoundManager.I.PlaySound(transform, PlaySoundId.Boss_Orb_Spawn);
        _fireIdx++;
        _readyTime = 0.0f;
    }

    //private void ReadyMove()
    //{
    //    if (_fireIdx >= _fires.Count)
    //        return;

    //    _fires[_fireIdx].Ready(PositionCheck());
    //    //m_fires[i].Ready(new Vector3(-1.5f + ((float)i / (m_fires.Count - 1) * 3.0f),
    //    //                                                    1.5f, 0.0f));

    //    _fireIdx++;

    //}

    //private void MoveFires()
    //{
    //    _fireTime += Time.deltaTime;

    //    if (_fireTime < 0.2f)
    //        return;

    //    Vector3 target = _target.position;
    //    target.y += 1.0f;

    //    _fires[_fireIdx].FireInit(_target, 700.0f);

    //    _fireIdx++;
    //    _fireTime = 0.0f;

    //    if (_fireIdx == _fires.Count)
    //    {
    //        _state = 3;
    //        _skill.IsOn = false;
    //    }
    //}

    private Vector3 PositionCheck()
    {
        Vector3 pos = Vector3.zero;
        int loopCount = 0;

        while (CrashCheck(out pos))
        {
            loopCount++;

            if (loopCount >= 100)
            {
                Debug.LogError("영역이 너무 좁아서 발생하는 에러압니다. 영역을 줄여주세요");
                break;
            }
        }

        return pos;
    }

    private bool CrashCheck(out Vector3 pos)
    {
        pos = new Vector3(Random.Range(-1.6f, 1.6f), Random.Range(0.8f, 2.2f), Random.Range(-1.6f, 0.8f));

        for (int i = 0; i < _fireIdx; i++)
        {
            //print(m_fireIdx + " " + i + " " + Vector3.Distance(pos, m_fires[i].ReadyPos));
            if (Vector3.Distance(pos, _fires[i].ReadyPos) <= 1.0f)
                return true;
        }

        return false;
    }

    //private void EndCheck()
    //{
    //    foreach (WitchSkillAFire f in _fires)
    //    {
    //        if (f.gameObject.activeInHierarchy)
    //            return;
    //    }

    //    _skill.IsOn = false;
    //}
}
