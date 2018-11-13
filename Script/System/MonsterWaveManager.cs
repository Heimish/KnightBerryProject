using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveManager : Singleton<MonsterWaveManager>
{
    //private static MonsterWaveManager _instance;
    //public static MonsterWaveManager I { get { return _instance; } }

    private List<StageArea> _stages = new List<StageArea>();
    private int _curStage;
    private bool _isRun;
    private bool _isStart;

    public bool IsRun { get { return _isRun; } }
    public int CurrentStage { get { return _curStage; } }

    private bool _allClear;
    public bool AllClear { get { return _allClear; } }

    private void Awake()
    {
        //_instance = this;
        _allClear = false;
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            StageArea area = transform.GetChild(i).GetComponent<StageArea>();
            area.gameObject.SetActive(true);
            _stages.Add(area);
        }

        _curStage = 0;
    }

    void Update()
    {
        if (!_isStart)
            return;

        if (_curStage >= _stages.Count)
            return;

        if (_stages[_curStage].StartNextWave() == AreaState.End)
        {
            _isRun = false;
            if (_curStage < 1)
                FindObjectOfType<StageGateHandler>().SetGateState(false);
            else
                FindObjectOfType<StageGateHandler>().SetGateState(false, 1);

            if (_curStage == _stages.Count - 1)
                _allClear = true;
        }
        else
            _isRun = true;
    }

    public void AddStage()
    {
        if (!_isStart)
            return;

        _curStage++;

        if (_curStage >= _stages.Count)
            return;

        _stages[_curStage].StartWave();
    }

    public void StartWave()
    {
        if (_isStart)
            return;

        _isStart = true;
        _stages[_curStage].StartWave();
    }
}
