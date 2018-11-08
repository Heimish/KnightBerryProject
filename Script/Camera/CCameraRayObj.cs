using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CCameraRayObj : MonoBehaviour
{
   public static CCameraRayObj _instance = null;
    public float MaxDistanceValue = 4.0f;
    // 카메라 Joom out 범위 설정
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    // 속도
    public float smooth = 10.0f;
    Vector3 dollyDir;

    public Transform Player;
    // 거리
    public float distance;

    private int _layerMask;

    private void Awake()
    {
        maxDistance = MaxDistanceValue;
        _layerMask = 1 << LayerMask.NameToLayer("BG");
        CCameraRayObj._instance = this;
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    private void Update()
    {
        Vector3 destiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;

        // 레이캐스트를 통해서 모든 오브젝트를 체크하여 카메라를 앞으로 줌함
        if (Physics.Linecast(transform.parent.position, destiredCameraPos, out hit, _layerMask))
        {
            //if (hit.collider.tag == "Player" || hit.collider.tag == "Boss" || hit.collider.tag == "Sword")
            //    return;
            distance = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
        }
        else
        {
            // 충돌한 물체가 없을시 카메라를 해당 값까지 아웃함
            distance = maxDistance;
        }
        // 좀더 부드러운 처리를위해 Lerp를 사용 (joom, out)을 했을때 포지션값 이동
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }

    public float MaxCamera(float type)
    {
        maxDistance = type;
        return maxDistance;
    }
    public float MinCamera(float type)
    {
        minDistance = type;
        return minDistance;
    }
}
