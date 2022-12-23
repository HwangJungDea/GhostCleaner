using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyUICatch : MonoBehaviour
{
    public int catchCount = 0; // ���� ���������� +1��
    static public EnemyUICatch instance;
    public List<Image> ghostIcons; // ���ɾ����� UI ����

    public GameObject DoorOpen;
    OpenTheDoor OT;
    //public int Catch
    //{
    //    get { return catchCount; }
    //    set
    //    {
    //        catchCount = value;
    //    }
    //}

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        OT = DoorOpen.GetComponent<OpenTheDoor>();
    }

    void Update()
    {
        // ���� ���� ���� 1�� �����Ҷ����� �����ܵ� 1���� ȸ��ó��
        switch (catchCount)
        {
            case 1:
                ghostIcons[4].GetComponent<Image>().color = Color.gray;
                break;
            case 2:
                ghostIcons[3].GetComponent<Image>().color = Color.gray;
                break;
            case 3:
                ghostIcons[2].GetComponent<Image>().color = Color.gray;
                break;
            case 4:
                ghostIcons[1].GetComponent<Image>().color = Color.gray;
                break;
            case 5:
                ghostIcons[0].GetComponent<Image>().color = Color.gray;
                // �� ����� -> Clear�� �����Ű��
                OT.OpenDoor = true;
                H_SoundManager.instance.On_OpenTheDoor();
                catchCount++;
                break;
        }
    }
}
