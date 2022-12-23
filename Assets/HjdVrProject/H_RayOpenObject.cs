using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class H_RayOpenObject : MonoBehaviour
{

    public SteamVR_Input_Sources handType;


    public SteamVR_Action_Boolean fireVR; //�Է°� ã�Ƽ� �ٽ� �ٲټ�.

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
            //    // 1. ���� Ű���� T��ư�� ������
            //    if (fireVR.GetState(SteamVR_Input_Sources.LeftHand))
            //    {
            //        lr.enabled = true; //���� ����.

            //        // 1.1 ���� �չ������� Ray�� ��� �ε��� ���� Tower��� ����ϰ� �ʹ�.
            //        Ray ray = new Ray(hand.position, hand.forward);

            //        lr.SetPosition(0, hand.position);

            //        RaycastHit hitInfo;

            //        if (Physics.Raycast(ray, out hitInfo))
            //        {
            //            //�ٶ󺻰��� �ִ� ��Ȳ
            //            lr.SetPosition(1, hitInfo.point);
            //        }
            //        else
            //        {
            //            // ����� �����ִ� ��Ȳ
            //            lr.SetPosition(1, ray.origin + ray.direction * 100); //������ ���⿡ �� ���.
            //        }
            //    }
            //    //2. �׷��� �ʰ� ���� ���� ������
            //    else if (fireVR.GetStateUp(SteamVR_Input_Sources.LeftHand))
            //    {
            //        lr.enabled = false;// ���� ���ֱ�.
            //                           //2.2 Tower�� ����ϰ� �־��ٸ� �װ����� �̵��ϰ� �ʹ�
            //                           //2.3 �÷��̾ ������ �ʹ�.
            //    }
            //}
        }

        Ray ray = new Ray(rayStartPoint.transform.position,
        rayStartPoint.transform.forward);// ��ġüũ, ����üũ
        Debug.DrawRay(rayStartPoint.transform.position, rayStartPoint.transform.forward, Color.red);


        RaycastHit hitInfo;
        int layerMask = 1 << (LayerMask.NameToLayer("LightTrigger"));
        // float.MaxValue, layerMask)
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layerMask))
        {
            leftHandCrosshair.position = hitInfo.point;
            leftHandCrosshair.forward = hitInfo.normal;//UI�� ���� ����. ���� �ٸ��� �ݴ�� ��.
            leftHandCrosshair.localScale = crosshairOriginSize * hitInfo.distance * kAudjust;

            if (fireVR.GetStateDown(handType))
            {
                
                lr.enabled = true;    //�ܰ� ���̾ƿ����� ��ü�Ǹ� lr�� ����.
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
                //�ݶ��̴� ���°��� ��ݾȵȴ�^^.
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
            leftHandCrosshair.forward = ray.direction;//UI�� ���� ����. ���� �ٸ��� �ݴ�� ��.
            leftHandCrosshair.localScale = crosshairOriginSize * 50 * kAudjust;

        }
    }
}
