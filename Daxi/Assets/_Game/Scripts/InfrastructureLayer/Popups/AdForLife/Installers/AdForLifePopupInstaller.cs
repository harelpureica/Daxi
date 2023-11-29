using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.AdForLife.Installers
{
    public class AdForLifePopupInstaller:MonoInstaller<AdForLifePopupInstaller>
    {
        #region Fields
        [SerializeField]
        private AdForLifePopup prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<AdForLifePopup, AdForLifePopup.AdForLifePopupFactory>()
                .FromComponentInNewPrefab(prefab)
                .AsSingle();
        }
        #endregion
    }
}
