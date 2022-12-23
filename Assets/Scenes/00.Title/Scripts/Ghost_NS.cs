using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플래쉬랑 트리거충돌되면 유령 활성화
// 유령 활성화 되면 유령 스크립트에서 러프로 플레이어 뒤쪽으로 유령 이동
public class Ghost_NS : MonoBehaviour
{
    SkinnedMeshRenderer ghost;

    void Start()
    {
        ghost = GetComponentInChildren<SkinnedMeshRenderer>();
        ghost.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flash")
        {
            ghost.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Flash")
        {
            ghost.enabled = false;
        }
    }


    internal void BottleFall()
    {
        ghost.enabled = true;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        Vector3 dir = UnityEngine.Random.insideUnitSphere;

        dir.y = Mathf.Abs(dir.y) * 15;  // 10을 곱해서 위 방향 벡터를 더 키우고싶다.
        dir.Normalize();
        rb.AddForce(dir * 3, ForceMode.Impulse);
        rb.AddTorque(dir * 10, ForceMode.Impulse);
        transform.localScale = Vector3.one * 1.5f;

        // 3. xx초 후에 파괴되게 하고싶다.
        Destroy(gameObject, 3f);
    }
}
