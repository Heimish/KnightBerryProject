using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 낫, 검방패 모드
public enum PlayerMode
{
    Shield = 0,
    Scythe,
}
public class CPlayerSwap : CPlayerBase
{
    public Shader afterimageShader;
    public Color tankerColor;
    public Color dealerColor;

    public PlayerMode _PlayerMode = PlayerMode.Shield; // 처음엔 검방패로 시작

    // 플레이어 좌표 담아둠
    public Transform _Follow;
    // _Shield 배열에 낫,검방패 오브젝트 넣어둠
    public GameObject[] _EffectModle;
    // 플레이어 텔포 이펙트
    public GameObject[] _EffectTelpo;


    public bool m_bSwapAttack;
    private float m_fSwapAttackTime;

    // 이펙트 on일때 사라지는 시간
    private bool isEffect;
    private bool isEffectTelpo;
    private float fEffectTime;

    // 블링크
    private float m_fDisMin;
    public float m_fMoveDir;
    private int nScytheNum;
    private int nScytheExponential;

    // 직업변경 쿨타임 
    //private bool isCoolTimeSwap;
    //public bool _isCoolTimeSwap { get { return isCoolTimeSwap; } set { value = isCoolTimeSwap; } }

    public bool isBlink;
    private float fBlinkTime;

    private bool isBlinkSwapKey;
    private bool isBlinkOut;

    void Start()
    {
        m_fDisMin = 1.5f;
        m_fMoveDir = 5.0f;

        fEffectTime = 0;
        m_bSwapAttack = false;
        m_fSwapAttackTime = 0.0f;

        nScytheNum = 0;
        nScytheExponential = 1;

        isBlink = false;
        isBlinkSwapKey = false;
        isBlinkOut = true;
    }
    void Update()
    {
        SwapKey();
        //EffectTimer();
        SwapAttacker();
        TelPoEffect();
        BlinkPlayer();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_PlayerMode != PlayerMode.Scythe)
                return;

            if (!_PlayerManager._PlayerAni_Contorl.ScytheShiftOn)
                return;

