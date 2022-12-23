using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Enemy : MonoBehaviour
{
    [Header("EnemyState")]
    public float speed = 5f;

    [Header("CheckList")]
    public bool isLight;
    public bool isShoot;
    public bool isLastEnemyLife;

    public bool isEnemyDie;


    [Header("PointList")]
    public Transform enemyEndPoint;
    public Transform runEnemyPoint;

    MeshRenderer meshRenderer;

    Vector3 dir = Vector3.right;
    float curTime;
    float repeatTime;
    float dieTime;
    // 여기서는 기본 좌우 움직임으로 일단 해놓기.

    void Start()
    {
        curTime = 0;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }


    void Update()
    {
        if (isLight == false)
        {
            meshRenderer.enabled = false;
        }

        if (isLight == true)
        {
            meshRenderer.enabled = true;
        }
        //기본적으로 빛에 맞으면 보이고 안맞으면 안보이게 해야됨.
        //isLight를 직접 건드는건 후레쉬에서 건들기.


        if (isLight == true && isShoot == true && isEnemyDie == true)
        {
            print("5");
            dir = enemyEndPoint.transform.position - transform.position;
            dir.Normalize();

            transform.position += (dir) * 5f * speed * Time.deltaTime;

            isShoot = false;

        }
        //이거만 작동되게 만들어야 된다.

        //총에 8초간 맞아서 isLastEnemyLife가 true된 상태.
        //일단은 시간으로 해결했는데 요놈이 도착했을때 저게 됬으면 좋겠단 말이지ㄱ-
        if (isLastEnemyLife == true && isEnemyDie == false)
        {
            //이상태에서 총맞으면 플레이어한테 쭈우우욱가서 죽어라.
            //여기서는 더이상 이동 안하는걸로.
            dieTime += Time.deltaTime;
            print("3");
            // 뚜까맞아서 도망갈떄.
            // 

            //if(runEnemyPoint.transform.position == transform.position)
            //보류 조금 오류있음.
            if (dieTime > 4)
            {
                print("4");

                isEnemyDie = true;
                isLastEnemyLife = false;
            }
            dir = runEnemyPoint.transform.position - transform.position;
            dir.Normalize();

            transform.position += (dir) * 5f * speed * Time.deltaTime;

        }


        //빛이 안닿일때의 기본 움직임. 이건 나중에 덕영님이 주신걸로 덮어씌워야됨.
        if (isLight == false)
        {
            //기본움직임. 
            //랜덤 포지션이든 머든 기본 움직임.

            meshRenderer.enabled = false;
            curTime += Time.deltaTime;
            print("2");

            if (curTime < 2)
            {
                transform.position += dir * speed * Time.deltaTime;
            }
            else if (curTime < 4)
            {
                transform.position += -(dir) * speed * Time.deltaTime;
            }

            else
            {
                curTime = 0;
            }
        }



        //isLight가 트루일때.
        //빛이 있고 뚜까 맞고 있을때.
        //도망한번도 가지않은 싱싱한놈.
        else if (isLight == true && isEnemyDie == false)
        {
            meshRenderer.enabled = true;
            //총에 맞고 있으면 isShoot은 계속 true;
            //sin움직임 두두두둥
            if (isShoot == true)
            {
                print("1");
                repeatTime += Time.deltaTime;

                if (repeatTime > 8)
                {
                    isLastEnemyLife = true;
                    repeatTime = 0; //필요없긴한데 혹시몰라서 초기화.
                }

                else if (repeatTime > 6)
                {

                    dir = enemyEndPoint.transform.position - transform.position;
                    dir.Normalize();

                    transform.position += -(dir) * speed * Time.deltaTime;
                }


                else if (repeatTime > 4)
                {

                    dir = enemyEndPoint.transform.position - transform.position;
                    dir.Normalize();

                    transform.position += (dir) * speed * Time.deltaTime;
                }

                else if (repeatTime > 2)
                {

                    dir = enemyEndPoint.transform.position - transform.position;
                    dir.Normalize();

                    transform.position += -(dir) * speed * Time.deltaTime;
                }


                else
                {
                    //여기에 따로 시간 추가해서 와리가리 거리게 만들기.
                    dir = enemyEndPoint.transform.position - transform.position;
                    dir.Normalize();

                    transform.position += (dir) * speed * Time.deltaTime;

                }


            }
            isShoot = false;
        }

    }
}
