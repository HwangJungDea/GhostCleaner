using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;


//1. ����Ʈ�� ���ѽ�Ű �Է°��� ã�ƺ���.
//2. �״��� �׳ѽ�Ű�� �÷��̾ �߰��ϱ�.
public class H_PlayerMoveVR : MonoBehaviour
{
    public float moveSpeed = 1f;

    public SteamVR_Input_Sources handType;

    //public SteamVR_Input_Sources handType;
    public SteamVR_Action_Vector2 teleportDir;
    public SteamVR_Action_Boolean teleportAction;
    CharacterController cc;
    

    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            Vector2 dir = teleportDir.GetAxis(handType);
            print("dir" + dir);
            float dirX = dir.x;
            float dirY = dir.y;
            Vector3 dirr = new Vector3(dirX, 0, dirY);

            dirr = Camera.main.transform.TransformDirection(dirr); //�������������� ����
            dirr.y = 0;
            dirr.Normalize();


            cc.Move(dirr * moveSpeed * Time.deltaTime);
            //transform.position += dirr * moveSpeed * Time.deltaTime;
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            print(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }



}
