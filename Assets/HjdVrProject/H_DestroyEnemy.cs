using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_DestroyEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Enemy"))
        {
            Destroy(other.gameObject, 1f);

            //���µ���
            //���ʹ� ������ ����Ʈ�� �̵�
            //�̶� ����1�� ����.
            //�̰ɷ� �ַ��ַ��� �ϳ��� ����.

        }
    }
}
