using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    private MonsterBase m_monster;
    [SerializeField]
    private float m_hp;
    [SerializeField]
    private float m_maxHp;
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private float m_rotateSpeed;
    [SerializeField]
    private float m_attackDistance;
    [SerializeField]
    private float m_chaseDistance;

    // properties
    public MonsterBase Monster {  get { return m_monster; } }
    public float Hp { get { return m_hp; } set { m_hp = value; }  }
    public float MaxHp { get { return m_maxHp; } set { m_maxHp = value; } }
    public float MoveSpeed { get { return m_moveSpeed; } set { m_moveSpeed = value; } }
    public float RotateSpeed { get { return m_rotateSpeed; } set { m_rotateSpeed = value; } }
    public float AttackDistance { get { return m_attackDistance; } set { m_attackDistance = value; } }
    public float ChaseDistance { get { return m_chaseDistance; } set { m_chaseDistance = value; } }

    private void Awake()
    {
        m_monster = GetComponent<MonsterBase>();
        m_hp = m_maxHp;
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        //if (Hp <= 0)
        //    gameObject.SetActive(false);
	}
}
