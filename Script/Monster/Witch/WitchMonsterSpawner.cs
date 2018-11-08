using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMonsterSpawner : MonoBehaviour
{

    private WitchBoss _witch;
    [SerializeField]
    private SpawnMonster _spawnType;

    private void Start()
    {
    }

    public void OnSpawn()
    {
        MonsterBase mon = ABUGameManager.I.MonsterPool.GetMonster(_spawnType);

        Vector3 homePos = transform.position;
        homePos.y = transform.position.y;

        mon.transform.position = homePos;
        mon.InitMonster(homePos);
    }
}