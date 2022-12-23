using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyUICatch : MonoBehaviour
{
    public int catchCount = 0; // 유령 잡을때마다 +1씩
    static public EnemyUICatch instance;
    public List<Image> ghostIcons; // 유령아이콘 UI 연결

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
        // 유령 잡은 수가 1씩 증가할때마다 아이콘도 1개씩 회색처리
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
                // 다 잡았음 -> Clear랑 연결시키기
                OT.OpenDoor = true;
                H_SoundManager.instance.On_OpenTheDoor();
                catchCount++;
                break;
        }
    }
}
