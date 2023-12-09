using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Daxi.InfrastructureLayer.Audio
{
    public class ButtonsAudioPlayer:MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip clip;

        private static ButtonsAudioPlayer instance;
        private void OnEnable()
        {
            ButtonsAudioPlayer[] objs = GameObject.FindObjectsOfType<ButtonsAudioPlayer>();

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
                return;
            }
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
       
        private void OnDisable()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
        private void OnApplicationQuit()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
        private void Update()
        {
            if (IsPointerOverUIButton())
            {
                if(!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(clip);
                }
            }
        }
       
        private bool IsPointerOverUIButton()
        {
            if(SystemInfo.deviceType!=DeviceType.Handheld)
            {
                if(!Input.GetMouseButtonDown(0))
                {
                    return false;
                }
            }else
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase != TouchPhase.Began)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
           
           
           
           
            // Check if the pointer is over a UI element
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            
            // Create a list to store the results of the raycast
            var results = new List<RaycastResult>();

            if(eventData==null || results==null)
            {
                return false;
            }
            // Raycast using the GraphicsRaycaster of the EventSystem
            EventSystem.current.RaycastAll(eventData, results);

            // Check if any of the raycast results are UI elements with a Button component
            foreach (var result in results)
            {
                // Check if the GameObject has a Button component
                if (result.gameObject.GetComponent<Button>() != null)
                {
                    return true;
                }
            }

            // If no UI element with a Button component is found
            return false;
        }
    }
}
