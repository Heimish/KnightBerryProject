using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public struct EffectInfo
{
    public EffectType Name;
    public GameObject Effect;
}

public enum EffectType
{
    Hero_Tanker_Attack1 = 0,
    Hero_Tanker_Attack2,
    Hero_Tanker_Attack3,
    Hero_Tanker_Attack4,
    Hero_Tanker_Attack5,
    Hero_Tanker_CounterAttack,
    Hero_Tanker_Shield,
    Hero_Tanker_DashAttack,
    Hero_SwapAttack_Scythe,
    Hero_Swap_Scythe,
    Hero_Swap_Shield,
    Hero_Scythe_Attack1,
    Hero_Scythe_Attack2,
    Hero_Scythe_Attack3,
    Hero_Scythe_JunBang,
    Hero_Scythe_BlinkIn,
    Hero_Scythe_BlinkOut,
    Witch_Teleport,
    Witch_Attack1,
    Witch_Attack2,
    Witch_Spawn,
    Witch_OrbHit,
    Tanker_AfterImage,
    Dealer_AfterImage,
    Tanker_DashAttack,
    Tanker_DashAttackHit,
    Witch_Attack3
}



[Serializable]
public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    public static EffectManager I { get { return _instance; } }

    [SerializeField]
    public EffectInfo[] _effectInfos;
    private Dictionary<EffectType, List<GameObject>> _effectPool = new Dictionary<EffectType, List<GameObject>>();
    private void Awake()
    {
        _instance = this;

        EffectType[] types = Enum.GetValues(typeof(EffectType)) as EffectType[];

        foreach (EffectType type in types)
        {
            _effectPool[type] = new List<GameObject>();
            _effectPool[type].Add(CreateEffect(type));
        }
    }

    public int EventOnEffect(int types)
    {
        if (types == 14)
            OnEffect((EffectType)types, CPlayerManager._instance.transform, 5.0f);
        else
            OnEffect((EffectType)types, CPlayerManager._instance.transform, 2.0f);

        return types;
    }


    private void Update()
    {
    }

    public GameObject OnEffect(EffectType type, Transform target, float deadTime = 0.0f, int posType = 0)
    {
        GameObject effect = GetEffect(type);

        if (effect == null)
        {
            Debug.Log("해당 이펙트는 존재하지 않습니다. typename = " + type.ToString());
            return null;
        }

        effect.SetActive(true);

        if (posType == 0)
        {
            effect.transform.parent = target;

            effect.transform.localPosition = GetEffectInfo(type).transform.position;
            effect.transform.localEulerAngles = GetEffectInfo(type).transform.localEulerAngles;
        }

        else if (posType == 1)
        {
            effect.transform.position = target.position;
            effect.transform.position += GetEffectInfo(type).transform.position;
            effect.transform.eulerAngles = target.eulerAngles;
            effect.transform.eulerAngles += GetEffectInfo(type).transform.localEulerAngles;
        }

        if (deadTime > 0.0f)
        {
            StartCoroutine(Co_EffectDead(effect, deadTime));
        }

        return effect;
    }

    public GameObject OnEffect(EffectType type, Transform target, Quaternion rotation, float deadTime = 0.0f, int posType = 0)
    {
        return OnEffect(type, target.position, rotation, deadTime, posType);
    }

    public GameObject OnEffect(EffectType type, Vector3 target, Quaternion rotation, float deadTime = 0.0f, int posType = 0)
    {
        GameObject effect = GetEffect(type);

        if (effect == null)
        {
            Debug.Log("해당 이펙트는 존재하지 않습니다. typename = " + type.ToString());
            return null;
        }

        effect.SetActive(true);

        if (posType == 1)
            effect.transform.position = target;
        else
        {
            Vector3 pos = GetEffectInfo(type).transform.position;
            pos += target;
            effect.transform.position = pos;
        }

        if (posType == 1)
            effect.transform.rotation = rotation;
        else
        {
            Vector3 angle = GetEffectInfo(type).transform.rotation.eulerAngles;
            angle += rotation.eulerAngles;
            rotation.eulerAngles = angle;
            effect.transform.rotation = rotation;
        }

        if (deadTime > 0.0f)
        {
            StartCoroutine(Co_EffectDead(effect, deadTime));
        }

        return effect;
    }

    public GameObject GetEffect(EffectType type)
    {
        foreach (GameObject info in _effectPool[type])
        {
            if (!info.activeInHierarchy)
                return info;
        }
        return CreateEffect(type);
    }

    private GameObject CreateEffect(EffectType type)
    {
        foreach (EffectInfo info in _effectInfos)
        {
            if (info.Name == type)
            {
                GameObject effect = Instantiate(info.Effect);
                effect.transform.parent = transform;
                effect.SetActive(false);
                _effectPool[type].Add(effect);

                return effect;
            }
        }

        Debug.LogError("해당하는 이펙트가 없습니다. 이펙트가 제대로 들어갔는지 확인 하세요. typename = " + type.ToString());
        return null;
    }

    private IEnumerator Co_EffectDead(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        obj.transform.parent = transform;
    }

    private GameObject GetEffectInfo(EffectType type)
    {
        foreach (EffectInfo info in _effectInfos)
        {
            if (info.Name == type)
                return info.Effect;
        }

        Debug.LogError("해당하는 이펙트가 없습니다 type : " + type.ToString());
        return null;
    }
}