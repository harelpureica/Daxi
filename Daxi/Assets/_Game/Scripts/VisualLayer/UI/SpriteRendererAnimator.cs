

using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Daxi.VisualLayer.UI
{
    public class SpriteRendererAnimator:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private List<Sprite> _sprites;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private int _frameIntervalMilSec;

        private int _frameIndex;

        private bool _isPlaying;

        #endregion
        #region Methods
        private async void Start()
        {

            _isPlaying = true;
            while (_isPlaying)
            {
                if (_spriteRenderer == null)
                {
                    return;
                }
                _spriteRenderer.sprite = _sprites[_frameIndex];
                _frameIndex++;
                if (_frameIndex >= _sprites.Count - 1)
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
