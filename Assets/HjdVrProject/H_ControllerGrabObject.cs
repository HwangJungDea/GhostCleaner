using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


public class H_ControllerGrabObject : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose conTrollerPose;
    public SteamVR_Action_Boolean grabAction;

    private GameObject collidingObject;
    private GameObject objectInHand;
    


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //버튼 누를때
        if (grabAction.GetStateDown(handType))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }
        //버튼 땔 때
        if (grabAction.GetStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GripObject")
        {
            SetCollidingObject(other);
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "GripObject")
        {
            SetCollidingObject(other);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GripObject")
        {
            if (!collidingObject)
            {
                return;
            }
            collidingObject = null;

        }
    }

    //충돌중인 객체로 설정
    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;

    }

    private void GrabObject()
    {


        objectInHand = collidingObject; //잡은 객체로 설정
        collidingObject = null; //충돌 객체 해제
       // print("잡아라");
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();



    }


    //FixedJoint = 객체들을 하나로 묶어 고정시켜줌
    //breakForce = 조인트가 제거되도록 하기 위한 필요한 힘의 크기
    //breakTorque = 조인트가 제거되도록 하기 위한 필요한 토크
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
    //객체를 놓음
    //contrllerPose.GetVeloecity() = 컨트롤러의 속도
    //contrllerPose.GetAngularVeloecity() = 컨트롤러의 각속도
    private void ReleaseObject()
    {
        //print("놓아라");
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());


            objectInHand.GetComponent<Rigidbody>().velocity =
                conTrollerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity =
                conTrollerPose.GetAngularVelocity();

        }
        objectInHand = null;
    }


}
