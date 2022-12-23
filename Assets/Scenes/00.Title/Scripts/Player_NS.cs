using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

// F��ư ������ �÷��� �� ����
// ������ ������ �� ����(�� �ִϸ��̼� ����)
// ���� �� �ȿ� VFX ȿ�� 
// �÷��̾ �̵��ؼ� ������ ������� �� ��ȯ
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

    public bool isCatch; // ������±�� ���� �ۺ� �����
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
