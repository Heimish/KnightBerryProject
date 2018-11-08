using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWave : MonoBehaviour
{
    private List<Transform> _objects = new List<Transform>();
    private List<MonsterBase> _monsters = new List<MonsterBase>();
    private bool _isRun;

    // properties
    public List<Transform> Objects { get { return _objects; } }
    public bool IsRun { get { return _isRun; } }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            _objects.Add(child);
        }
        foreach (MonsterBase m in GetComponentsInChildren<MonsterBase>())
        {
            _monsters.Add(m);
        }
    }

	void Update ()
    {
        _isRun = RunCheck();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            foreach (Transform obj in _objects)
            {
                obj.gameObject.SetActive(false);
            }
            //foreach (MonsterBase mon in FindObjectsOfType<MonsterBase>())
            //{
            //    if (mon.GetType() == typeof(WitchBoss))
            //        continue;

            //    mon.gameObject.SetActive(false);
            //}
        }
    }

    public bool RunCheck()
    {
        foreach (Transform obj in _objects)
        {
            if (obj.gameObject.activeInHierarchy)
                return true;
        }

        return false;
    }

    public bool WaveRunCheck()
    {
        foreach (Transform obj in _objects)
        {
            if (obj.gameObject.activeInHierarchy)
                return true;
        }

        return false;
    }

    public void InitWave()
    {
        foreach (MonsterBase m in _monsters)
        {
            m.OnSpawn();
            EffectManager.I.OnEffect(EffectType.Witch_Spawn, m.transform, m.transform.rotation, 1.5f);
        }
            SoundManager.I.PlaySound(transform, PlaySoundId.Boss_MonSpawn);
        //foreach (Transform trf in _objects)
        //{
        //    QueenMushroom queen = trf.GetComponent<QueenMushroom>();

        //    if (queen != null)
        //    {
        //        Stage1.I.ChangeMode = false;
        //        return;
        //    }
        //}

        //Stage1.I.ChangeMode = true;
    }
}
