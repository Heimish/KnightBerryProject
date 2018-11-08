using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager _instance;
    public static MonsterManager I { get { return _instance; } }

    private List<MonsterBase> _monsters = new List<MonsterBase>();

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void FindAllMonsters()
    {
        _monsters.Clear();
        foreach (MonsterBase mon in FindObjectsOfType<MonsterBase>())
        {
            if (mon.gameObject.activeInHierarchy)
                _monsters.Add(mon);
        }
    }

    public MonsterBase[] FindNearMonster(Vector3 from, float dis)
    {
        List<MonsterBase> mons = new List<MonsterBase>();

        foreach (MonsterBase mon in _monsters)
        {
            if (Vector3.Distance(from, mon.transform.position) > dis)
                continue;

            mons.Add(mon);
        }

        return mons.ToArray();
    }
}
