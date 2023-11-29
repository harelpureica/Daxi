using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Terms.Installers
{
    public class TermsPopupInstaller:MonoInstaller<TermsPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private TermsPopup _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<TermsPopup, TermsPopup.TermsPopupFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle();
        }
        #endregion
    }
}
