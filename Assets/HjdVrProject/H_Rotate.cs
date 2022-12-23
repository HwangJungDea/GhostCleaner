using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Rotate : MonoBehaviour
{


    float rx;
    float ry;
    public float rotSpeed = 200f;

    void Update()
    {
        //마우스를 움직여서 (변화량)
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //rx의 각도를 제한하고 싶다.
        rx = Mathf.Clamp(rx, -75, 75);
        //transform.Rotate(-my, mx, 0); 오류가 많으니 버려버려

        ry += mx * rotSpeed * Time.deltaTime; //마우스 좌/우 이동으로 카메라 y축 회전
        rx -= my * rotSpeed * Time.deltaTime; // 마우스 위/아래 이동으로 카메라 x축 회전


        transform.eulerAngles = new Vector3(rx, ry, 0);

    }
}