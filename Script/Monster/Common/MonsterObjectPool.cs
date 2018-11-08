using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _monsterRcs;

    private Dictionary<SpawnMonster, List<MonsterBase>> _monsters = new Dictionary<SpawnMonster, List<MonsterBase>>();
    private List<MonsterBase> _guards = new List<MonsterBase>();
    private List<MonsterBase> _queens = new List<MonsterBase>();

    // properties
    public Dictionary<SpawnMonster, List<MonsterBase>> Monsters { get { return _monsters; } }
    public List<MonsterBase> Guards { get { return _guards; } }
    public List<MonsterBase> Queens { get { return _queens; } }

    private void Awake()
    {
        foreach (MonsterBase mon in transform.GetComponentsInChildren<GuardMushroom>())
        {
            mon.gameObject.SetActive(false);
            _guards.Add(mon);
        }
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    GuardMushroom mon = transform.GetChild(i).GetComponent<GuardMushroom>();

        //    if (mon == null)
        //        continue;

        //    mon.gameObject.SetActive(false);
        //    _guards.Add(mon);
        //}
        _monsters.Add(SpawnMonster.GuardMushroom, _guards);

        foreach (MonsterBase mon in transform.GetComponentsInChildren<QueenMushroom>())
        {
            mon.gameObject.SetActive(false);
            _queens.Add(mon);
        }
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    QueenMushroom mon = transform.GetChild(i).GetComponent<QueenMushroom>();

        //    if (mon == null)
        //        continue;

        //    mon.gameObject.SetActive(false);
        //    _queens.Add(mon);
        //}
        _monsters.Add(SpawnMonster.QueenMushroom, _queens);
    }

    public MonsterBase GetMonster(SpawnMonster type)
    {
        foreach (MonsterBase m in _monsters[type])
        {
            if (m == null)
                continue;

            if (!m.gameObject.activeInHierarchy)
            {
                m.gameObject.SetActive(true);
                return m;
            }
        }

        MonsterBase createMon = CreateMonster(type);
        createMon.gameObject.SetActive(true);

        return createMon;
    }

    private MonsterBase CreateMonster(SpawnMonster type)
    {
        GameObject monObj = null;
        MonsterBase mon = null;

        monObj = Instantiate(_monsterRcs[(int)type]) as GameObject;
        monObj.SetActive(false);
        monObj.transform.parent = transform;
        monObj.transform.position = Vector3.zero;

        mon = monObj.GetComponent<MonsterBase>();

        switch (type)
        {
            case SpawnMonster.GuardMushroom:
                _guards.Add(mon);
                break;
            case SpawnMonster.QueenMushroom:
                _queens.Add(mon);
                break;
        }
        return mon;
    }

    public bool IsEmpty()
    {
        foreach (List<MonsterBase> mons in _monsters.Values)
        {
            foreach (MonsterBase mon in mons)
            {
                if (mon.gameObject.activeInHierarchy)
                    return false;
            }
        }

        return true;
    }
}
