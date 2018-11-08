using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchValueManager : MonoBehaviour
{
    static private WitchValueManager _instance;
    static public WitchValueManager I
    {
        get
        {
            return _instance;
        }
    }

    // 화살 날라가는거
    public float ArrowSpawnTime = 0.2f; // 화살 뜨는 간격
    public float ArrowFireTime = 0.4f; // 화살 쏘는 간격
    public float ArrowSpeed = 1350.0f; // 화살 날라가는 속도
    public float ArrowDamage = 20.0f; // 화살당 데미지

    // 오브
    public float OrbSpeed = 700.0f; // 오브 날라가는 속도
    public float OrbDamage = 20.0f; // 오브당 데미지
    public float OrbLifeTime = 5.0f; // 오브 라이프타임
    public float OrbDealyTime = 0.5f; // 오브 생기고 대기시간
    public float OrbNextTime = 3.0f; // 오브 다음 시간

    // 발판
    public float FootholdNextTime = 0.5f; // 다음 발판 나오는 간격
    public float FootholdDamage = 20.0f; // 발판 데미지
    public float FootholdDuration = 1.0f;
    public float FootholdContinueTime = 10.0f; // 다음 발판 나오는 시간

    public float CloseAttack0Damage = 20.0f; // 찌르기 데미지
    public float CloseAttack1Damage = 20.0f; // 베기 데미지
    public float TeleportDamage = 40.0f; // 얼마나 쳐맞으면 텔포하는지
    public float GroggyDuration = 5.0f; // 그로기 지속시간
    public float GroggyDeleteTime = 10.0f; // 그로기 수치 없어지는 시간
    public float GroggyMaxValue = 100.0f; // 그로기가 되는 수치
    public float HitAnimStopDuration = 0.1f; // 쳐 맞으면 애니메이션 정지 지속시간

    public float MonsterSpawnTime = 40.0f; // 몬스터 스폰 시간

    public float ReleaseDamage = 30.0f;

    public float[] AttackDamge = new float[] { 20.0f, 20.0f, 20.0f };
    public float[] AttackMinAngle = new float[] { 0.7f, 0.0f, 0.7f };
    public float[] AttackMaxAngle = new float[] { 1.0f, 1.0f, 1.0f };
    public float[] AttackDistance = new float[] { 5.0f, 5.0f, 5.0f };

    private void Awake()
    {
        _instance = this;
    }

    void Start ()
    {

	}
	
	void Update ()
    {
		
	}
}
