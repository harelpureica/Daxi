using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shopMenu : MonoBehaviour
{
    public void LoadPlay()
    {
        SceneManager.LoadSceneAsync("worlds");
    }

    public void LoadShop()
    {
        SceneManager.LoadSceneAsync("shop");
    }

    public void back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("main");
    }
}
