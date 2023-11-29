
using Daxi.DataLayer.GameItems;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.UI.PlayerUI.Installers
{
    public class PlayerPowerUpUiInstaller:MonoInstaller<PlayerPowerUpUiInstaller>
    {
        #region Fields
        [SerializeField]
        private PlayerPowerUpUi _playerPowerUpUi;

        [SerializeField]
        private List<GameItemData> _powerUpsDatas;
      
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerPowerUpUi>()
                .FromInstance(_playerPowerUpUi)
                .AsSingle();

            Container
              .Bind<List<GameItemData>>()
              .FromInstance(_powerUpsDatas)
              .AsSingle();

        }
        #endregion
    }
}
