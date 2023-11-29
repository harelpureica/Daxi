using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class worldALevels : MonoBehaviour
{
    public GameObject MissionPanel;
   
    public void LoadWorlds()
    {
        SceneManager.LoadSceneAsync("worlds");
        LevelLoader("lvl 1");
    }

    public void LevelLoader(string levelToLoad)
    {
        StartCoroutine(LoadSceneAsyncMethod(levelToLoad));
    }

    IEnumerator LoadSceneAsyncMethod(string levelToLoad)
    {        
        MissionPanel.SetActive(true);
        AsyncOperation loadingLevelOperation = SceneManager.LoadSceneAsync(levelToLoad);
        while (!loadingLevelOperation.isDone)
        {
                      
            yield return null;
        }

        loadingLevelOperation.allowSceneActivation = true;
    }

    public void LoadLvl1()
    {
        LevelLoader("lvl 1");
        Debug.Log("lvl 1");
        //SceneManager.LoadSceneAsync("lvl 1");
    }
    public void LoadLvl2()
    {
        LevelLoader("lvl 2");
        Debug.Log("1");
    }
    public void LoadLvl3()
    {
        //SceneManager.LoadSceneAsync("lvl 3");
    }
    public void LoadLvl4()
    {
        //SceneManager.LoadSceneAsync("lvl 4");
    }
    public void LoadLvl5()
    {
        //SceneManager.LoadSceneAsync("lvl 5");
    }
    public void LoadLvl6()
    {
        //SceneManager.LoadSceneAsync("lvl 6");
    }

}
