

using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Configuration.Jumping;
using Daxi.VisualLayer.Player;
using System;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Jumping
{
    public class JumpingComponent:ITickable
    {
        #region Injects
        [Inject]
        private Rigidbody2D _rb;

        [Inject]
        private JumpingSettings _settings;

        [Inject]
        private PlayerManager _playerManager;             

        #endregion

        #region Fields
        private bool _grounded;

        private int _jumps;

        private bool _recentlyJumped;

        private bool _lastJump;

        private CapsuleCollider2D _capsuleCollider;

        #endregion

        #region Properties
        public bool RecentlyJumped=>_recentlyJumped;

        public bool LastJump=> _lastJump;
        #endregion

        #region Methods
        public void Jump()
        {
            
           
            _rb.gravityScale = 1;
            _rb.angularVelocity = 0;
            if (_grounded)
            {
                _playerManager.PlayClip(PlayersClipInfo.PlayersClipType.jump);
                _rb.velocity = new Vector2(_settings.JumpForce * 0.43f, _settings.JumpForce);                
                _jumps++;
                HandleRecentlyJumped();

            }
            else if(_jumps<=_settings.JumpTimes)
            {
                _playerManager.PlayClip(PlayersClipInfo.PlayersClipType.jump);
                _rb.velocity = new Vector2(_settings.JumpForce * 0.43f, _settings.JumpForce*0.9f);
                _jumps++;
                HandleRecentlyJumped();
                if (_jumps>_settings.JumpTimes)
                {
                    HandleLastJump();
                }
            }           
        }

        public void Tick()
        {
            if (_capsuleCollider == null)
            {
                _capsuleCollider = _rb.GetComponent<CapsuleCollider2D>();
            }
            var hitColliders = Physics2D.CapsuleCastAll(_capsuleCollider.bounds.center, _capsuleCollider.bounds.size,CapsuleDirection2D.Vertical,0f,Vector2.down*0.1f,0.1f,LayerMask.GetMask(new string[] { "Ground", "Interaction"}));
            _grounded = false;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (_playerManager.gameObject == hitColliders[i].collider.gameObject)
                {
                    continue;
                }
                else                
                {
                    if(!hitColliders[i].collider. isTrigger)
                    {
                        _grounded = true;
                    }
                }
            }
            if(_grounded&&!_recentlyJumped)
            {
                _jumps = 0;
              
            }
        }

        private async void HandleLastJump()
        {
            var grounded = false;
            _lastJump = true;
            while(!grounded)
            {
                if(_rb==null)
                {
                    return;
                }
                var hit = Physics2D.Raycast(_rb.position, Vector2.down, _settings.GroundDistance, _settings.GroundLayer);
                grounded = hit.collider != null ? true : false;
                await UniTask.Yield();
            }
            _lastJump = false;
        }

        private async void HandleRecentlyJumped()
        {
            if(_recentlyJumped)
            {
                return;
            }
            _recentlyJumped = true;
            await UniTask.Delay(150);
            _recentlyJumped = false;
        }
        #endregion
    }
}
