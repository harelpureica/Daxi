using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pliersAni : MonoBehaviour
{
    [SerializeField] private Animator animator;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animator.SetTrigger("OnPlier");
        }
    }
}
