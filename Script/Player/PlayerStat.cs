using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float Inspector_MaxHp = 200.0f;
    public float Inspector_MaxStamina = 100.0f;
    public float Inspector_MaxPowerGague = 100.0f;
    public float Inspector_MoveSpeed = 10.0f;
    public float Inspector_RotationSpeed = 360.0f;

    private PlayerBase _player;
    private float[] _tankerDamage;
    private float[] _dealerDamage;
    private float _counterDamage;
    private float _hp;
    private float _maxHp;
    private float _stamina;
    private float _maxStamina;
    private float _powerGague;
    private float _maxPowerGague;
    private float _moveSpeed;
    private float _rotationSpeed;
    private float _addRoationSpeed;

    // properties
    public PlayerBase Player { get { return _player; } }
    public float[] TankerDamage { get { return _tankerDamage; } }
    public float[] DealerDamage { get { return _dealerDamage; } }
    public float CounterDmage { get { return _counterDamage; } }
    public float Hp { get { return _hp; } }
    public float MaxHp { get { return _maxHp; } }
    public float Stamina { get { return _stamina; } }
    public float MaxStamina { get { return _maxStamina; } }
    public float PowerGague { get { return _powerGague; } }
    public float MaxPowerGague { get { return _maxPowerGague; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float RotationSpeed { get { return _rotationSpeed; } }
    public float AddRotationSpeed { get { return _addRoationSpeed; } set { _addRoationSpeed = value; } }

	void Start ()
    {
        _player = GetComponent<PlayerBase>();

        _tankerDamage = new float[5];
        for (int i = 0; i < InspectorManager._InspectorManager.nDamgeShild.Length; i++)
            _tankerDamage[i] = InspectorManager._InspectorManager.nDamgeShild[i];

        _dealerDamage = new float[3];
        for (int i = 0; i < InspectorManager._InspectorManager.nDamgeScythe.Length; i++)
            _dealerDamage[i] = InspectorManager._InspectorManager.nDamgeScythe[i];

        _counterDamage = InspectorManager._InspectorManager.fCountAttackDamge;

        _maxHp = MaxHp;
        _hp = _maxHp;
        _maxStamina = MaxStamina;
        _stamina = _maxStamina;
        _maxPowerGague = Inspector_MaxPowerGague;
        _powerGague = 0.0f;

        _moveSpeed = Inspector_MoveSpeed;
        _rotationSpeed = Inspector_RotationSpeed;
        _addRoationSpeed = 0.0f;
    }
}
