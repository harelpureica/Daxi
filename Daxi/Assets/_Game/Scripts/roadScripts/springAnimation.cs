using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springAnimation : MonoBehaviour
{
    [SerializeField]private Animator animator;

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            animator.SetTrigger("OnIt");
        }
    }
    
}
