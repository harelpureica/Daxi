using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.UI.PlayerUI.Installers
{
    public class PlayerHealthUiInstaller:MonoInstaller<PlayerHealthUiInstaller>
    {
        #region Fields
        [SerializeField]
        private PlayerHealthUi _playerHealthUi;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerHealthUi>()
                .FromInstance(_playerHealthUi)
                .AsSingle();
        }
        #endregion
    }
}
