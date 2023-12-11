using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Configuration.Player;
using Daxi.DataLayer.Configuration.Sliding;
using Daxi.VisualLayer.Player;
using System;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Sliding
{
    public class SlidingComponent:IFixedTickable,IInitializable
    {
        #region Injects
        [Inject]
        private CapsuleCollider2D _collider;

        [Inject]
        private SlidingSettings _slidingSettings;

        [Inject]
        private PlayerSettings _playerSettings;

        [Inject]
        private PlayerManager _playerManager;

        
        #endregion

        #region Fields
        private bool _sliding;

        private Rigidbody2D _rb;
        #endregion


        #region Properties
        public bool Sliding => _sliding;

        public void FixedTick()
        {
            if (_sliding)
            {
                var grounded = Physics2D.OverlapCircle((Vector2)_collider.transform.position + _playerSettings.OverlapOffset, _playerSettings.GroundRadius, _playerSettings.GroundLayer) == null ? false : true;
                if (!grounded)
                {
                    _rb.AddForce(Vector2.down ,ForceMode2D.Impulse);
                }
            }
        }

        public void Initialize()
        {
            _rb=_collider.GetComponent<Rigidbody2D>();
        }
        #endregion

        #region Methods
        public async void Slide()
        {          
            _sliding = true;
            var lerp = 0f;
            _playerManager.PlayClip(PlayersClipInfo.PlayersClipType.slide);

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
