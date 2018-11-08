using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerParams : CharacterUI
{
    public static PlayerParams _instance = null;

    protected UiManager uiManager;
    protected CPlayerManager _CPlayerManager;
    protected CPlayerManager CPlayerManager { get { return _CPlayerManager; } set { _CPlayerManager = value; } }

    [Header("HP")]
    public GameObject HPCheck;
    public Image HPBar;
    public string names { get; set; }

    [Header("Swap")]
    public GameObject EffectFull;
    public GameObject EffectDealer;
    public Image SwapBar;
    public Image[] SwapOn;
    public Image[] SwapOff;
    public Image[] SwapGaugeFull;

    [Header("Skill")]
    public GameObject NowisTanker;
    public GameObject NowisDealer;
    public Image Defense;
    public Image[] DefenseOn;
    public Image[] DefenseOff;
    public Image Roll;
    public Image[] RollOn;
    public Image[] RollOff;
    public Image Rush;
    public Image[] RushOn;
    public Image[] RushOff;
    public Image Heal;
    public Image[] HealOn;
    public Image[] HealOff;
    public Image Blink;
    public Image[] BlinkOn;
    public Image[] BlinkOff;
    public Image Downward;
    public Image[] DownwardOn;
    public Image[] DownwardOff;

    [Header("Tab")]
    public GameObject Skill_Explanation;
    public Image Return_Explanation;
    public Image TankerControl;
    public Image DealerControl;
    [System.NonSerialized]
    public bool Explanation_On = false;

    [Header("Tip")]
    public Image Tanker_Icon;
    public Image Dealer_Icon;
    public Image[] Tanker_Tip;
    public Image[] Dealer_Tip;
    private int RandomValue = 0;
    private float TipTime = 10f;
    private bool SwapCheck = false;

    public override void InitParams()
    {
        PlayerParams._instance = this;

        names = "Player";
        maxHP = CPlayerManager._instance.m_PlayerMaxHp;
        curHP = maxHP;
        maxRush = InspectorManager._InspectorManager.ShieldSkillCoolTime;
        curRush = maxRush;
        maxDownward = InspectorManager._InspectorManager.ShieldSkillCoolTime;
        curDownward = maxDownward;
        maxSwap = InspectorManager._InspectorManager.SwapMaxGauge;
        curSwap = CPlayerManager._instance.ScytheGauge;
        maxRoll = InspectorManager._InspectorManager.ShieldShiftCoolTime;
        curRoll = maxRoll;
        maxBlink = InspectorManager._InspectorManager.ScytheShiftCoolTime;
        curBlink = maxBlink;
    }
   
    void Awake()
    {
        HPBar = GameObject.FindGameObjectWithTag("HP").GetComponentInChildren<Image>();       
    }

    public void HpSet()
    {
        curHP = Mathf.Clamp(CPlayerManager._instance.m_PlayerHp, 0, maxHP);
        HPBar.fillAmount = curHP / maxHP;
    }

    public void PC_HpCheck()
    {
        if (curHP <= 0)
            HPCheck.SetActive(false);
    }

    public void Skill_SwapSet()
    {
        curSwap = Mathf.Clamp(CPlayerManager._instance.ScytheGauge, 0, maxSwap);
        SwapBar.fillAmount = curSwap / maxSwap;
    }

    public void Skill_SwapImageSet()
    {
        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            if (SwapBar.fillAmount == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    SwapOff[i].enabled = false;
                }

                SwapGaugeFull[0].enabled = true;
                SwapGaugeFull[1].enabled = false;
                SwapOn[0].enabled = true;
                SwapOn[1].enabled = true;
                EffectFull.SetActive(true);
            }

            else
            {
                for (int i = 0; i < 3; i++)
                {
                    SwapOff[i].enabled = true;
                }

                SwapGaugeFull[0].enabled = false;
                SwapGaugeFull[1].enabled = true;
                SwapOn[0].enabled = false;
                SwapOn[1].enabled = false;
                EffectFull.SetActive(false);
                EffectDealer.SetActive(false);
            }
        }

        else if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
        {
            for (int i = 0; i < 3; i++)
            {             
                SwapOff[i].enabled = false;
            }

            SwapOn[0].enabled = true;
            SwapOn[1].enabled = true;
            SwapGaugeFull[0].enabled = false;
            SwapGaugeFull[1].enabled = true;
            EffectFull.SetActive(false);
            EffectDealer.SetActive(true);
        }

    }
    public void Skill_ExplanationSet()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            Explanation_On = !Explanation_On;
        Skill_Explanation.SetActive(true);
    }

    public void Skill_ExplanationImageSet()
    {
        if (!Explanation_On)
        {
            Skill_Explanation.SetActive(false);
            Return_Explanation.enabled = false;
            Time.timeScale = 1f;
        }

        if (Explanation_On)
        {
            Time.timeScale = 0;
            Return_Explanation.enabled = true;

            if(CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
            {
                TankerControl.enabled = true;
                DealerControl.enabled = false;
            }

            else
            {
                DealerControl.enabled = true;
                TankerControl.enabled = false;
            }
        }
    }

    public void Skill_GaugeSet()
    {
        if(CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            NowisTanker.SetActive(true);
            NowisDealer.SetActive(false);
        }

        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
        {
            NowisDealer.SetActive(true);
            NowisTanker.SetActive(false);
        }
    }

    public void Skill_GaugeImageSet()
    {
        // Tanker
        Defense.fillAmount = 1 / 1;
        curRoll = Mathf.Clamp(CPlayerManager._instance._PlayerAni_Contorl.SheildShiftCool, 0, maxRoll);
        Roll.fillAmount = 1 - (curRoll / maxRoll);
        curRush = Mathf.Clamp(CPlayerManager._instance._PlayerAni_Contorl.RushSkillcool, 0, maxRush);
        Rush.fillAmount = 1 - (curRush / maxRush);

        if(Defense.fillAmount == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                DefenseOn[i].enabled = true;
            }

            DefenseOff[0].enabled = false;
            DefenseOff[1].enabled = false;
        }

        if(Defense.fillAmount < 1)
        {
            for (int i = 0; i < 3; i++)
            {
                DefenseOn[i].enabled = false;
            }

            DefenseOff[0].enabled = true;
            DefenseOff[1].enabled = true;
        }

        if (Roll.fillAmount == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                RollOn[i].enabled = true;
            }

            RollOff[0].enabled = false;
            RollOff[1].enabled = false;
        }

        if (Roll.fillAmount < 1)
        {
            for (int i = 0; i < 3; i++)
            {
                RollOn[i].enabled = false;
            }

            RollOff[0].enabled = true;
            RollOff[1].enabled = true;
        }

        if (Rush.fillAmount == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                RushOn[i].enabled = true;
            }

            RushOff[0].enabled = false;
            RushOff[1].enabled = false;
        }

        if (Rush.fillAmount < 1)
        {
            for (int i = 0; i < 3; i++)
            {
                RushOn[i].enabled = false;
            }

            RushOff[0].enabled = true;
            RushOff[1].enabled = true;
        }

        // Dealer
        Heal.fillAmount = 1 / 1;
        curBlink = Mathf.Clamp(CPlayerManager._instance._PlayerAni_Contorl.ScytheShiftCool, 0, maxBlink);
        Blink.fillAmount = 1 - (curBlink / maxBlink);
        curDownward = Mathf.Clamp(CPlayerManager._instance._PlayerAni_Contorl.DownWardSkillCool, 0, maxDownward);
        Downward.fillAmount = 1 - (curDownward / maxDownward);

        if (Heal.fillAmount == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                HealOn[i].enabled = true;
            }

            HealOff[0].enabled = false;
            HealOff[1].enabled = false;
        }

        if (Heal.fillAmount < 1)
        {
            for (int i = 0; i < 3; i++)
            {
                HealOn[i].enabled = false;
            }

            HealOff[0].enabled = true;
            HealOff[1].enabled = true;
        }

        if (Blink.fillAmount == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                BlinkOn[i].enabled = true;
            }

            BlinkOff[0].enabled = false;
            BlinkOff[1].enabled = false;
        }

        if (Blink.fillAmount < 1)
        {
            for (int i = 0; i < 3; i++)
            {
                BlinkOn[i].enabled = false;
            }

            BlinkOff[0].enabled = true;
            BlinkOff[1].enabled = true;
        }

        if (Downward.fillAmount == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                DownwardOn[i].enabled = true;
            }

            DownwardOff[0].enabled = false;
            DownwardOff[1].enabled = false;
        }

        if (Downward.fillAmount < 1)
        {
            for (int i = 0; i < 3; i++)
            {
                DownwardOn[i].enabled = false;
            }

            DownwardOff[0].enabled = true;
            DownwardOff[1].enabled = true;
        }
    }

    public void Tip_Set()
    {
        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            Tanker_Icon.enabled = true;
            Dealer_Icon.enabled = false;

            for (int i = 0; i < Dealer_Tip.Length; i++)
            {
                Dealer_Tip[i].enabled = false;
            }
        }

        else if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
        {
            Tanker_Icon.enabled = false;
            Dealer_Icon.enabled = true;

            for (int i = 0; i < Tanker_Tip.Length; i++)
            {
                Tanker_Tip[i].enabled = false;
            }
        }
    }

    public void Tip_CorSet()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SwapCheck = true;

        TipTime += Time.deltaTime;
        if (TipTime >= 10f || SwapCheck)
        {
            if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
            {
                Tip_TankerSet();
            }

            if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
            {
                Tip_DealerSet();
            }

            if(!SwapCheck)
            TipTime = 0;

            SwapCheck = false;
        }
    }

    public void Tip_TankerSet()
    {
        for (int i = 0; i < Tanker_Tip.Length; i++)
        {
            Tanker_Tip[i].enabled = false;
        }
        RandomValue = Random.Range(0, Tanker_Tip.Length);
        Tanker_Tip[RandomValue].enabled = true;
    }

    public void Tip_DealerSet()
    {
        for (int i = 0; i < Dealer_Tip.Length; i++)
            Dealer_Tip[i].enabled = false;
        RandomValue = Random.Range(0, Dealer_Tip.Length);
        Dealer_Tip[RandomValue].enabled = true;
    }

    void Start()
    {
        InitParams();
        Skill_Explanation.SetActive(false);
    }

    void Update()
    {
        // Player 캐릭터의 체력 값을 받아온다.
        HpSet();
        PC_HpCheck();

        // Skill_Explanation
        Skill_ExplanationSet();
        Skill_ExplanationImageSet();

        // Skill_Gauge
        Skill_GaugeSet();
        Skill_GaugeImageSet();

        // Skill_Swap
        Skill_SwapSet();
        Skill_SwapImageSet();

        // Tip
        Tip_CorSet();
        Tip_Set();
    }
}
