using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillFootholdFire : MonoBehaviour
{
    private Vector3 _target;
    private int _state;
    private Vector3 _readyPos;
    private GameObject[] _fires;
    private float _footholdDuration;
    private float _budeulDuration;
    private CapsuleCollider _collider;

    private void Awake()
    {
        _fires = new GameObject[2];
        _fires[0] = transform.Find("Foothold").gameObject;
        _fires[1] = transform.Find("Explosion").gameObject;
        _collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        switch (_state)
        {
            case 1:
                FootholdUpdate();
                break;
            case 2:
                break;
        }
    }

    public void OnFire(Transform target)
    {
        _collider.enabled = false;
        _state = 1;
        _target = target.position;
        transform.position = _target;
        _footholdDuration = 0.0f;
        _budeulDuration = 0.0f;
        _fires[0].SetActive(true);
        _fires[1].SetActive(false);
    }

    private void FootholdUpdate()
    {
        _footholdDuration += Time.deltaTime;

        if (_footholdDuration < WitchValueManager.I.FootholdDuration)
            return;

        _fires[0].SetActive(false);
        _fires[1].SetActive(true);
        SoundManager.I.PlaySound(transform, PlaySoundId.Boss_FootHold);

        _footholdDuration = 0.0f;
        _state = 2;

        StartCoroutine(Co_Collider());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CPlayerManager._instance.PlayerHp(0.2f, 1, WitchValueManager.I.FootholdDamage);
        }
    }

    private IEnumerator Co_Collider()
    {
        _collider.enabled = true;
        yield return new WaitForSeconds(1.8f);
        _collider.enabled = false;
        StartCoroutine(Co_Active());
    }

    private IEnumerator Co_Active()
    {
        yield return new WaitForSeconds(0.9f);
        gameObject.SetActive(false);
    }
}
