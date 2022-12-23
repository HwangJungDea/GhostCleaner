using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle_NS : MonoBehaviour
{
    public GameObject ghostInBottle;
    public GameObject door;
    public GameObject lid;
    Rigidbody rb;

    AudioSource audioBottleFall;
    public AudioSource audioGlass;
    public AudioSource audioGhost;

    [Header("BottleCheak")]
    public bool bottleCheak;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        audioBottleFall = GetComponent<AudioSource>();
        audioGlass.Play();
        audioGhost.Play();
    }

    private void Update()
    {
        rb.AddTorque(transform.up * 10, ForceMode.Impulse);
        Vector3 dir = door.transform.position - transform.position;
        transform.position += dir * 0.01f * Time.deltaTime;
    }

    int audiocount = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            bottleCheak = true;
            audioGlass.Stop();

            if (audiocount == 0)
            {
                audioBottleFall.Play();
                audiocount++;
            }
            ghostInBottle.SetActive(false);
            lid.transform.position += transform.up * 20 * Time.deltaTime;

            int layerMask = 1 << LayerMask.NameToLayer("Ghost");
            Collider[] cols = Physics.OverlapSphere(transform.position, 1, layerMask);

            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].GetComponent<Ghost_NS>().BottleFall();
            }
        }
    }
}
