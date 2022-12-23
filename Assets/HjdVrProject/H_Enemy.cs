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
    // ���⼭�� �⺻ �¿� ���������� �ϴ� �س���.

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
        //�⺻������ ���� ������ ���̰� �ȸ����� �Ⱥ��̰� �ؾߵ�.
        //isLight�� ���� �ǵ�°� �ķ������� �ǵ��.


        if (isLight == true && isShoot == true && isEnemyDie == true)
        {
            print("5");
            dir = enemyEndPoint.transform.position - transform.position;
            dir.Normalize();

            transform.position += (dir) * 5f * speed * Time.deltaTime;

            isShoot = false;

        }
        //�̰Ÿ� �۵��ǰ� ������ �ȴ�.

        //�ѿ� 8�ʰ� �¾Ƽ� isLastEnemyLife�� true�� ����.
        //�ϴ��� �ð����� �ذ��ߴµ� ����� ���������� ���� ������ ���ڴ� ��������-
        if (isLastEnemyLife == true && isEnemyDie == false)
        {
            //�̻��¿��� �Ѹ����� �÷��̾����� �޿����� �׾��.
            //���⼭�� ���̻� �̵� ���ϴ°ɷ�.
            dieTime += Time.deltaTime;
            print("3");
            // �ѱ�¾Ƽ� ��������.
            // 

            //if(runEnemyPoint.transform.position == transform.position)
            //���� ���� ��������.
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


        //���� �ȴ��϶��� �⺻ ������. �̰� ���߿� �������� �ֽŰɷ� ������ߵ�.
        if (isLight == false)
        {
            //�⺻������. 
            //���� �������̵� �ӵ� �⺻ ������.

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



        //isLight�� Ʈ���϶�.
        //���� �ְ� �ѱ� �°� ������.
        //�����ѹ��� �������� �̽��ѳ�.
        else if (isLight == true && isEnemyDie == false)
        {
            meshRenderer.enabled = true;
            //�ѿ� �°� ������ isShoot�� ��� true;
            //sin������ �εεε�
            if (isShoot == true)
            {
                print("1");
                repeatTime += Time.deltaTime;

                if (repeatTime > 8)
                {
                    isLastEnemyLife = true;
                    repeatTime = 0; //�ʿ�����ѵ� Ȥ�ø��� �ʱ�ȭ.
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
                    //���⿡ ���� �ð� �߰��ؼ� �͸����� �Ÿ��� �����.
                    dir = enemyEndPoint.transform.position - transform.position;
                    dir.Normalize();

                    transform.position += (dir) * speed * Time.deltaTime;

                }


            }
            isShoot = false;
        }

    }
}
