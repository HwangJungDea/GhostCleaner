using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

// F버튼 누르면 플래쉬 빛 켜짐
// 유령을 잡으면 문 열림(문 애니메이션 연결)
// 열린 문 안에 VFX 효과 
// 플레이어가 이동해서 나가면 밝아지고 씬 전환
public class Player_NS : MonoBehaviour
{
    public GameObject bottle;

    GameObject audioBGM;
    void Start()
    {
        flashLight.SetActive(false);
        doorVFX.SetActive(false);
        bottle.SetActive(false);

        audioBGM = GetComponentInChildren<AudioSource>().gameObject;
        audioBGM.SetActive(false);
        audioDoorOpen.SetActive(false);
    }

    public bool isCatch; // 유령잡는기능 연결 퍼블릭 지우기
    public Animator doorAnim;

    public GameObject flashLight;
    public GameObject doorVFX;
    public GameObject text;

    public GameObject audioDoorOpen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("1");
            flashLight.SetActive(true);
            bottle.SetActive(true);
        }

        if (isCatch == true)
        {
            audioDoorOpen.SetActive(true);
            doorAnim.SetBool("open", true);
            doorVFX.SetActive(true);
        }

        if (text == null)
        {
            audioBGM.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
