using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Daxi.VisualLayer.UI
{
    public class UiImageAnimator:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private List<Sprite> sprites;

        [SerializeField]
        private Image _image;        

        [SerializeField]
        private int _frameIntervalMilSec;

        private int _frameIndex;

        private bool _isPlaying;

        #endregion
        #region Methods
        private async void OnEnable()
        {
            
            _isPlaying = true;
            while (_isPlaying)
            {      
                if(_image == null)
                {
                    return;
                }
                _image.sprite = sprites[_frameIndex];
                _frameIndex++;
                if(_frameIndex>= sprites.Count-1)
                {
                    _frameIndex = 0;
                }
                await UniTask.Delay(_frameIntervalMilSec);

            }
        }
        private void OnDisable()
        {
            _isPlaying = false;
        }

        #endregion
    }

}
