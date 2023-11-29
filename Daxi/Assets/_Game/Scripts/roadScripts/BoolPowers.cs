using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolPowers : MonoBehaviour
{
    [SerializeField] private bool power1;
    [SerializeField] private bool power2;
    [SerializeField] private bool power3;
    [SerializeField] private int random;

    public bool Power1
    {
        get
        {
            return this.power1;
        }
        set
        {
            power1 = value;
        }
    }
    public bool Power2
    {
        get
        {
            return this.power2;
        }
        set
        {
            power2 = value;
        }
    }
    public bool Power3
    {
        get
        {
            return this.power3;
        }
        set
        {
            power3 = value;
        }
    }
    public int Random
    {
        get
        {
            return this.random;
        }
        set
        {
            random = value;
        }
    }
}
