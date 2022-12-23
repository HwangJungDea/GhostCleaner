using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//����
//4. �̵�
//5. �������� ���� ��?
//6. #�÷��̾� �ڵ�; ��ġ��
//7. ehp.HP ����� ����� ������??


public class Enemy : MonoBehaviour
{
    public State state;
    public enum State
    {
        hidden,   //�������
        brightened, //����Ʈ �߰�
        run,    //������Ʈ�� �̵���
        death,  //����
    }
    public bool start_Suprise = false;
    public float moveSpeed = 3;

    public float maxTime = 10;
    public float runMotionTime = 1.5f;
    public float attackMoveSpeed;

    public GameObject cage;

    public int ghostNumber;

    public SkinnedMeshRenderer m;

    public Transform target;
    public Transform outside;
    public Transform SurpriseUI;

    bool hitEnabled;
    bool finish;

    EnemyHP ehp;

    float currentTime;

    Transform emptySpot;

    Vector3 Ori_objectPosition;
    Vector3 Des_objectPosition;
    Vector3 dir;



    public float curTime;//������ ���ִµ� �ʿ��� �ð�
    // Start is called before the first frame update
    void Start()
    {
        ehp = gameObject.GetComponent<EnemyHP>();
        m = gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        m.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.hidden: UpdateHidden(); break;
            case State.brightened: UpdateBrightened(); break;
            case State.run: UpdateRun(); break;
            case State.death: UpdateDeath(); break;
            default: break;
        }
    }

    private void UpdateHidden()
    {
        transform.up = Vector3.up;
        transform.forward = Vector3.forward;
        transform.right = Vector3.right;

        if (m.enabled)
        {
            //�ð� �ʱ�ȭ
            currentTime = 0;

            //HP�� ���� �� �ִ� ���º��� true
            hitEnabled = true;

            start_Suprise = true;
            Ori_objectPosition = transform.position;
            Des_objectPosition = transform.position + new Vector3(0, 1, 0);
            SurpriseUI.gameObject.SetActive(true);
            H_SoundManager.instance.On_enemySurprise();
            // �̵����·� �����ϰ�ʹ�.
            state = State.brightened;
        }
    }

    private void UpdateBrightened()
    {

        curTime = curTime + Time.deltaTime;

        transform.LookAt(target.transform.position);

        //���� ���������� �� ���� ����� �ʾ��� ��
        if (start_Suprise && !finish)
        {
            //��ǥ �������� �̵�(���� ��¦)
            transform.position = Vector3.Lerp(transform.position, Des_objectPosition, Time.deltaTime * 8);

            //��ǥ ������ �ٴٸ���
            if (Vector3.Distance(transform.position, Des_objectPosition) < 0.2f)
            {
                finish = true;
            }
        }

        //��ǥ �������� �̵�(�Ʒ��� ��¦)
        if (finish)
        {
            //��ǥ �������� �̵�
            transform.position = Vector3.Lerp(transform.position, Ori_objectPosition, Time.deltaTime * 8);

            //��ǥ ������ �ٴٸ���
            if (curTime > 1f)
            {
                //�� �� ���� ������ ���� �ٽ� ��Ÿ���°� �ƴ� �̻� ���UI ��Ȱ��ȭ
                SurpriseUI.gameObject.SetActive(false);
                finish = false;
                start_Suprise = false;
            }
        }

        //UI �߿� �� ������
        if (!start_Suprise && !finish)
        {
            //�� ���߰� ���� �ð�
            currentTime += Time.deltaTime;

            //���� �ð��� ������ ���� ���°� �Ǹ鼭 ������
            if (currentTime > maxTime)
            {
                hitEnabled = false;     //���� �Ұ���
                currentTime = 0;
                H_SoundManager.instance.On_enemyRunAway();
                state = State.run;
            }

            //���� ���ϴ� ��
            if (currentTime <= maxTime)
            {
                print("ghost movement");
                //�ͽ� ������
                dir = new Vector3(-2 * Mathf.Sin(Time.time * 3), Mathf.Sin(Time.time), Mathf.Cos(Time.time));
                transform.position += dir * attackMoveSpeed * Time.deltaTime * 0.3f;

                //������
                if (ehp.HP <= 0)//hp 0 �� ��
                {
                    state = State.death;
                }

            }
        }
    }

    private void UpdateRun()        //�÷��̾� �Ű�鼭 ����ġ�� ����..���°� ������ �׳� ���� ���� �հ� ������..
    {
        runMotionTime -= Time.deltaTime;

        if (runMotionTime > 0)
        {
            /*��Ʈ �̵�*/
            //�÷��̾� �Ű��
            transform.LookAt(target);
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);

            //���� ���� �հ� ������
            //transform.forward = Vector3.forward;
            //Vector3.Lerp(transform.position, outside.position, 0.1f);
        }

        else
        {
            m.enabled = false;
            //��� �ִ� ���� ã�� && Spot�� �� �ִ� ���� ����
            emptySpot = EnemyManager.instance.FindRandEmptySpot(ghostNumber);

            //�̵� �� ���� �ʱ�ȭ
            transform.position = emptySpot.position;
            runMotionTime = 1.5f;
            hitEnabled = false;
            state = State.hidden;
        }
    }
    public GameObject DeathVFX;
    public GameObject DeathEnemy;
    bool OnDVFX = true;
    private void UpdateDeath()
    {
        if (OnDVFX)
        {
            GameObject ODVFX = Instantiate(DeathVFX);
            ODVFX.transform.position = this.transform.position;
            DeathEnemy.SetActive(true);
            OnDVFX = false;
        }

        
        print("�׾���");
        //������ ������ �̵�
        print("1");
        Vector3 dir = -transform.position + cage.transform.position;
        dir.Normalize();
        transform.position += dir * 4 * Time.deltaTime;
//        transform.position = Vector3.Lerp(transform.position, cage.transform.position, Time.deltaTime * 10);
        if (Vector3.Distance(transform.position, cage.transform.position) < 0.2f)
        {
            H_SoundManager.instance.On_enemyCatch();
            EnemyUICatch.instance.catchCount++;
            Destroy(gameObject);
        }
    }
}
