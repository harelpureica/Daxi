using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonsControl : MonoBehaviour
{
    public Button world2Button;
    public GameObject lockMaskWorld2;
    public GameObject TitleImgWorld2;
    public Button world3Button;
    public GameObject lockMaskWorld3;
    public GameObject TitleImgWorld3;

    private bool wasIn1 = false;
    private bool wasIn2 = false;
    void Start()
    {
        if (!wasIn1)
        {
            TitleImgWorld2.gameObject.SetActive(false);
            world2Button.interactable = false;
        }
        if (!wasIn2)
        {
            TitleImgWorld3.gameObject.SetActive(false);
            world3Button.interactable = false;
        }
    }

    void Update()
    {
        if (checkFinnishWorld1() && wasIn1)
        {
            TitleImgWorld2.gameObject.SetActive(true);
            lockMaskWorld2.gameObject.SetActive(false);
            world2Button.interactable = true;
            wasIn1 = false;
        }
        if (checkFinnishWorld2() && wasIn2)
        {
            TitleImgWorld3.gameObject.SetActive(true);
            lockMaskWorld3.gameObject.SetActive(false);
            world3Button.interactable = true;
            wasIn2 = false;
        }
    }

    public bool checkFinnishWorld1()
    {
        return true;
    } 
    public bool checkFinnishWorld2()
    {
        return true;
    } 

}
