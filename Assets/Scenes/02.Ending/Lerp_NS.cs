using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp_NS : MonoBehaviour
{
    public Transform A;

    [Range(0, 1)]
    public float t;
    float currentTime;
    public AnimationCurve ac;


    void Update()
    {
        currentTime += Time.deltaTime;
        t = ac.Evaluate(currentTime);
        transform.position = Vector3.Lerp(transform.position, A.position, t*Time.deltaTime);
    }

}
