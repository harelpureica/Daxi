using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    private Animator ani;
    private SpriteRenderer spr;
    [SerializeField] private int skinNum;
    [SerializeField] private List<Sprite> skinImg;
    [SerializeField] private List<RuntimeAnimatorController> controllers;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        spr.sprite = skinImg[skinNum - 1];
        ani.runtimeAnimatorController = controllers[skinNum - 1] as RuntimeAnimatorController;
    }
}
