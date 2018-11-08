using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillFootholdObject : MonoBehaviour
{
    private WitchBoss _witch;
    private WitchSkillBase _skill;
    private Transform _target;
    private List<WitchSkillFootholdFire> _fires = new List<WitchSkillFootholdFire>();
    private int _state;
    private int _fireIdx;
    private float m_fireTime;
    private float _readyTime;

    public void Init(WitchSkillBase witchSkill)
    {
        _skill = witchSkill;
        _witch = witchSkill.Witch;

        foreach (WitchSkillFootholdFire a in GetComponentsInChildren<WitchSkillFootholdFire>())
        {
            a.gameObject.SetActive(false);
            _fires.Add(a);
        }
    }

    public void OnSkill(Transform target)
    {
        _skill.IsOn = true;
        _target = target;
        _fireIdx = 0;
        _state = 1;
        _readyTime = 0.0f;

        Vector3 witchPos = _witch.transform.position;
        witchPos.y += 0.0f;

        foreach (WitchSkillFootholdFire a in _fires)
        {
            a.transform.position = Vector3.zero;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case 1:
                OnFire();
                break;

        }
    }

    private void OnFire()
    {

        if (_fireIdx != 0)
        {
            _readyTime += Time.deltaTime;

            if (_readyTime < WitchValueManager.I.FootholdNextTime)
                return;

            //_fires[_fireIdx - 1].gameObject.SetActive(false);

            if (_fireIdx >= _fires.Count)
            {
                _state = 2;
                _skill.IsOn = false;
                return;
            }
        }

        _fires[_fireIdx].gameObject.SetActive(true);
        _fires[_fireIdx].OnFire(_target);

        _fireIdx++;
        _readyTime = 0.0f;
    }
}
