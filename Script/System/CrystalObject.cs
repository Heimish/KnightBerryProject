using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalObject : MonoBehaviour
{
    [SerializeField]
    private Camera _viewCamera;

    [SerializeField]
    private Camera _centerCamera;

    private GameObject _crystal;
    private GameObject _beam;

    [SerializeField]
    private StatueObject _statue;

    //[SerializeField]
    //private GameObject _destroyEffect;
    //[SerializeField]
    //private GameObject _beamReady;
    //[SerializeField]
    //private GameObject _destroyAfterEffect;

    //[SerializeField]
    //private GameObject _beam;
    //[SerializeField]
    //private GameObject _beamObejct;

    private int _state;
    private bool _isCrash;
    private bool _on;
    private bool _active;

    private float _currentValue;
    private float _maxValue;

    // properties
    public Camera ViewCamera { get { return _viewCamera; } }
    public float CurrentValue { get { return _currentValue; } }
    public float MaxValue { get { return _maxValue; } }

	void Start ()
    {
        _beam = transform.Find("UpBeam").gameObject;
        _beam.SetActive(false);
        _crystal = transform.Find("Crystal").gameObject;
        _viewCamera.gameObject.SetActive(false);
        _centerCamera.gameObject.SetActive(false);
        _maxValue = 3.0f;
    }
	
	void Update ()
    {
        switch (_state)
        {
            case 0:
                KeyInput();
                OnUpdate();
                break;
            case 1:

                break;
        }

    }

    private void KeyInput()
    {
        if (!_isCrash)
            return;

        if (_active)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            CPlayerManager._instance._PlayerAni_Contorl.InteractionOn();
            Vector3 pos = transform.position;
            pos.y = CPlayerManager._instance.transform.position.y;
            CPlayerManager._instance.transform.LookAt(pos);
            CPlayerManager._instance._PlayerAni_Contorl.InteractionOn();
            _on = true;
            _statue.Crystal = this;
        }
    }

    private void OnUpdate()
    {
        if (!_on)
            return;

        if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Shild
            != PlayerAni_State_Shild.Interaction)
        {
            _on = false;
            _currentValue = 0.0f;
            return;
        }

        _currentValue += Time.deltaTime;

        if (_currentValue < 1.0f)
            return;

        CPlayerManager._instance._PlayerAni_Contorl.PlayerAniFile.speed = 0.0f;

        if (_currentValue < _maxValue)
            return;

        CPlayerManager._instance._PlayerAni_Contorl.PlayerAniFile.speed = 1.0f;
        _beam.SetActive(true);
        _crystal.SetActive(false);

        _state = 1;
        _on = false;

        _viewCamera.depth = 0;
        _viewCamera.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _isCrash = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _isCrash = false;
    }
}
