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
        //���콺�� �������� (��ȭ��)
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //rx�� ������ �����ϰ� �ʹ�.
        rx = Mathf.Clamp(rx, -75, 75);
        //transform.Rotate(-my, mx, 0); ������ ������ ��������

        ry += mx * rotSpeed * Time.deltaTime; //���콺 ��/�� �̵����� ī�޶� y�� ȸ��
        rx -= my * rotSpeed * Time.deltaTime; // ���콺 ��/�Ʒ� �̵����� ī�޶� x�� ȸ��


        transform.eulerAngles = new Vector3(rx, ry, 0);

    }
}