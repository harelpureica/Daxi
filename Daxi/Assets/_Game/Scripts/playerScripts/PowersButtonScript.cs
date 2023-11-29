using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowersButtonScript : MonoBehaviour
{    
    [SerializeField] private Image image;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelManager;

    private float startTimeOfMask = 0;
    public void PowerUsed()
    {
        if(image.fillAmount==1)
        {
            UpdatePlayer();
            UpdateLevelManager(image.sprite);
            player.GetComponent<ButtonPower>().UsePower(image.name);
        }        
    }
    private void UpdateLevelManager(Sprite imageSprite)
    {
        levelManager.GetComponent<LevelScript>().powerWasUsed(imageSprite);
    }
    private void UpdatePlayer()
    {
        player.GetComponent<ButtonPower>().UsePower(image.mainTexture.name);        
        image.fillAmount = 0f;
        StartCoroutine(ChangesFillAmount(5,image));
    }
    private IEnumerator ChangesFillAmount(float duration,Image image)
    {

        while (startTimeOfMask < duration)
        {
            startTimeOfMask += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(0, 1, startTimeOfMask / duration);
            yield return null;
        }        
    }   
}
