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

    // 시간을 표시하는 text UI를 유니티에서 가져온다.
    public Text gameTime;
    // 전체 제한 시간을 설정해준다. 3분=180초.
    float setTime = 180;
    // 분단위와 초단위를 담당할 변수를 만들어준다.
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
        // 잡은 귀신 수 연동
        if (catchGhostCount == 0)
        {
            doorAnim.SetBool("open", true);
            doorFX.SetActive(true);
            roomLight.SetActive(true);
        }

        // 남은 시간을 감소시켜준다.
        setTime -= Time.deltaTime;

        // 전체 시간이 60초 보다 클 때
        if (setTime >= 60f)
        {
            // 60으로 나눠서 생기는 몫을 분단위로 변경
            min = (int)setTime / 60;
            // 60으로 나눠서 생기는 나머지를 초단위로 설정
            sec = setTime % 60;
            // UI를 표현해준다
            if (sec < 10)
            {
                gameTime.text = "0" + min + " : 0" + (int)sec;
            }
            else
            {
                gameTime.text = "0" + min + " : " + (int)sec;
            }
        }

        // 전체시간이 60초 미만일 때
        if (setTime < 60f)
        {
            // 분 단위는 필요없어지므로 초단위만 남도록 설정
            if (setTime < 10)
            {
                gameTime.text = "00 : 0" + (int)setTime;
            }
            else
            {
                gameTime.text = "00 : " + (int)setTime;
            }
        }

        // 남은 시간이 0보다 작아질 때
        if (setTime <= 0)
        {
            // UI 텍스트를 0초로 고정시킴.
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
