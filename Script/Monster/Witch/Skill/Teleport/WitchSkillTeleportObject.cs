using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillTeleportObject : MonoBehaviour
{
    private WitchBoss _witch;
    private WitchSkillBase _skill;
    private Transform _target;
    private int _state = 0;
    private GameObject _in;
    private GameObject _out;
    private float _teleportTime;
    private Vector3 _destination;
    private int _type;
    private float _witchY;
    private bool _init;
    private bool _outActive;
    private float _hide;
    private int _hideInOut;
    private float _hideTime;
    private bool _posChange;

    public void Init(WitchSkillBase skill)
    {
        _witch = skill.Witch;
        _skill = skill;
        _state = 0;
        
        //_in = transform.Find("In").gameObject;
        //_out = transform.Find("Out").gameObject;

        //_in.SetActive(false);
        //_out.SetActive(false);
    }

    void Update()
    {
        switch (_state)
        {
            case 1:
                TeleportUpdate();
                break;
        }
        FadeInOut();
    }

    private void FadeInOut()
    {
        if (_hideInOut == 0)
            return;

        //_hideTime += Time.deltaTime;

        //if (_hideTime < 0.03f)
        //    return;

        if (_hideInOut == 1)
            _hide = Mathf.Clamp(_hide - 0.04f, -1.0f, 1.0f);
        else if (_hideInOut == 2)
        {
            _hide = Mathf.Clamp(_hide + 0.04f, -1.0f, 1.0f);
        }

        _witch.WitchModel.material.SetFloat("_Hide", Mathf.Clamp(_hide, -1.0f, 1.0f));
        _witch.WeaponModel.material.SetFloat("_Hide", Mathf.Clamp(_hide, -1.0f, 1.0f));
        _witch.RingModel.material.SetFloat("_Hide", Mathf.Clamp(_hide, -1.0f, 1.0f));

        //_hideTime = 0.0f;
    }

    public void OnSkill(Transform target, int type = 0)
    {
        if (_skill.IsOn)
            return;

        _witch.WitchModel.material = _witch.NoiseWitch;
        _witch.WeaponModel.material = _witch.NoiseWeapon;
        _witch.RingModel.material = _witch.NoiseRing;
        _witch.WitchModel.material.SetFloat("_Hide", 1.0f);
        _witch.WeaponModel.material.SetFloat("_Hide", 1.0f);
        _witch.RingModel.material.SetFloat("_Hide", 1.0f);

        _hide = 1.0f;
        _witch.IsTel = true;
        _outActive = false;
        _witchY = _witch.transform.position.y;
        _skill.IsOn = true;
        _state = 1;
        _target = target;
        _teleportTime = 0.0f;
        _type = type;
        _init = false;
        _hideInOut = 0;
        _posChange = false;
        //_in.SetActive(true);
        //Vector3 telPos = _witch.transform.position;
        //telPos.y = _in.transform.position.y;
        //_in.transform.position = telPos;
        //_out.transform.position = telPos;
        //_out.SetActive(false);
        Vector3 effectPos = new Vector3(_witch.transform.position.x, _witchY - 2.5f, _witch.transform.position.z);

        EffectManager.I.OnEffect(EffectType.Witch_Teleport, effectPos, _witch.transform.rotation, 3.0f, 1);
        SoundManager.I.PlaySound(transform, PlaySoundId.Boss_Teleport);
        if (_type == 0)
            _destination = Raycast();
        else if (_type == 2)
        {
            _destination = Vector3.zero;
            _destination.y = _witch.transform.position.y;
        }
    }

    private void TeleportUpdate()
    {
        _teleportTime += Time.deltaTime;

        //if (_teleportTime < 0.3f)
        //    return;

        _hideInOut = 1;

        //if (_teleportTime < 0.4f)
        //    return;

        //_hideInOut = 0;

        if (_teleportTime < 1.0f)
            return;

        if (!_posChange)
        {
            _witch.transform.Translate(Vector3.down * 100.0f);
            _posChange = true;
        }

        _hideInOut = 0;

        if (_teleportTime < 1.5f)
            return;

        if (_type == 1 &&
            !_init)
        {
            _destination = _witch.Target.transform.position + (GetRandomDirection() * 
                (_witch.Stat.AttackDistance + 0.0f));
            _destination.y = _witchY;
            _init = true;
        }

        if (!_outActive)
        {
            Vector3 effectPos = _destination;
            effectPos.y -= 2.5f;
            EffectManager.I.OnEffect(EffectType.Witch_Teleport, effectPos, _witch.transform.rotation, 3.0f, 1);
            SoundManager.I.PlaySound(transform, PlaySoundId.Boss_Teleport);
            _outActive = true;
        }

        //_in.SetActive(false);
        //_out.SetActive(true);
        //Vector3 telPos = _destination;
        //telPos.y = _out.transform.position.y;
        //_out.transform.position = telPos;

        _witch.transform.position = _destination;
        Vector3 target = _witch.Target.transform.position;
        target.y = _witch.transform.position.y;
        _witch.RotateToTarget(target);

        _hideInOut = 2;

        if (_teleportTime < 2.2f)
            return;

        _skill.IsOn = false;
        _witch.IsTel = false;
        _hideInOut = 0;
        _state = 2;
        _witch.WitchModel.material = _witch.NormalWitch;
        _witch.WeaponModel.material = _witch.NormalWeapon;
        _witch.RingModel.material = _witch.NormalRing;
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 randDir = Vector3.zero;

        while (true)
        {
            int x = Random.Range(-1, 2);
            int z = Random.Range(-1, 2);

            if (x == 0 &&
                z == 0)
                continue;

            randDir = new Vector3(x, 0.0f, z);
            break;
        }

        return randDir;
    }

    private Vector3 Raycast()
    {
        Vector3 pos = _witch.transform.position;
        pos.y += 1.0f;
        Ray ray = new Ray(pos, _witch.transform.forward * -1.0f);
        // hit 
        RaycastHit hit = new RaycastHit();
        // 거리
        float dir;

        Debug.DrawRay(ray.origin, ray.direction, Color.red, Mathf.Infinity);

        // 레이캐스트를 통해 어느 객체에 맞았을 경우 (범위는 캐릭터 거리 이동만큼)
        if (Physics.Raycast(ray, out hit, 8.0f, 1 << LayerMask.NameToLayer("BG")))
        {
            return (_witch.transform.forward * 8.0f) + _witch.transform.position;
        }

        return ((_witch.transform.forward * -1) * 8.0f) + _witch.transform.position;
    }
}
