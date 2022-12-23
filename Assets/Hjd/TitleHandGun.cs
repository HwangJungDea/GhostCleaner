using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class TitleHandGun : MonoBehaviour
{
    //잠만잠만잠만
    //여기서는 그냥 레이쏘고
    //닿이면 사라지고 켜지게한다 그거만 있으면 되잖아 등신아ㄱ-
    public SteamVR_Input_Sources handType;
    [Header("tele")]
    public SteamVR_Action_Boolean fireVR;
    //실제로 할떄는 총구에서 나가게 하기~ㅅ~.
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
        Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);// 위치체크, 방향체크
        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward, Color.red);


        RaycastHit hitInfo;
        int layerMask = 1 << (LayerMask.NameToLayer("LightTrigger"));
        // float.MaxValue, layerMask)
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layerMask))
        {

            crosshair.position = hitInfo.point;
            crosshair.forward = hitInfo.normal;//UI는 같은 방향. 만약 다르면 반대로 혀.
            crosshair.localScale = crosshairOriginSize * hitInfo.distance * kAudjust;
            //1. 사용자가 마우스 왼쪽버튼을 누르면
            //if (Input.GetButton("Fire1"))
            //Player가 Enemy에게 Damage를 입히면
            //만약 부딪힌것이 Enemy라면 EnemyHP컴포넌트를 가져와서
        }
        else
        {
            crosshair.position = ray.origin + ray.direction * 50;
            crosshair.forward = ray.direction;//UI는 같은 방향. 만약 다르면 반대로 혀.
            crosshair.localScale = crosshairOriginSize * 50 * kAudjust;
        }
    }

    void AttackButtle()
    {
        Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);// 위치체크, 방향체크
                                                                                     //3. 만약 바라본 후 부딪혔다면
        RaycastHit hitInfo;
        int layerMask = 1 << (LayerMask.NameToLayer("LightTrigger"));

        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward, Color.red);

        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layerMask))
        {
            if (hitInfo.transform.name.Contains("Bottle"))// 안에 Enemy를 포함하고 있는가 검증하기.
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
