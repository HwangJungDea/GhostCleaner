using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//!!!: ���� ����
//���� ����: public, alphabet, array
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject cage; //������ ��ġ
    bool[] isFull;          //���� ä���
    public List<GameObject> spot;   //���� ��ġ ����Ʈ
    int[] curPosition;              //���� ��ġ
    SkinnedMeshRenderer[] m;        //�޽�

    int fullSpotCnt;                //�� ���� ���� üũ
    

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
        //���� ȣ��� �ͽ� ��ȣ = fullSpotCnt
        do
        {
            print(1);
            int i = UnityEngine.Random.Range(0, spot.Count);
            if (!isFull[i])     //������ ����ִٸ�
            {
                //ȣ��� �ͽ��� ��ġ = ����ִ� ������ ��ġ
                transform.GetChild(fullSpotCnt).transform.position = spot[i].transform.position;

                //�޽������� ���� �� ����ȭ
                m[fullSpotCnt] = transform.GetChild(fullSpotCnt).GetChild(0).GetComponent<SkinnedMeshRenderer>();
                m[fullSpotCnt].enabled = false;

                //���� ���� ���߱�
                isFull[i] = true;       //���� �ͽ��� �ִ� ������ á�ٴ� ���� �ǹ�
                curPosition[fullSpotCnt] = i;       //�ͽ��� ���� ���� ��ȣ
                fullSpotCnt++;      //������ �� ���� á����
            }
        } while (fullSpotCnt < transform.childCount);     //���� 5���� �� ������ ����
    }


    // Update is called once per frame
    void Update()
    {

    }

    internal Transform FindRandEmptySpot(int ghostNumber)
    {
        int i;
        do//�� ���� ã�� ������ ����
        {
            i = UnityEngine.Random.Range(0, spot.Count);
        } while (isFull[i]);

        //���� ���� ���߱�
        isFull[i] = true;       //�ͽ��� ���� ������ á��
        isFull[curPosition[ghostNumber]] = false;     //�ͽ��� ���� ������ �����
        curPosition[ghostNumber] = i;     //�ͽ��� ���� ���� ��ȣ
        return spot[i].transform;
    }
}
