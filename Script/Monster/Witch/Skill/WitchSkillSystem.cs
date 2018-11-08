using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WitchSkill
{
    A = 0,
    B,
    C,
    Teleport,
}
public class WitchSkillSystem : MonoBehaviour
{
    private WitchBoss _witch;
    private Dictionary<WitchSkill, WitchSkillBase> _skills = new Dictionary<WitchSkill, WitchSkillBase>();
    private List<WitchSkillBase> _randomSkills = new List<WitchSkillBase>();
    private WitchSkill _oldSkill;
    private WitchSkillBase _curSkill;
    private int[] _skillIdxs;
    private int _curIdx = int.MaxValue;

    // properties
    public WitchBoss Witch { get { return _witch; } set { _witch = value; } }
    public Dictionary<WitchSkill, WitchSkillBase> Skills { get { return _skills; } }
    public WitchSkillBase CurrentSkill { get { return _curSkill; } }
    public WitchSkill OldSkill { get { return _oldSkill; } }

    private void Awake()
    {
        _witch = GetComponent<WitchBoss>();
        _skills.Add(WitchSkill.A, new WitchSkillA(_witch, 400.0f));
        _skills.Add(WitchSkill.B, new WitchSkillArrow(_witch, 100.0f));
        _skills.Add(WitchSkill.C, new WitchSkillFoothold(_witch, 310.0f));
        _skills.Add(WitchSkill.Teleport, new WitchSkillTeleport(_witch, 10.0f));
        _randomSkills.Add(_skills[WitchSkill.A]);
        _randomSkills.Add(_skills[WitchSkill.B]);
        _skillIdxs = new int[_randomSkills.Count];
    }

    void Update()
    {

    }

    public void OnSkill(Transform target)
    {
        //if (_curIdx >= _randomSkills.Count)
        //{
        //    for (int i = 0; i < _randomSkills.Count; i++)
        //    {
        //        while (true)
        //        {
        //            bool flag = true;

        //            _skillIdxs[i] = Random.Range(0, _randomSkills.Count);

        //            for (int j = 0; j < i; j++)
        //            {
        //                if (_skillIdxs[i] == _skillIdxs[j])
        //                {
        //                    flag = false;
        //                    break;
        //                }
        //                flag = true;
        //            }

        //            if (flag)
        //                break;
        //        }
        //    }

        //    _curIdx = 0;
        //}


        //_oldSkill = (WitchSkill)Random.Range(0, _randomSkills.Count);
        //_curSkill = _randomSkills[(int)_oldSkill];
        //_curSkill.OnSkill(target);
        OnSkill((WitchSkill)Random.Range(0, 2), target);
        //_curIdx++;
    }

    public void OnSkill(WitchSkill skill, Transform target)
    {
        _oldSkill = skill;
        _curSkill = _skills[_oldSkill];
        _curSkill.OnSkill(target);
    }

    public void OnTeleport(Transform target, int type)
    {
        _oldSkill = WitchSkill.Teleport;
        _curSkill = _skills[_oldSkill];
        _curSkill.OnSkill(target, type);
    }

    public void OnPassiveSkill(WitchSkill skill, Transform target)
    {
        _skills[skill].OnSkill(target);
    }
}
