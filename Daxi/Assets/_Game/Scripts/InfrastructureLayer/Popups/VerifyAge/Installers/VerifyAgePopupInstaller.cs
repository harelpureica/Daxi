using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.VerifyAge.Installers
{
    public class VerifyAgePopupInstaller:MonoInstaller<VerifyAgePopupInstaller>
    {
        #region Fields
        [SerializeField]
        private VerifyAgePopup _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                 .BindFactory<VerifyAgePopup, VerifyAgePopup.VerifyAgePopupFactory>()
                 .FromComponentInNewPrefab(_prefab)
                 .AsSingle();

        }
        #endregion
    }
}
