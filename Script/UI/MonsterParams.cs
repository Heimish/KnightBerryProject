using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonsterParams : CharacterUI
{
    MonsterStat _monster;

    public string names { get; set; }
    public Image GDHPBar;
    public GameObject UIbar;
 
    public override void InitParams()
    {
        names = "GuardGoblin";
        maxHP = _monster.MaxHp;
        curHP = _monster.Hp;
    }

    private void Awake()
    {
        _monster = GetComponentInParent<MonsterStat>();
    }

    public void SetHp()
    {
        curHP = _monster.Hp;
        curHP = Mathf.Clamp(curHP, 0, maxHP);
    }

    public void HPBarSet()
    {
        float _hp = curHP / maxHP;
        GDHPBar.fillAmount = curHP / maxHP;

        if (curHP <= 0)
            UIbar.SetActive(false);
    }

    public void CameraSet()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }

	void Update () {
        // 카메라 바라보게 함
        CameraSet();

        //몬스터 HP 값 담기
        SetHp();

        //몬스터 HP 실시간 로컬스케일 및 HP가 0이면 삭제
        HPBarSet();
    }
}
