using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier; 
    [SerializeField] private Camera mainCamera; 


    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = mainCamera.transform;
        lastCameraPosition = cameraTransform.position;
    }

    void FixedUpdate()
    {
        Vector3 deltaMovment = cameraTransform.position - lastCameraPosition;
        transform.position -= new Vector3(deltaMovment.x * parallaxEffectMultiplier.x, deltaMovment.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;
    }
}
