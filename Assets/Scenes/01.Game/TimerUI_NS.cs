using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerUI_NS : MonoBehaviour
{
    static TimerUI_NS instance;
    private void Awake()
    {
        instance = this;
    }

    // �ð��� ǥ���ϴ� text UI�� ����Ƽ���� �����´�.
    public Text gameTime;
    // ��ü ���� �ð��� �������ش�. 3��=180��.
    float setTime = 180;
    // �д����� �ʴ����� ����� ������ ������ش�.
    int min;
    float sec;

    public GameObject gameOverUI;
    private void Start()
    {
        gameOverUI.SetActive(false);
        doorFX.SetActive(false);
        roomLight.SetActive(false);
    }

    public int catchGhostCount = 5;

    public Animator doorAnim;
    public GameObject doorFX;
    public GameObject roomLight;
    void Update()
    {
        // ���� �ͽ� �� ����
        if (catchGhostCount == 0)
        {
            doorAnim.SetBool("open", true);
            doorFX.SetActive(true);
            roomLight.SetActive(true);
        }

        // ���� �ð��� ���ҽ����ش�.
        setTime -= Time.deltaTime;

        // ��ü �ð��� 60�� ���� Ŭ ��
        if (setTime >= 60f)
        {
            // 60���� ������ ����� ���� �д����� ����
            min = (int)setTime / 60;
            // 60���� ������ ����� �������� �ʴ����� ����
            sec = setTime % 60;
            // UI�� ǥ�����ش�
            if (sec < 10)
            {
                gameTime.text = "0" + min + " : 0" + (int)sec;
            }
            else
            {
                gameTime.text = "0" + min + " : " + (int)sec;
            }
        }

        // ��ü�ð��� 60�� �̸��� ��
        if (setTime < 60f)
        {
            // �� ������ �ʿ�������Ƿ� �ʴ����� ������ ����
            if (setTime < 10)
            {
                gameTime.text = "00 : 0" + (int)setTime;
            }
            else
            {
                gameTime.text = "00 : " + (int)setTime;
            }
        }

        // ���� �ð��� 0���� �۾��� ��
        if (setTime <= 0)
        {
            // UI �ؽ�Ʈ�� 0�ʷ� ������Ŵ.
            gameTime.text = "00 : 00";
            gameOverUI.SetActive(true);
            Invoke("LoadScene", 5);
        }

    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
