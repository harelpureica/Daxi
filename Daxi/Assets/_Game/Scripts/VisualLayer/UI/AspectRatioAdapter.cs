using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

[RequireComponent(typeof(AspectRatioFitter))]
public class AspectRatioAdapter : MonoBehaviour
{
    [SerializeField]
    private AspectRatioFitter _fitter;
    private void Awake()
    { 
        _fitter.aspectRatio= (float)UnityEngine.Device.Screen.currentResolution.width/(float)UnityEngine.Device.Screen.currentResolution.height;
    }
    

}
