using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABUGameManager : MonoBehaviour
{
    private static ABUGameManager _instance;
    public static ABUGameManager I
    {
        get { return _instance; }
    }


    private MonsterObjectPool _monsterPool;
    private WitchMonsterPhaseManager _monsterPhase;

    //properties
    public MonsterObjectPool MonsterPool { get { return _monsterPool; } set { _monsterPool = value; } }
    public WitchMonsterPhaseManager MonsterPhase { get { return _monsterPhase; } set { _monsterPhase = value; } }

    private void Awake()
    {
        _instance = this;
        _monsterPool = FindObjectOfType<MonsterObjectPool>();
        _monsterPhase = FindObjectOfType<WitchMonsterPhaseManager>();
    }

    void Start()
    {

    }

    void Update()
    {
    }
}
