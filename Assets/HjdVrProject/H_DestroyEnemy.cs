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

            //상태들어갈떄
            //에너미 마지막 포인트로 이동
            //이때 점수1점 증가.
            //이걸로 주렁주렁이 하나씩 증가.

        }
    }
}
