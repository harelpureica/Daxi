using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPower : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject board;    

    [SerializeField] private Animator animator;   

    private void Start()
    {
        animator = GetComponent<Animator>();
    }   

    public void UsePower(string spriteName)
    {            
        if (spriteName== "Gum button")
        {
            animator.SetTrigger("gum start");
            StartCoroutine(gumOn());
            gameObject.GetComponent<PlayerMovement>().GumEffect();
        }
        else if (spriteName == "Shield button")
        {
            shield.SetActive(true);
            StartCoroutine(waitShield());
            // the character is immune while has a shield. taken care of in the Movment class. 
        }
        else if (spriteName == "Board button")
        {
            //throw a board under Daxi
            useBoard();
        }        
    }

public void useBoard()
    {
        //its from type "Ground" to be able to jump
        GameObject boardObj = Instantiate(board, transform);
        int LayerNameToInt = LayerMask.NameToLayer("Ground");
        boardObj.gameObject.layer = LayerNameToInt;
    }
   IEnumerator waitShield()
    {
        yield return new WaitForSeconds(15);
        shield.SetActive(false);
        //remove the imunne
    }
    IEnumerator gumOn()
    {
        yield return new WaitForSeconds(15);
        animator.SetTrigger("finnish gum");
    }    
}


