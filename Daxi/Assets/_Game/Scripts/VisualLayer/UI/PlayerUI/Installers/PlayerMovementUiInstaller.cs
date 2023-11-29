using Zenject;
using UnityEngine;

namespace Daxi.VisualLayer.UI.PlayerUI.Installers
{
    public class PlayerMovementUiInstaller:MonoInstaller<PlayerMovementUiInstaller>
    {
        #region Fields
        [SerializeField]
        private PlayerMovementUi _playerMovementUi;

        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerMovementUi>()
                .FromInstance(_playerMovementUi)
                .AsSingle();
        }
        #endregion
    }
}
