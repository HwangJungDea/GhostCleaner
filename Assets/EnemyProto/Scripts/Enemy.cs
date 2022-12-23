using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//문제
//4. 이동
//5. 공격판정 뭐로 함?
//6. #플레이어 코드; 합치기
//7. ehp.HP 여기랑 저기랑 같겠지??


public class Enemy : MonoBehaviour
{
    public State state;
    public enum State
    {
        hidden,   //투명상태
        brightened, //라이트 발각
        run,    //오브젝트간 이동중
        death,  //죽음
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



    public float curTime;//유아이 없애는데 필요한 시간
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
            //시간 초기화
            currentTime = 0;

            //HP를 줄일 수 있는 상태변수 true
            hitEnabled = true;

            start_Suprise = true;
            Ori_objectPosition = transform.position;
            Des_objectPosition = transform.position + new Vector3(0, 1, 0);
            SurpriseUI.gameObject.SetActive(true);
            H_SoundManager.instance.On_enemySurprise();
            // 이동상태로 전이하고싶다.
            state = State.brightened;
        }
    }

    private void UpdateBrightened()
    {

        curTime = curTime + Time.deltaTime;

        transform.LookAt(target.transform.position);

        //빛에 비춰졌지만 한 번도 놀라지 않았을 때
        if (start_Suprise && !finish)
        {
            //목표 지점으로 이동(위로 살짝)
            transform.position = Vector3.Lerp(transform.position, Des_objectPosition, Time.deltaTime * 8);

            //목표 지점에 다다르면
            if (Vector3.Distance(transform.position, Des_objectPosition) < 0.2f)
            {
                finish = true;
            }
        }

        //목표 지점으로 이동(아래로 살짝)
        if (finish)
        {
            //목표 지점으로 이동
            transform.position = Vector3.Lerp(transform.position, Ori_objectPosition, Time.deltaTime * 8);

            //목표 지점에 다다르면
            if (curTime > 1f)
            {
                //한 번 놀라면 다음에 숨고서 다시 나타나는게 아닌 이상 놀람UI 비활성화
                SurpriseUI.gameObject.SetActive(false);
                finish = false;
                start_Suprise = false;
            }
        }

        //UI 중엔 안 움직임
        if (!start_Suprise && !finish)
        {
            //빛 비추고 지난 시간
            currentTime += Time.deltaTime;

            //일정 시간이 지나면 무적 상태가 되면서 도망감
            if (currentTime > maxTime)
            {
                hitEnabled = false;     //공격 불가능
                currentTime = 0;
                H_SoundManager.instance.On_enemyRunAway();
                state = State.run;
            }

            //공격 당하는 중
            if (currentTime <= maxTime)
            {
                print("ghost movement");
                //귀신 움직임
                dir = new Vector3(-2 * Mathf.Sin(Time.time * 3), Mathf.Sin(Time.time), Mathf.Cos(Time.time));
                transform.position += dir * attackMoveSpeed * Time.deltaTime * 0.3f;

                //움직임
                if (ehp.HP <= 0)//hp 0 일 때
                {
                    state = State.death;
                }

            }
        }
    }

    private void UpdateRun()        //플레이어 놀래키면서 도망치고 숨기..놀라는거 싫으면 그냥 위로 지붕 뚫고 나가기..
    {
        runMotionTime -= Time.deltaTime;

        if (runMotionTime > 0)
        {
            /*고스트 이동*/
            //플레이어 놀래키기
            transform.LookAt(target);
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);

            //위로 지붕 뚫고 나가기
            //transform.forward = Vector3.forward;
            //Vector3.Lerp(transform.position, outside.position, 0.1f);
        }

        else
        {
            m.enabled = false;
            //비어 있는 곳을 찾기 && Spot의 차 있는 상태 설정
            emptySpot = EnemyManager.instance.FindRandEmptySpot(ghostNumber);

            //이동 후 설정 초기화
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

        
        print("죽었음");
        //죽으면 통으로 이동
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
