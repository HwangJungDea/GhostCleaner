using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Sound : MonoBehaviour
{
    AudioSource audioSource;
    public static Attack_Sound instance; //½Ì±ÛÅæ

    //Ä® »ç¿îµå
    public AudioClip attack;
    public AudioClip bottleOpen;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void On_Attack()
    {
        audioSource.PlayOneShot(attack);
    }

    public void Off_Attack()
    {
        audioSource.Stop();
    }

    public void On_Bottle()
    {
        audioSource.PlayOneShot(bottleOpen);
    }
}
