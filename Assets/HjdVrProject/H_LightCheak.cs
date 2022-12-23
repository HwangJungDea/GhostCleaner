using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class H_LightCheak : MonoBehaviour
{

    public SteamVR_Input_Sources rightHandType; //오른손으로
    public SteamVR_Action_Boolean teleportAction;


    public GameObject bottleGroup;
    public GameObject light2;
    bool lightOn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (teleportAction.GetStateDown(rightHandType))
        {
            bottleGroup.SetActive(true);

            if (lightOn == false)
            {
                lightOn = true;
                light2.SetActive(true);
            }
            else
            {
                lightOn = false;
                light2.SetActive(false);
            }
        }
    }

}