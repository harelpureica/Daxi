using UnityEngine;
using Zenject;
namespace Daxi.InfrastructureLayer.Popups.ConfirmedTerms.Installers
{
    public class ConfirmedTermsPopupInstaller:MonoInstaller<ConfirmedTermsPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private ConfirmedTermsPopup _prefab;
        public override void InstallBindings()
        {
            Container
                .BindFactory<ConfirmedTermsPopup, ConfirmedTermsPopup.ConfirmedTermsPopupFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsTransient();
        }
        #endregion
    }
}
