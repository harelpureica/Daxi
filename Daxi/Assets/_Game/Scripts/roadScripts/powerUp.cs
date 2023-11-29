using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class powerUp : MonoBehaviour 
{        
    [SerializeField] GameObject LevelManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("Player"))
        {
            RandomPower();
        }
        Destroy(gameObject);
    }      

    public void RandomPower()
    {                       
        LevelManager.GetComponent<LevelScript>().PowerRecieved();
    }

 
}
