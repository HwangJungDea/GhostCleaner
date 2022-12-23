using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class H_RayOpenObject : MonoBehaviour
{

    public SteamVR_Input_Sources handType;


    public SteamVR_Action_Boolean fireVR; //입력값 찾아서 다시 바꾸셈.

    public LineRenderer lr;

    public Transform rayStartPoint;


    [Header("CrossHair")]
    public Transform leftHandCrosshair;
    Vector3 crosshairOriginSize;
    public float kAudjust = 0.1f;

    void Start()
    {
        crosshairOriginSize = leftHandCrosshair.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        {
            //    // 1. 만약 키보드 T버튼을 누르면
            //    if (fireVR.GetState(SteamVR_Input_Sources.LeftHand))
            //    {
            //        lr.enabled = true; //라인 생성.

            //        // 1.1 손의 앞방향으로 Ray를 쏘고 부딪힌 곳이 Tower라면 기억하고 싶다.
            //        Ray ray = new Ray(hand.position, hand.forward);

            //        lr.SetPosition(0, hand.position);

            //        RaycastHit hitInfo;

            //        if (Physics.Raycast(ray, out hitInfo))
            //        {
            //            //바라본것이 있는 상황
            //            lr.SetPosition(1, hitInfo.point);
            //        }
            //        else
            //        {
            //            // 허공을 보고있는 상황
            //            lr.SetPosition(1, ray.origin + ray.direction * 100); //임의의 방향에 점 찍기.
            //        }
            //    }
            //    //2. 그렇지 않고 만약 손을 뗏을때
            //    else if (fireVR.GetStateUp(SteamVR_Input_Sources.LeftHand))
            //    {
            //        lr.enabled = false;// 라인 없애기.
            //                           //2.2 Tower를 기억하고 있었다면 그곳으로 이동하고 싶다
            //                           //2.3 플레이어를 보내고 싶다.
            //    }
            //}
        }

        Ray ray = new Ray(rayStartPoint.transform.position,
        rayStartPoint.transform.forward);// 위치체크, 방향체크
        Debug.DrawRay(rayStartPoint.transform.position, rayStartPoint.transform.forward, Color.red);


        RaycastHit hitInfo;
        int layerMask = 1 << (LayerMask.NameToLayer("LightTrigger"));
        // float.MaxValue, layerMask)
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layerMask))
        {
            leftHandCrosshair.position = hitInfo.point;
            leftHandCrosshair.forward = hitInfo.normal;//UI는 같은 방향. 만약 다르면 반대로 혀.
            leftHandCrosshair.localScale = crosshairOriginSize * hitInfo.distance * kAudjust;

            if (fireVR.GetStateDown(handType))
            {
                
                lr.enabled = true;    //외각 레이아웃으로 대체되면 lr은 삭제.
                lr.SetPosition(0, rayStartPoint.position);
                lr.SetPosition(1, hitInfo.point);


                if (hitInfo.transform.gameObject.tag == "Funiture")
                {
                    H_MoveObject hitObject = hitInfo.transform.GetComponent<H_MoveObject>();
                    print("1");
                    if (!hitObject.stateOpen)
                    {
                        hitObject.openObject = true;
                        hitObject.closeObject = false;
                        hitObject.SoundOn = true;
                    }
                    if (hitObject.stateOpen)
                    {
                        hitObject.openObject = false;
                        hitObject.closeObject = true;
                        hitObject.SoundOff = true;
                    }
                }

                //if (hitInfo.transform.gameObject.tag == "MoveObject")
                //{

                //}
            }
            else
            {
                lr.enabled = false;
                //콜라이더 없는곳은 사격안된다^^.
            }

            //if (fireVR.GetStateUp(SteamVR_Input_Sources.LeftHand))
            //{
            //    if (hitInfo.transform.gameObject.tag == "Funiture")
            //    {
            //        print("2");

            //        hitInfo.transform.GetComponent<H_MoveObject>().openObject = false;
            //        hitInfo.transform.GetComponent<H_MoveObject>().closeObject = true;
            //    }
            //}
        }
        else
        {
            leftHandCrosshair.position = ray.origin + ray.direction * 50;
            leftHandCrosshair.forward = ray.direction;//UI는 같은 방향. 만약 다르면 반대로 혀.
            leftHandCrosshair.localScale = crosshairOriginSize * 50 * kAudjust;

        }
    }
}
