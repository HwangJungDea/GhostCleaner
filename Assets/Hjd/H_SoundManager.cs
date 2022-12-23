using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_SoundManager : MonoBehaviour
{

    AudioSource audioSource;
    public static H_SoundManager instance; //½Ì±ÛÅæ

    //Ä® »ç¿îµå
    public AudioClip OpenTheDoor;
    public AudioClip attack;
    public AudioClip bottleLid;
    public AudioClip bottleOpen;

    public AudioClip drawer_close;
    public AudioClip drawer_Open;
    public AudioClip fridge_Close;
    public AudioClip fridge_Open;
    public AudioClip drawer_Quaternion_Open;
    public AudioClip drawer_Quaternion_Close;
    public AudioClip Microwave_Open;
    public AudioClip Microwave_Close;

    public GameObject enemyCatch;
    public AudioClip enemyHide;
    public AudioClip enemyRunAway;
    public AudioClip enemySurprise;
    



    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemyCatch.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void On_OpenTheDoor()
    {
        audioSource.PlayOneShot(OpenTheDoor);
    }
    public void On_attack()
    {
        audioSource.PlayOneShot(attack);
    }

    public void Off_attack()
    {
        audioSource.Stop();
    }

    public void On_bottleLid()
    {
        audioSource.PlayOneShot(bottleLid);
    }
    public void On_bottleOpen()
    {
        audioSource.PlayOneShot(bottleOpen);

    }
    
    public void On_drawer_close()
    {
        audioSource.PlayOneShot(drawer_close);
    }
    public void On_drawer_Open()
    {
        audioSource.PlayOneShot(drawer_Open);
    }
    public void On_fridge_Close()
    {
        audioSource.PlayOneShot(fridge_Close);
    }
    public void On_fridge_Open()
    {
        audioSource.PlayOneShot(fridge_Open);
    }
    public void On_enemyCatch()
    {
        enemyCatch.SetActive(true);
        Invoke("Off_enemyCatchSound", 1f);
    }
    void Off_enemyCatchSound()
    {
        enemyCatch.SetActive(false);
    }
    public void On_enemyHide()
    {
        audioSource.PlayOneShot(enemyHide);
    }
    public void On_enemyRunAway()
    {
        audioSource.PlayOneShot(enemyRunAway);
    }
    public void On_enemySurprise()
    {
        audioSource.PlayOneShot(enemySurprise);
    }
    public void On_drawer_Quaternion_Open()
    {
        audioSource.PlayOneShot(drawer_Quaternion_Open);
    }
    public void On_drawer_Quaternion_Close()
    {
        audioSource.PlayOneShot(drawer_Quaternion_Close);
    }
    public void On_Microwave_Open()
    {
        audioSource.PlayOneShot(Microwave_Open);
    }
    public void On_Microwave_Close()
    {
        audioSource.PlayOneShot(Microwave_Close);
    }
}
