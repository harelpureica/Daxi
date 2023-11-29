using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableBoard : MonoBehaviour
{
    [SerializeField] private UnityEvent interaction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interaction.Invoke();
            Destroy(gameObject);
        }
    }
}
