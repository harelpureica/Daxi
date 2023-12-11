

using Cysharp.Threading.Tasks;
using Daxi.VisualLayer.UI.PlayerUI;
using System;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.PowerUps
{
    public class GumMovementComponent:IInitializable,IFixedTickable
    {
        #region Injects
        [Inject]
        private Rigidbody2D _rb;

        [Inject]
        private PlayerMovementUi _playerMovementUi;

        [Inject]
        private PlayerManager _playerManager;

        private bool _active;

        private bool _movingUp;

        private bool _movingDown;

        #endregion

        #region Methods    
        public void SetActive(bool active)
        {            
           if(_active==active) return;

            if(active)
            {
                _rb.gravityScale = 0.05f;
                if(!_active)
                {
                    _rb.AddForce(Vector2.up *1.8f, ForceMode2D.Impulse);
                }
                
                _playerManager.PlayClip(PlayersClipInfo.PlayersClipType.gum);
            }else
            {
                _playerManager.PlayClip(PlayersClipInfo.PlayersClipType.endgum);

            }
            _active = active;
        }       

        public void Initialize()
        {
            _playerMovementUi.OnDownClickDown += (() => SetMovingDown(true));
            _playerMovementUi.OnDownClickUp += (() => SetMovingDown(false));
            _playerMovementUi.OnUpClickDown += (() => SetMovingUp(true));
            _playerMovementUi.OnUpClickUp += (() => SetMovingUp(false));

        }

        public  void SetMovingUp(bool movingUp)       
        {
            _movingUp= movingUp;
        }
        public void SetMovingDown(bool movingDown)
        {
            _movingDown= movingDown;
        }

        public void FixedTick()
        {
            if (!_active)
            {
                return;
            }
            if(_rb.velocity.x<5f)
            {
                _rb.AddForce(Vector2.right*2.5f, ForceMode2D.Force);
            }
            if (_movingUp)
            {
                _rb.AddForce(Vector2.up*3f, ForceMode2D.Force);
                
            }
            else if(_movingDown)
            {
                _rb.AddForce(Vector2.down*3f, ForceMode2D.Force);
            }
        }
        #endregion
    }
}
