using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_NS : MonoBehaviour
{
    public GameObject light;
    public GameObject ghost;
    public GameObject endingObj;
    public GameObject text;

    AudioSource audioLight;
    void Start()
    {
        light.SetActive(false);
        ghost.SetActive(false);
        endingObj.SetActive(true);
        text.SetActive(false);

        audioLight = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (ghostCount == 5)
        {
            Invoke("LightOn", 3);
            Invoke("LightOff", 6);
            Invoke("TextTheEnd", 7.5f);
        }
    }

    void LightOn()
    {
        light.SetActive(true);
        ghost.SetActive(true);
    }
    int audioCount = 0;
    void LightOff()
    {
        endingObj.SetActive(false);
        if (audioCount == 0)
        {
            audioLight.Play();
            audioCount++;
        }
    }
    void TextTheEnd()
    {
        text.SetActive(true);
    }

    int ghostCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            other.gameObject.SetActive(false);
            ghostCount++;
        }
    }
}
