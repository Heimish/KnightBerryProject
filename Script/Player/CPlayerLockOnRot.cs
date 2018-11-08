using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerLockOnRot : MonoBehaviour
{
    public static CPlayerLockOnRot _instance = null;

    private void Start()
    {
        CPlayerLockOnRot._instance = this;
    }
    
    // 락온
    public void LockRot(Transform target)
    {
        // 타겟 좌표 - 현재좌표
        Vector3 curPos = target.position - transform.position;
        // 노멀라이즈 해줌
        curPos.Normalize();
        
        // 현재 각도 저장
        Quaternion q = transform.rotation;
        // x축은 사용하지않으니 0으로 고정
        q.x = 0.0f;
        
        if (Vector3.Distance(transform.position, target.position) < 2.0f)
        {
            transform.rotation = q;
        }
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(curPos), Time.deltaTime * 20f);

    }

}
