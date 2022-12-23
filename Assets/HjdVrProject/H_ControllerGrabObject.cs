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
        //��ư ������
        if (grabAction.GetStateDown(handType))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }
        //��ư �� ��
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

    //�浹���� ��ü�� ����
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


        objectInHand = collidingObject; //���� ��ü�� ����
        collidingObject = null; //�浹 ��ü ����
       // print("��ƶ�");
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();



    }


    //FixedJoint = ��ü���� �ϳ��� ���� ����������
    //breakForce = ����Ʈ�� ���ŵǵ��� �ϱ� ���� �ʿ��� ���� ũ��
    //breakTorque = ����Ʈ�� ���ŵǵ��� �ϱ� ���� �ʿ��� ��ũ
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
    //��ü�� ����
    //contrllerPose.GetVeloecity() = ��Ʈ�ѷ��� �ӵ�
    //contrllerPose.GetAngularVeloecity() = ��Ʈ�ѷ��� ���ӵ�
    private void ReleaseObject()
    {
        //print("���ƶ�");
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
