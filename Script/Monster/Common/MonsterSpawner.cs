using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnMonster
{
    GuardMushroom = 0,
    QueenMushroom,
}
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnMonster _spawnType;

    private float _spawnTime;

    [SerializeField]
    private float NextSpawnTime = 5.0f;

    [SerializeField]
    private float LifeTime = 10.0f;

    private float _lifeTime;

    private void Awake()
    {
        _spawnTime = NextSpawnTime;
    }

    private void Update()
    {
        SpawnUpdate();
        DeadCheck();
    }

    public void OnSpawn()
    {
        MonsterBase mon = ABUGameManager.I.MonsterPool.GetMonster(_spawnType);

        Vector3 homePos = transform.position;
        homePos.y = transform.position.y;

        mon.transform.position = homePos;
        mon.InitMonster(homePos);
    }

    private void SpawnUpdate()
    {
        _spawnTime += Time.deltaTime;

        if (_spawnTime < NextSpawnTime)
            return;

        OnSpawn();

        _spawnTime = 0.0f;
    }

    private void DeadCheck()
    {
        _lifeTime += Time.deltaTime;

        if (_lifeTime < LifeTime)
            return;

        _lifeTime = 0.0f;
        gameObject.SetActive(false);
    }
}
