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

    [Header("냉장고 각도(-360~360")]
    public float OpenQuaternion = -160;
    [Header("서랍여는 힘")]
    public float OpenForward = 1.5f;


    public bool openObject = false;
    public bool closeObject = false;
    public bool stateOpen = false;


    public enum SpawnType
    {
        회전열기,
        앞뒤로열기,
    }

    public enum SoundType
    {
        냉장고,
        전자체인지,
        서랍,
        회전서랍,
    }
    public SoundType soundType = SoundType.냉장고;
    public SpawnType spawnType = SpawnType.회전열기;

    void Start()
    {
        switch (spawnType)
        {
            case SpawnType.회전열기:
                origin_ro = transform.rotation;
                destination_ro = transform.rotation;
                destination_ro = transform.rotation * Quaternion.Euler(new Vector3(0, OpenQuaternion, 0));
                break;

            case SpawnType.앞뒤로열기:
                origin_tr = transform.position;
                print(OpenForward);
                destination_tr = transform.position + (transform.forward * OpenForward);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //플레이어 입력값 (누를때)
        {
            openObject = true;
            closeObject = false;
        }

        if (Input.GetButtonUp("Fire1"))  //플레이어 입력값 (뗄때)
        {
            openObject = false;
            closeObject = true;
        }

        switch (spawnType)
        {
            case SpawnType.회전열기: Update회전열기(); break;
            case SpawnType.앞뒤로열기: Update앞뒤로열기(); break;
        }

        switch (soundType)
        {
            case SoundType.냉장고: 냉장고(); break;
            case SoundType.서랍: 서랍(); break;
            case SoundType.전자체인지: 전자레인지(); break;
            case SoundType.회전서랍: 회전서랍(); break;
        }
    }
    private void Update회전열기()
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

    private void Update앞뒤로열기()
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
    private void 냉장고()
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

    private void 전자레인지()
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

    private void 서랍()
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

    private void 회전서랍()
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