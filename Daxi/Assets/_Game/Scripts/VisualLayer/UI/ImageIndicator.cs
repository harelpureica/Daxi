
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.UI
{
    public class ImageIndicator:MonoBehaviour
    {
       
        #region Fields
        [SerializeField]
        private Image _image;
        #endregion

        #region Methods

        public void SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
        public void SetAlpha(float alpha)
        {
            _image.color = new Color(_image.color.r,_image.color.g,_image.color.b,Mathf.Clamp01(alpha));
        }
        #endregion

    }
}
