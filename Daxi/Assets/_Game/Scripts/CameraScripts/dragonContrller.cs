using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonContrller : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;

    private bool trigger;

    void Start()
    {
        triggerFunction();
    }

    void FixedUpdate()
    {
        if (trigger)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + parallaxEffectMultiplier, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - parallaxEffectMultiplier, transform.position.z);
        }
    }

    public void triggerFunction()
    {
        if(trigger == true)
        {
            trigger = false;
        }
        else
        {
            trigger = true;
        }
        StartCoroutine(wait2sec());
    }

    IEnumerator wait2sec()
    {
        yield return new WaitForSeconds(2);
        triggerFunction();
    }
}
