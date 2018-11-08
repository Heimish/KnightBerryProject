using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMonsterPhaseManager : MonoBehaviour
{
    private WitchBoss _witch;
    private List<WitchMonsterPhase> _phase = new List<WitchMonsterPhase>();
    private int _index;

	void Start ()
    {
        _index = 0;
        _witch = FindObjectOfType<WitchBoss>();

	    foreach (WitchMonsterPhase phase in GetComponentsInChildren<WitchMonsterPhase>())
        {
            phase.gameObject.SetActive(false);
            _phase.Add(phase);
        }
	}
	
    public void OnNextSpawn()
    {
        _phase[_index].gameObject.SetActive(true);
        _phase[_index].OnPhase();
        Stage1.I.InitMonsters();
        _index++;
    }


}
