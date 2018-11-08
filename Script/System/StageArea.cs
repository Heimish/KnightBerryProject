using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaState
{
    Run,
    Next,
    End
}

public class StageArea : MonoBehaviour
{
    private List<MonsterWave> _waves = new List<MonsterWave>();
    private int _curWave;

    public int CurrentWave { get { return _curWave; } }

    void Start ()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            MonsterWave wave = transform.GetChild(i).GetComponent<MonsterWave>();
            wave.gameObject.SetActive(false);
            _waves.Add(wave);
        }

        _curWave = 0;
    }

    void Update ()
    {
		
	}

    public AreaState StartNextWave()
    {
        if (!_waves[_curWave].gameObject.activeInHierarchy)
            return AreaState.Run;

        if (_waves[_curWave].RunCheck())
            return AreaState.Run;

        _waves[_curWave].gameObject.SetActive(false);

        if (_curWave >= _waves.Count - 1)
            return AreaState.End;

        _curWave++;

        StartWave();
        return AreaState.Next;
    }

    public void StartWave()
    {
        //if (_curWave != 0)
        //    return;

        _waves[_curWave].gameObject.SetActive(true);
        _waves[_curWave].InitWave();
        Stage1.I.InitMonsters();
        MonsterManager.I.FindAllMonsters();
    }
}
