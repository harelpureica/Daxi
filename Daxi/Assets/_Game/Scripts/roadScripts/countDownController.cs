using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class countDownController: MonoBehaviour
{
    public int countDownTime;
    public TMPro.TextMeshProUGUI countDownDisplay;

    public void Start()
    {
        StartCoroutine(CountDownToStart());
    }
    IEnumerator CountDownToStart()
    {
        while (countDownTime > 0)
        {
            if(countDownTime == 2)
            {
                countDownDisplay.text = "Ready?";
            }
            else
            {
                countDownDisplay.text = "Set";
            }

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

        countDownDisplay.text = "GO!";

        pauseMenu.instance.Resume();

        yield return new WaitForSeconds(1f);

        countDownDisplay.gameObject.SetActive(false);
    }
}
