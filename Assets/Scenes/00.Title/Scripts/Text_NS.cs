using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_NS : MonoBehaviour
{
    public float speed;
    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            speed = 0;
            Destroy(collision.gameObject);
            Destroy(gameObject, 2);
        }
    }
}
