using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    Quaternion destination_ro;
    public bool OpenDoor = false;
    public GameObject roomRight;
    // Start is called before the first frame update

    AudioSource doorAudio;
    void Start()
    {
        destination_ro = transform.rotation * Quaternion.Euler(new Vector3(0, -160, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenDoor)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, destination_ro, Time.deltaTime);
            roomRight.SetActive(true);
        }

    }
}
