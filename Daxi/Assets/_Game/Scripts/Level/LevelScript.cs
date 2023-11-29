using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{    
    [SerializeField] private List<Sprite> powersOptions;
    [SerializeField] private List<GameObject> powerButtons;
    [SerializeField] private List<Sprite> powersOptionsLeft;
    private int buttonToUpdate = 0;

    void Start()
    {
        powersOptionsLeft =new List<Sprite>(powersOptions);
    }
    public void PowerRecieved()
    {
        if(buttonToUpdate<powerButtons.Count)
        {
            int chosenPower = Random.Range(0,powersOptionsLeft.Count);
            powerButtons[buttonToUpdate].GetComponent<Image>().sprite = powersOptionsLeft[chosenPower];
            powerButtons[buttonToUpdate].SetActive(true);
            powersOptionsLeft.RemoveAt(chosenPower); 
            buttonToUpdate++;
        }
        
    }
            
    public void powerWasUsed(Sprite powerToReturnToList)
    {
        // if(buttonToUpdate!=0)
        //     powerButtons[buttonToUpdate-1].SetActive(false);
        powersOptionsLeft.Add(powerToReturnToList);
        buttonToUpdate--;
    }
}
