using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour 
{
    public static pauseMenu instance;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public int countDownTime;
    public TMPro.TextMeshProUGUI countDownDisplay;

    private void Awake()
    {
        instance = this;
    }
    /*private void Start()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        StartCoroutine(CountDownToStart());
        Debug.Log("7");
    }*/

    public void Begin()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator CountDownToStart()
    {
        Debug.Log("2");
        while (countDownTime > 0)
        {
            if (countDownTime == 2)
            {
                Debug.Log("3");
                countDownDisplay.text = "Ready?";
            }
            else
            {
                Debug.Log("4");
                countDownDisplay.text = "Set";
            }
            Debug.Log("5");
            yield return new WaitForSeconds(1f);

            countDownTime--;
        }
        Debug.Log("6");
        countDownDisplay.text = "GO!";

        Begin();

        yield return new WaitForSeconds(1f);
        countDownDisplay.gameObject.SetActive(false);
    }

}
