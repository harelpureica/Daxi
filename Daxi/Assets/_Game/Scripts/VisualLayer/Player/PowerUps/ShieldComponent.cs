using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.PowerUps
{
    public class ShieldComponent
    {
        #region Injects
        [Inject]
        private PlayerAnimationComponent _playerAnimationComponent;

        [Inject(Id ="Shield")]
        private GameObject _shield;
        #endregion

        #region Methods
        public void Shield()
        {
            _shield.SetActive(true);
            _playerAnimationComponent.AnimateShield();
        }
        public  async UniTask EndShield()
        {
            _playerAnimationComponent.AnimateEndShield();
            await UniTask.Delay(1000);
            _shield.SetActive(false);
        }
        #endregion
    }
}
