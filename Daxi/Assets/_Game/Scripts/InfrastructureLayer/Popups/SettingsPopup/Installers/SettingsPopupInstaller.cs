using Zenject;
using UnityEngine;

namespace Daxi.InfrastructureLayer.Popups.SettingsPopup.Installers
{
    public class SettingsPopupInstaller:MonoInstaller<SettingsPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private SettingsPopup _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<SettingsPopup, SettingsPopup.SettingsPopupFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsTransient();
        }
        #endregion
    }
}
