using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class TitleHandGun : MonoBehaviour
{
    //�Ḹ�Ḹ�Ḹ
    //���⼭�� �׳� ���̽��
    //���̸� ������� �������Ѵ� �װŸ� ������ ���ݾ� ��žƤ�-
    public SteamVR_Input_Sources handType;
    [Header("tele")]
    public SteamVR_Action_Boolean fireVR;
    //������ �ҋ��� �ѱ����� ������ �ϱ�~��~.
    public Transform firePoint;

    [Header("Crosshair")]
    public Transform crosshair;
    Vector3 crosshairOriginSize;
    public float kAudjust = 0.1f;

    [Header("Change")]
    public GameObject gameHandButtle;
    public GameObject buttle;
    public GameObject lightHand;

    public GameObject ExitDoor;
    


    // Start is called before the first frame update
    void Start()
    {
        crosshairOriginSize = crosshair.localScale;
        

    }




    // Update is called once per frame
    void Update()
    {




        CrossHair();
        if (fireVR.GetStateDown(handType))
        {

            AttackButtle();
        }
    }


    public void CrossHair()
    {
        Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);// ��ġüũ, ����üũ
        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward, Color.red);


        RaycastHit hitInfo;
        int layerMask = 1 << (LayerMask.NameToLayer("LightTrigger"));
        // float.MaxValue, layerMask)
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layerMask))
        {

            crosshair.position = hitInfo.point;
            crosshair.forward = hitInfo.normal;//UI�� ���� ����. ���� �ٸ��� �ݴ�� ��.
            crosshair.localScale = crosshairOriginSize * hitInfo.distance * kAudjust;
            //1. ����ڰ� ���콺 ���ʹ�ư�� ������
            //if (Input.GetButton("Fire1"))
            //Player�� Enemy���� Damage�� ������
            //���� �ε������� Enemy��� EnemyHP������Ʈ�� �����ͼ�
        }
        else
        {
            crosshair.position = ray.origin + ray.direction * 50;
            crosshair.forward = ray.direction;//UI�� ���� ����. ���� �ٸ��� �ݴ�� ��.
            crosshair.localScale = crosshairOriginSize * 50 * kAudjust;
        }
    }

    void AttackButtle()
    {
        Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);// ��ġüũ, ����üũ
                                                                                     //3. ���� �ٶ� �� �ε����ٸ�
        RaycastHit hitInfo;
        int layerMask = 1 << (LayerMask.NameToLayer("LightTrigger"));

        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward, Color.red);

        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layerMask))
        {
            if (hitInfo.transform.name.Contains("Bottle"))// �ȿ� Enemy�� �����ϰ� �ִ°� �����ϱ�.
            {
                
                {
                buttle.SetActive(false);
                lightHand.SetActive(false);
                gameHandButtle.SetActive(true);
                OpenTheDoor door = ExitDoor.GetComponent<OpenTheDoor>();
                door.OpenDoor = true;
                H_SoundManager.instance.On_OpenTheDoor();

                }

            }
        }
    }
}
