using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Configuration.Sliding;
using Daxi.VisualLayer.Player;
using System;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Sliding
{
    public class SlidingComponent
    {
        #region Injects
        [Inject]
        private CapsuleCollider2D _collider;

        [Inject]
        private SlidingSettings _slidingSettings;      
        #endregion

        #region Fields
        private bool _sliding;
        #endregion


        #region Properties
        public bool Sliding => _sliding;
        #endregion

        #region Methods
        public async void Slide()
        {          
            _sliding = true;
            var lerp = 0f;
            while (lerp < 1)
            {
                _collider.size = Vector2.Lerp(_slidingSettings.ColliderStartSize, _slidingSettings.ColliderShrinkSize, lerp);
                _collider.offset = Vector2.Lerp(_slidingSettings.ColliderStartOffset, _slidingSettings.ColliderShrinkOffset, lerp);
                lerp += Time.deltaTime/0.2f;
                await UniTask.Yield();
            }
            _collider.size = _slidingSettings.ColliderShrinkSize;
            _collider.offset = _slidingSettings.ColliderShrinkOffset;

        }
        public async void StopSliding()
        { 
            if(!_sliding)
            {
                return;
            }
            var lerp = 0f;
            while(lerp<1)
            {
                _collider.size = Vector2.Lerp( _slidingSettings.ColliderShrinkSize,_slidingSettings.ColliderStartSize,lerp);
                _collider.offset =Vector2.Lerp(_slidingSettings.ColliderShrinkOffset, _slidingSettings.ColliderStartOffset,lerp);
                lerp += Time.deltaTime/0.2f;
                await UniTask.Yield();
            }
            _collider.size = _slidingSettings.ColliderStartSize;
            _collider.offset = _slidingSettings.ColliderStartOffset;
            _sliding = false;
        }
      
        #endregion
    }
}
