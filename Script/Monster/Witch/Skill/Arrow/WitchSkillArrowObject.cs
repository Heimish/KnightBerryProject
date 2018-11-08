using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillArrowObject : MonoBehaviour
{
    private WitchBoss _witch;
    private WitchSkillBase _skill;
    private Transform _target;
    private List<WitchSkillArrowSubArrow> _subArrows = new List<WitchSkillArrowSubArrow>();
    private WitchSkillArrowSubArrow _centerArrow;
    private List<WitchSkillArrowSubArrow[]> _onObjs = new List<WitchSkillArrowSubArrow[]>();
    private int _state;
    private int _arrowIdx;
    private float _fireTime;
    private float _readyTime;

    public void Init(WitchSkillBase witchSkill)
    {
        _witch = witchSkill.Witch;
        _skill = witchSkill; 
        foreach (WitchSkillArrowSubArrow a in GetComponentsInChildren<WitchSkillArrowSubArrow>())
        {
            if (a.name == "CenterArrow")
            {
                _centerArrow = a;
                continue;
            }

            _subArrows.Add(a);
        }

        WitchSkillArrowSubArrow[] obj = new WitchSkillArrowSubArrow[1];
        obj[0] = _centerArrow;
        _onObjs.Add(obj);
        obj = new WitchSkillArrowSubArrow[2];
        obj[0] = _subArrows[1];
        obj[1] = _subArrows[2];
        _onObjs.Add(obj);
        obj = new WitchSkillArrowSubArrow[2];
        obj[0] = _subArrows[0];
        obj[1] = _subArrows[3];
        _onObjs.Add(obj);
    }

    public void OnSkill(Transform target)
    {
        _skill.IsOn = true;
        _target = target;
        _arrowIdx = 0;
        _state = 1;
        _readyTime = 0.0f;
        _fireTime = 0.0f;

        Vector3 witchPos = _witch.transform.position;
        witchPos.y += 1.5f;

        transform.position = witchPos;
        transform.rotation = _witch.transform.rotation;

        for (int i = 0; i < _subArrows.Count; i++)
        {
            _subArrows[i].transform.localPosition = new Vector3(-2.0f + (i / (float)_subArrows.Count) * 4.0f,
                                                0.0f, 0.0f);
            if (i >= 2)
            {
                _subArrows[i].transform.localPosition = new Vector3(-2.0f + ((i+1) / (float)_subArrows.Count) * 4.0f,
                                                                0.0f, 0.0f);
            }
            _subArrows[i].gameObject.SetActive(false);
        }

        _centerArrow.transform.localPosition = new Vector3(-2.0f + (2 / (float)_subArrows.Count) * 4.0f,
                                                                0.0f, 0.0f);
        _centerArrow.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        switch (_state)
        {
            case 1:
                ReadyMove();
                break;

            case 2:
                ShotArrow();
                break;
        }
    }

    private void ReadyMove()
    {
        _readyTime += Time.deltaTime;

        if (_readyTime < WitchValueManager.I.ArrowSpawnTime)
            return;

        if (_arrowIdx >= _onObjs.Count)
        {
            _state = 2;
            _arrowIdx = _onObjs.Count - 1;
            return;
        }

        foreach (WitchSkillArrowSubArrow obj in _onObjs[_arrowIdx])
        {
            obj.Ready(_target);
            obj.gameObject.SetActive(true);
        }

        _arrowIdx++;
        SoundManager.I.PlaySound(_witch.transform, PlaySoundId.Boss_Arrow_Spawn);
        _readyTime = 0.0f;

        Vector3 pos = _target.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
    }

    private void ShotArrow()
    {
        if (_arrowIdx < 0)
        {
            _skill.IsOn = false;
            foreach (WitchSkillArrowSubArrow a in _subArrows)
            {
                if (a.gameObject.activeInHierarchy)
                    return;
            }

            if (_centerArrow.gameObject.activeInHierarchy)
                return;

            gameObject.SetActive(false);
            return;
        }

        _fireTime += Time.deltaTime;

        if (_fireTime < WitchValueManager.I.ArrowFireTime)
            return;

        foreach (WitchSkillArrowSubArrow obj in _onObjs[_arrowIdx])
        {
            obj.OnArrow(WitchValueManager.I.ArrowSpeed);
        }
        SoundManager.I.PlaySound(transform, PlaySoundId.Boss_Arrow);
        _arrowIdx--;
        _fireTime = 0.0f;
    }
}
