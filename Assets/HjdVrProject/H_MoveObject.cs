using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_MoveObject : MonoBehaviour
{
    //public static H_MoveObject instance;

    //private void Awake()
    //{
    //    instance = this;
    //}

    Vector3 origin_tr;
    Vector3 destination_tr;

    Quaternion origin_ro;
    Quaternion destination_ro;

    [Header("����� ����(-360~360")]
    public float OpenQuaternion = -160;
    [Header("�������� ��")]
    public float OpenForward = 1.5f;


    public bool openObject = false;
    public bool closeObject = false;
    public bool stateOpen = false;


    public enum SpawnType
    {
        ȸ������,
        �յڷο���,
    }

    public enum SoundType
    {
        �����,
        ����ü����,
        ����,
        ȸ������,
    }
    public SoundType soundType = SoundType.�����;
    public SpawnType spawnType = SpawnType.ȸ������;

    void Start()
    {
        switch (spawnType)
        {
            case SpawnType.ȸ������:
                origin_ro = transform.rotation;
                destination_ro = transform.rotation;
                destination_ro = transform.rotation * Quaternion.Euler(new Vector3(0, OpenQuaternion, 0));
                break;

            case SpawnType.�յڷο���:
                origin_tr = transform.position;
                print(OpenForward);
                destination_tr = transform.position + (transform.forward * OpenForward);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //�÷��̾� �Է°� (������)
        {
            openObject = true;
            closeObject = false;
        }

        if (Input.GetButtonUp("Fire1"))  //�÷��̾� �Է°� (����)
        {
            openObject = false;
            closeObject = true;
        }

        switch (spawnType)
        {
            case SpawnType.ȸ������: Updateȸ������(); break;
            case SpawnType.�յڷο���: Update�յڷο���(); break;
        }

        switch (soundType)
        {
            case SoundType.�����: �����(); break;
            case SoundType.����: ����(); break;
            case SoundType.����ü����: ���ڷ�����(); break;
            case SoundType.ȸ������: ȸ������(); break;
        }
    }
    private void Updateȸ������()
    {
        if (openObject == true)
        {
            stateOpen = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, destination_ro, Time.deltaTime * 5);
        }

        if (closeObject == true)
        {
            stateOpen = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, origin_ro, Time.deltaTime * 5);
        }
    }

    private void Update�յڷο���()
    {
        if (openObject == true)
        {
            stateOpen = true;

            transform.position = Vector3.Lerp(transform.position, destination_tr, Time.deltaTime * 5);
            if (Vector3.Distance(transform.position, destination_tr) < 0.1f)
            {
                openObject = false;
            }
        }

        if (closeObject == true)
        {
            stateOpen = false;

            transform.position = Vector3.Lerp(transform.position, origin_tr, Time.deltaTime * 5);
            if (Vector3.Distance(transform.position, destination_tr) < 0.1f)
            {
                closeObject = false;
            }
        }
    }

    public bool SoundOn = false;
    public bool SoundOff = false;
    private void �����()
    {
        if (SoundOn)
        {
            H_SoundManager.instance.On_fridge_Open();
            SoundOn = false;
        }
        if (SoundOff)
        {
            H_SoundManager.instance.On_fridge_Close();
            SoundOff = false;
        }
    }

    private void ���ڷ�����()
    {
        if (SoundOn)
        {
            H_SoundManager.instance.On_Microwave_Open();
            SoundOn = false;
        }
        if (SoundOff)
        {
            H_SoundManager.instance.On_Microwave_Close();
            SoundOff = false;
        }
    }

    private void ����()
    {
        if (SoundOn)
        {
            H_SoundManager.instance.On_drawer_Open();
            SoundOn = false;
        }
        if (SoundOff)
        {
            H_SoundManager.instance.On_drawer_close();
            SoundOff = false;
        }
    }

    private void ȸ������()
    {
        if (SoundOn)
        {
            H_SoundManager.instance.On_drawer_Quaternion_Open();
            SoundOn = false;
        }
        if (SoundOff)
        {
            H_SoundManager.instance.On_drawer_Quaternion_Close();
            SoundOff = false;
        }
    }
}