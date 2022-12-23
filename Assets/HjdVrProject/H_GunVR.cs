using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using System;

public class H_GunVR : MonoBehaviour
{
    public SteamVR_Input_Sources handType;


    [Header("tele")]
    public SteamVR_Action_Boolean fireVR;
    //������ �ҋ��� �ѱ����� ������ �ϱ�~��~.


    public Transform firePoint;

    public Transform crosshair;
    Vector3 crosshairOriginSize;
    public float kAudjust = 0.1f;

    [Header("bottle")]
    public Transform capPosition;

    Quaternion origin_ro;
    Quaternion destination_ro;

    [Header("AttackVFX")]
    public GameObject PostP;

    EnemyHP ehp;



    // Start is called before the first frame update
    void Start()
    {
        crosshairOriginSize = crosshair.localScale;

        origin_ro = capPosition.transform.localRotation;//�̻��ϸ� �ٽ� rotation
        destination_ro = capPosition.transform.localRotation;
        destination_ro = capPosition.transform.localRotation * Quaternion.Euler(new Vector3(0, 0, -150));

    }



    public GameObject FireVFX;
    // Update is called once per frame
    void Update()
    {
        CrossHair();
        if (fireVR.GetStateDown(handType))
        {
            Attack_Sound.instance.On_Attack();
            Attack_Sound.instance.On_Bottle();
            FireVFX.SetActive(true);
        }
        else if (fireVR.GetState(handType))
        {
            capPosition.transform.localRotation = Quaternion.Slerp(capPosition.transform.localRotation, destination_ro, Time.deltaTime * 5);
            AttackEnemy();

            Start_PostProcess(true);
        }
        else if (fireVR.GetStateUp(handType))
        {
            FireVFX.SetActive(false);
            ehp.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
            Attack_Sound.instance.Off_Attack();
        }
        else
        {
            capPosition.transform.localRotation = Quaternion.Slerp(capPosition.transform.localRotation, origin_ro, Time.deltaTime * 5);
            Start_PostProcess(false);
            Attack_Sound.instance.Off_Attack();
        }
    }

    //public GameObject FirePoint;
    //public float MaxLength;
    //public GameObject Prefabs;

    //private GameObject Instance;
    //private Hovl_Laser LaserScript;
    //private Hovl_Laser2 LaserScript2;
    //private void UpdateRazer()
    //{
    //    //Enable lazer
    //    if (fireVR.GetState(handType))
    //    {
    //        Destroy(Instance);
    //        Instance = Instantiate(Prefabs, FirePoint.transform.position, FirePoint.transform.rotation);
    //        Instance.transform.parent = transform;
    //        LaserScript = Instance.GetComponent<Hovl_Laser>();
    //        LaserScript2 = Instance.GetComponent<Hovl_Laser2>();
    //    }

    //    //Disable lazer prefab
    //    if (fireVR.GetStateUp(handType))
    //    {
    //        if (LaserScript) LaserScript.DisablePrepare();
    //        if (LaserScript2) LaserScript2.DisablePrepare();
    //        Destroy(Instance, 1);
    //    }
    //}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

    }

    private void Start_PostProcess(bool inputPost)
    {
        PostP.SetActive(inputPost);
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

    //public GameObject EnemyObject;
    void AttackEnemy()
    {
        Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);// ��ġüũ, ����üũ
                                                                                     //3. ���� �ٶ� �� �ε����ٸ�
        RaycastHit hitInfo;
        int layerMask = 1 << (LayerMask.NameToLayer("LightTrigger"));

        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward, Color.red);

        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layerMask))
        {
            if (hitInfo.transform.name.Contains("Enemy"))// �ȿ� Enemy�� �����ϰ� �ִ°� �����ϱ�.
            {
                ehp = hitInfo.transform.GetComponent<EnemyHP>();
                // �÷��̾��� ü���� 1 �����ϰ�ʹ�.
                ehp.HP -= Time.deltaTime;
                ehp.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;

                print(1);
                // ���� �÷��̾��� ü���� 0 ���϶��
                if (ehp.HP <= 0)
                {

                }
            }
            else
            {
                print(22);
                ehp.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
            }
        }

    }
}