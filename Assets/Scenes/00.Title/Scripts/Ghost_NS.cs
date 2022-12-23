using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷����� Ʈ�����浹�Ǹ� ���� Ȱ��ȭ
// ���� Ȱ��ȭ �Ǹ� ���� ��ũ��Ʈ���� ������ �÷��̾� �������� ���� �̵�
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

        dir.y = Mathf.Abs(dir.y) * 15;  // 10�� ���ؼ� �� ���� ���͸� �� Ű���ʹ�.
        dir.Normalize();
        rb.AddForce(dir * 3, ForceMode.Impulse);
        rb.AddTorque(dir * 10, ForceMode.Impulse);
        transform.localScale = Vector3.one * 1.5f;

        // 3. xx�� �Ŀ� �ı��ǰ� �ϰ�ʹ�.
        Destroy(gameObject, 3f);
    }
}
