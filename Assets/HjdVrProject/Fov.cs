using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov : MonoBehaviour
{
    public GameObject S_rayPoint;

    bool[] ghosts;

    void Start()
    {
        ghosts = new bool[2];
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ghost")
        {
            Vector3 TriggerTarget_dir = other.transform.position - S_rayPoint.transform.position;
            TriggerTarget_dir.Normalize();
            Ray ray = new Ray(S_rayPoint.transform.position, TriggerTarget_dir);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.green);


                MeshRenderer ren = other.transform.GetComponent<MeshRenderer>(); //자기꺼


                SkinnedMeshRenderer ghostColor = hitInfo.transform.gameObject.GetComponentInParent<SkinnedMeshRenderer>();
                if (hitInfo.transform.gameObject.tag == "Ghost") //태그 확인
                {
                    ren.enabled = false;
                    ghostColor.material.color = Color.red;
                }
                else
                {
                    ghostColor.material.color = Color.white;
                    ren.enabled = true;
                }


                //float hitinfoDis = Vector3.Distance(S_rayPoint.transform.position, hitInfo.transform.position);

                //if (targetDis == hitinfoDis)//(targetDis == hitinfoDis)
                //{
                //    enemyMR.enabled = true;
                //    other = null;
                //}
                //else
                //{
                //    enemyMR.enabled = false;
                //    other = null;
                //}

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MeshRenderer ren = other.transform.GetComponent<MeshRenderer>();
        if (ren != null && ren.enabled == false)
        {
            ren.enabled = true;
        }
    }


}
