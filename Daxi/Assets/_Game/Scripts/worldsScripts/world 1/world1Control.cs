using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class world1Control : MonoBehaviour
{
    public GameObject[] lockImages;
    public Button[] lvlsButtons;
    public bool[] openlvls;

    private int checklvl = 0; 
    void Start()
    {
        for (int i = 0; i < lvlsButtons.Length; i++)
        {
            lvlsButtons[i].interactable = false;
        }
        for (int i = 0; i < openlvls.Length; i++)
        {
            if (openlvls[i])
            {
                lvlsButtons[i].interactable = true;
                lockImages[i].SetActive(false);
                checklvl = i + 1;
            }
            else
            {
                break;
            }
        }
    }

    
    void Update()
    {
        if (checklvl < openlvls.Length)
        {
            if (openlvls[checklvl])
            {
                lvlsButtons[checklvl].interactable = true;
                lockImages[checklvl].SetActive(false);
                checklvl++;
            }
        }
    }
}
