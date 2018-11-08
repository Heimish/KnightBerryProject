using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMonsterPhase : MonoBehaviour
{
    private List<MonsterBase> _monsters = new List<MonsterBase>();

    // properties
    public List<MonsterBase> Monsters { get { return _monsters; } }

    private void Awake()
    {
        foreach (MonsterBase m in GetComponentsInChildren<MonsterBase>())
        {
            _monsters.Add(m);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPhase()
    {
        foreach (MonsterBase m in _monsters)
        {
            m.OnSpawn();
            EffectManager.I.OnEffect(EffectType.Witch_Spawn, m.transform, m.transform.rotation, 1.5f);
        }
    }
}
