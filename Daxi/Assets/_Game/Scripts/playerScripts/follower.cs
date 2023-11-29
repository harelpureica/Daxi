using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower : MonoBehaviour
{
    [SerializeField] private GameObject Character;
    [SerializeField] private float speed = 1.5f;
    void Update()
    {
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Character.transform.position, speed * Time.deltaTime);

    }
}
