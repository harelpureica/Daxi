using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardTrap : MonoBehaviour
{
    [SerializeField] private GameObject board1;
    [SerializeField] private Animator animator1;
    [SerializeField] private GameObject board2;
    [SerializeField] private Animator animator2;


    public void triggerBoard()
    {
       animator1.SetTrigger("on");
       animator2.SetTrigger("on");
    }
}
