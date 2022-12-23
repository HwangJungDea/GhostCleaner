using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatchCheck : MonoBehaviour
{
    public List<GameObject> enemyInButtle; // 유령아이콘 UI 연결



    void Start()
    {
        
    }

    void Update()
    {
        // 유령 잡은 수가 1씩 증가할때마다 아이콘도 1개씩 회색처리
        switch (EnemyUICatch.instance.catchCount)
        {
            case 1:
                print("병에 있는 유령 출현.");
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
                // 다 잡았음 -> Clear랑 연결시키기
                break;
        }
    }
}
