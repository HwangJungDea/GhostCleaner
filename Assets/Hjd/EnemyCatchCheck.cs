using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatchCheck : MonoBehaviour
{
    public List<GameObject> enemyInButtle; // ���ɾ����� UI ����



    void Start()
    {
        
    }

    void Update()
    {
        // ���� ���� ���� 1�� �����Ҷ����� �����ܵ� 1���� ȸ��ó��
        switch (EnemyUICatch.instance.catchCount)
        {
            case 1:
                print("���� �ִ� ���� ����.");
                enemyInButtle[4].SetActive(true);
                break;
            case 2:
                enemyInButtle[3].SetActive(true);
                break;
            case 3:
                enemyInButtle[2].SetActive(true);
                break;
            case 4:
                enemyInButtle[1].SetActive(true);
                break;
            case 5:
                enemyInButtle[0].SetActive(true); //.GetComponent<GameObject>()
                // �� ����� -> Clear�� �����Ű��
                break;
        }
    }
}
