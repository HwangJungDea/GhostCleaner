using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_FlashLight : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost")
        {
            MeshRenderer enemyMR = other.GetComponent<MeshRenderer>();
            if (enemyMR != null)
            {
                enemyMR.enabled = true;
            }
        } //밑에넘이 에너미로 넘어간거임.

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ghost")
        {
            //print("Lightin2");
            //H_Enemy enemy = other.transform.GetComponent<H_Enemy>();
            MeshRenderer enemyMR = other.GetComponent<MeshRenderer>();

            if (enemyMR != null)
            {
                enemyMR.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ghost")
        {
            MeshRenderer enemyMR = other.GetComponent<MeshRenderer>();

            if (enemyMR != null)
            {

                enemyMR.enabled = false;

            }
        }
    }




}
