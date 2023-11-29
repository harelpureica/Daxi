using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.OutOfhearts.Installers
{
    public class OutOfHeartsPopupInstaller:MonoInstaller<OutOfHeartsPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private OutOfHeartsPopup _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<OutOfHeartsPopup, OutOfHeartsPopup.OutOfHeartsPopupFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle();
        }
        #endregion
    }
}
