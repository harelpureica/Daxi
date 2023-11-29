using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finnishLine : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private bool missionDone;
    private bool isFinnished = false;
    private bool alreadyStopped = false;

    // private void Update()
    // {/*
    //     if (isFinnished && player.GetComponent<Movment>().Speed > 0)
    //     {
    //         StartCoroutine(slowStop());
    //     }
    //     else if (player.GetComponent<Movment>().Speed <= 0 && !alreadyStopped && isFinnished)
    //     {
    //         alreadyStopped = true;
    //         player.GetComponent<Animator>().SetTrigger("stand");
    //         StartCoroutine(wait1sec());
    //     }*/
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isFinnished = true;
        }
    }

    private void stand()
    {

    }
    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(1);
        if (missionDone)
        {
            player.GetComponent<Animator>().SetTrigger("victory");

        }
        else
        {
            player.GetComponent<Animator>().SetTrigger("lose");
        }
    }
    
    IEnumerator slowStop()
    {
        yield return new WaitForSeconds(0.3f);
        if (player.GetComponent<Movment>().Speed > 0)
        {
            player.GetComponent<Movment>().Speed -= 0.02f;
        }
    }
}
