using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//!!!: 수정 요함
//변수 순서: public, alphabet, array
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject cage; //유리병 위치
    bool[] isFull;          //스팟 채우기
    public List<GameObject> spot;   //스팟 위치 리스트
    int[] curPosition;              //현재 위치
    SkinnedMeshRenderer[] m;        //메시

    int fullSpotCnt;                //찬 스팟 개수 체크
    

    // Start is called before the first frame update
    void Start()
    {
        isFull = new bool[spot.Count];
        m = new SkinnedMeshRenderer[transform.childCount];
        curPosition = new int[transform.childCount];
        Spawn();
    }

    private void Spawn()
    {
        //현재 호출된 귀신 번호 = fullSpotCnt
        do
        {
            print(1);
            int i = UnityEngine.Random.Range(0, spot.Count);
            if (!isFull[i])     //스팟이 비어있다면
            {
                //호출된 귀신의 위치 = 비어있는 스팟의 위치
                transform.GetChild(fullSpotCnt).transform.position = spot[i].transform.position;

                //메쉬랜더러 저장 후 투명화
                m[fullSpotCnt] = transform.GetChild(fullSpotCnt).GetChild(0).GetComponent<SkinnedMeshRenderer>();
                m[fullSpotCnt].enabled = false;

                //스팟 설정 맞추기
                isFull[i] = true;       //현재 귀신이 있는 스팟이 찼다는 것을 의미
                curPosition[fullSpotCnt] = i;       //귀신의 현재 스팟 번호
                fullSpotCnt++;      //스팟이 몇 개가 찼는지
            }
        } while (fullSpotCnt < transform.childCount);     //스팟 5개가 찰 때까지 수행
    }


    // Update is called once per frame
    void Update()
    {

    }

    internal Transform FindRandEmptySpot(int ghostNumber)
    {
        int i;
        do//빈 스팟 찾을 때까지 시행
        {
            i = UnityEngine.Random.Range(0, spot.Count);
        } while (isFull[i]);

        //스팟 설정 맞추기
        isFull[i] = true;       //귀신의 현재 스팟이 찼다
        isFull[curPosition[ghostNumber]] = false;     //귀신의 이전 스팟이 비었다
        curPosition[ghostNumber] = i;     //귀신의 현재 스팟 번호
        return spot[i].transform;
    }
}
