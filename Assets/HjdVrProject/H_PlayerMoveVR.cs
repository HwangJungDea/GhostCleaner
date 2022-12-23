using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;


//1. 프린트로 저넘시키 입력값을 찾아보자.
//2. 그다음 그넘시키를 플레이어에 추가하기.
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

            dirr = Camera.main.transform.TransformDirection(dirr); //지역변수값으로 변경
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
