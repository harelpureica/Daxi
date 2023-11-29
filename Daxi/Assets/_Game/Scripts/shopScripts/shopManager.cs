using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopManager : MonoBehaviour
{
    //public int coins;
    //public TMP_Text coin_UI;
    public shopItemSO[] shopItemsSO;
    public shopTemplate[] shopPanels;

    public void Start()
    {
        LoadPanels();
    }

    public void LoadPanels()
    {
        for(int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            if(shopItemsSO[i].basecost == 0)
            {
                shopPanels[i].costTxt.text = "free";
            }
            else
            {
                shopPanels[i].costTxt.text = shopItemsSO[i].basecost.ToString();
            }
        }
    }
}
