using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMonsterSpawnerObject : MonoBehaviour
{
    private List<WitchMonsterSpawner> _spawners = new List<WitchMonsterSpawner>();

    public List<WitchMonsterSpawner> Spawners { get { return _spawners; } set { _spawners = value; } }

    private void Awake()
    {
        foreach (WitchMonsterSpawner spawner in GetComponentsInChildren<WitchMonsterSpawner>())
        {
            spawner.gameObject.SetActive(false);
            _spawners.Add(spawner);
        }
    }

    public void OnSpawn()
    {
        foreach (WitchMonsterSpawner spawner in _spawners)
        {
            spawner.gameObject.SetActive(true);
            spawner.OnSpawn();
        }
    }
}