            CSwapSystem._instance.ObjSwap(false, false);
            CCameraFind._instance.BlinkCameraOn();
            RayCastChack();
            BlinkStart();
            SoundManager.I.PlaySound(transform, PlaySoundId.Dealer_Blink);
            _PlayerManager._PlayerAni_Contorl.ScytheShiftCool = InspectorManager._InspectorManager.ScytheShiftCoolTime;
        }
    }

    void SwapKey()
    {
        if (_PlayerManager.isDead)
            return;

        // Q버튼을 누르면 직업이 바뀜
        if (_PlayerMode == PlayerMode.Shield)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!_PlayerManager.CanSwap)
                    return;

                if (_PlayerManager._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Dash)
                    return;

                ScytheReset(1);
                //PlayerParams._instance.nGauge = 0;
                //PlayerParams._instance.GaugeOff(); // 수정하기
            }
            //else if (Input.GetKeyDown(KeyCode.E))
            //{
            //    AfterImageSet();
            //    ScytheReset();
            //}
        }
        else
        {
            _PlayerManager.ScytheGauge -= Time.deltaTime * 5.0f;
            if (!isBlinkSwapKey)
            {
                if (_PlayerManager._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill1 ||
                    _PlayerManager._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    return;

                if (Input.GetKeyDown(KeyCode.Q) ||
                    _PlayerManager.ScytheGauge <= 0.0f)
                {
                    ShildReset(0);
                }

                //else if (Input.GetKeyDown(KeyCode.E))
                //{
                //    AfterImageSet();
                //    ShildReset(1);
                //}
            }
        }
    }

    void EffectTimer()
    {
        if (!isEffect)
        {
            fEffectTime = 0;
            return;
        }

        if (isEffect)
        {
            fEffectTime += Time.deltaTime;
            if (fEffectTime >= 2.0f)
            {
                isEffect = false;
            }
        }
    }
    void TelPoEffect()
    {
        if (!isEffectTelpo)
        {
            return;
        }

        if (isEffectTelpo)
        {
            // 이펙트
            EffectManager.I.EventOnEffect(15);
            isEffectTelpo = false;
        }
    }

    private void AfterImageSet()
    {
        GameObject obj = null;
        Renderer[] renderers;
        Collider[] colliders;

        if (_PlayerMode == PlayerMode.Shield)
        {
            obj = Instantiate(transform.Find("NewNew_Berry").gameObject);
            obj.transform.position = transform.position;
            StartCoroutine(Co_AfterImageOn(obj, 0));
        }
        else
        {
            obj = Instantiate(transform.Find("Devil_Berry").gameObject);
            obj.transform.position = transform.position;
            StartCoroutine(Co_AfterImageOn(obj, 1));
        }

        obj.AddComponent<PlayerAfterImage>().InitRenderer();
        renderers = obj.transform.GetComponentsInChildren<Renderer>();
        colliders = obj.transform.GetComponentsInChildren<Collider>();

        foreach (Collider coll in colliders)
        {
            coll.enabled = false;
        }

        foreach (Renderer render in renderers)
        {
            Material mat = new Material(afterimageShader);
            mat.CopyPropertiesFromMaterial(render.material);
            mat.SetFloat("_Rimpower", 3.5f);

            if (_PlayerMode == PlayerMode.Shield)
                mat.SetColor("_Color", tankerColor);
            else
                mat.SetColor("_Color", dealerColor);

            render.material = mat;
        }
    }

    void SwapAttacker()
    {
        //if (!m_bSwapAttack)
        //{
        //    m_fSwapAttackTime = 0;
        //    return;
        //}

        if (m_bSwapAttack)
        {
            m_fSwapAttackTime += Time.deltaTime;
            _PlayerManager._PlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.Skill1;
            //if (m_fSwapAttackTime >= 0.1f)
            //{
            //    CPlayerManager._instance.PlayerHitCamera2(2.0f);
            //}
            if (m_fSwapAttackTime >= 0.4f)
            {
                m_fSwapAttackTime = 0.0f;
                m_bSwapAttack = false;
            }
        }
    }

    public void RayCastChack()
    {
        isEffectTelpo = true;
        // 레이 쏠위치, 방향 -> 캐릭터는 계속 직선만 보기때문에 forward를 사용함
        Ray ray = new Ray(_Follow.transform.position, _Follow.transform.forward);
        // hit 
        RaycastHit hit = new RaycastHit();
        // 거리
        float dir;

        Debug.DrawRay(ray.origin, ray.direction, Color.red, Mathf.Infinity);

        // 레이캐스트를 통해 어느 객체에 맞았을 경우 (범위는 캐릭터 거리 이동만큼)
        //if (Physics.Raycast(ray, out hit, m_fMoveDir))
        //{
        //    // 거리 =  (현재좌표 - 레이캐스트 맞은 객체좌표) - m_fDisMin
        //    // 거리 구한값에서 m_fDisMin를 빼주는 이유
        //    // 벽에서 사용하면 hit좌표의 끝에 걸려 위로 올라가기때문에 벽보다 좀뒤의 좌표로 계산
        //    dir = Vector3.Distance(transform.position, hit.point) - m_fDisMin;
        //    // 현재거리가 m_fDisMin거리보다 작을경우 
        //    if (dir > m_fDisMin)
        //    {
        //        // 캐릭터의 위치를 dir 거리만큼 증가
        //        transform.position += transform.forward * dir;
        //    } // 아닐경우 함수호출중단
        //    else
        //        return;
        //}
        //else
        //{
        transform.position += transform.forward * m_fMoveDir;
        //}
    }

    void BlinkStart()
    {
        isBlinkOut = true;
        isBlinkSwapKey = true;
        isBlink = true;
        fBlinkTime = 0;
    }

    void BlinkPlayer()
    {
        if (!isBlink)
            return;


        fBlinkTime += Time.deltaTime;

        if (fBlinkTime > 0.4f)
        {
            isBlinkSwapKey = false;
            CSwapSystem._instance.ObjSwap(false, true);
            if (isBlinkOut)
            {
                EffectManager.I.EventOnEffect(16);
                isBlinkOut = false;
            }
        }
        if (fBlinkTime > 0.8f)
        {
            //_EffectTelpo[0].SetActive(false);
        }
        if (fBlinkTime > 2.0f)
        {
            // _EffectTelpo[1].SetActive(false);
            isBlink = false;
        }
    }



    void ScytheReset(int type = 0)
    {
        CSwapSystem._instance.ScytheObjs(type);
        //StartCoroutine("ScytheHpDown");
        _PlayerMode = PlayerMode.Scythe;
        SoundManager.I.PlaySound(transform, PlaySoundId.Dealer_Swap);
        if (type == 1)
        {
            EffectManager.I.EventOnEffect(9);
            //EffectManager.I.EventOnEffect(8);
            _PlayerManager.isPull = true;
            SoundManager.I.PlaySound(transform, PlaySoundId.Dealer_Holding);
            m_bSwapAttack = true;
        }
        Common();
    }
    void ShildReset(int idx)
    {
        //_PlayerManager.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.8f);

        CSwapSystem._instance.ShildObjs();
        //StopCoroutine("ScytheHpDown");
        nScytheNum = 0;
        nScytheExponential = 1;
        _PlayerMode = PlayerMode.Shield;
        SoundManager.I.PlaySound(transform, PlaySoundId.Tanker_Swap);
        if (idx == 0)
        {
            EffectManager.I.EventOnEffect(10);
            _PlayerManager.isPush = true;
            SoundManager.I.PlaySound(transform, PlaySoundId.Tanker_Knockback);
        }
        isBlink = false;
        Common();
        _PlayerManager.ScytheGauge = 0.0f;
    }
    void Common()
    {
        //_PlayerManager.SwapHpType((int)_PlayerMode + 1);
        _PlayerManager._CPlayerAniEvent.MoveOn();
        isEffect = true;
    }


    //IEnumerator ScytheHpDown()
    //{
    //    while(true)
    //    {
    //        float hp = InspectorManager._InspectorManager.fScytheTimeHpDown;
    //        yield return new WaitForSeconds(InspectorManager._InspectorManager.fScytheTime);

    //        nScytheNum++;
    //        if(nScytheNum >= InspectorManager._InspectorManager.fScytheTimeHpExponential)
    //        {
    //            nScytheExponential = nScytheExponential * 2;
    //            nScytheNum = 0;
    //        }
    //        else
    //        {
    //            _PlayerManager.PlayerHp(0, 3, hp * nScytheExponential);
    //        }
    //    }
    //}
    //IEnumerator CoolTimeSwap()
    //{
    //    yield return new WaitForSeconds(InspectorManager._InspectorManager.fSwapCoolTime);        
    //    isCoolTimeSwap = true;
    //}

    IEnumerator Co_AfterImageOn(GameObject obj, int idx)
    {
        yield return new WaitForSeconds(InspectorManager._InspectorManager.AfterImageDeadTime);

        obj.GetComponent<PlayerAfterImage>().EndAfterImage();
        Vector3 pos = obj.transform.position;
        pos.y += 1.0f;
        if (idx == 0)
            EffectManager.I.OnEffect(EffectType.Tanker_AfterImage, pos, obj.transform.rotation, 1.0f, 1);
        else
            EffectManager.I.OnEffect(EffectType.Dealer_AfterImage, pos, obj.transform.rotation, 1.0f, 1);
    }
}

