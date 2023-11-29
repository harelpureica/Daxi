

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Daxi.VisualLayer.UI
{
    public class DaxiButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Fields
        [SerializeField]
        private Image _AlbedoImage;


        [SerializeField]
        [Range(0f,1f)]
        private float _alpha = 0.5f;

        #endregion

        #region Events

        public event Action OnClickDown;

        public event Action OnClickUp;
     
        #endregion


        #region Methods
        public void OnPointerDown(PointerEventData eventData)
        {
           
            OnClickDown?.Invoke();
            var color = _AlbedoImage.color;
            color.a = 0.7f;
            _AlbedoImage.color = color;

        }

      
        public void OnPointerUp(PointerEventData eventData)
        {
            OnClickUp?.Invoke();
            var color = _AlbedoImage.color;
            color.a = 1f;
            _AlbedoImage.color = color;

        }
        #endregion

    }
}
