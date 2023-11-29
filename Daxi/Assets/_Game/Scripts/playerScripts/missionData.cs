using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class missionData : MonoBehaviour
{
    [SerializeField] private int counter;
    [SerializeField] private GameObject missionButton;
    [SerializeField] private int goal;
    private string missionText;

    private void Start()
    {
        missionText = missionButton.GetComponentInChildren<TextMeshProUGUI>().text;
    }
    public void pickUpForMission()
    {
        if(counter < goal)
        {
            counter++;
            missionButton.GetComponentInChildren<TextMeshProUGUI>().text = counter + missionText.Substring(missionText.IndexOf('/'));
        }
    }
}
