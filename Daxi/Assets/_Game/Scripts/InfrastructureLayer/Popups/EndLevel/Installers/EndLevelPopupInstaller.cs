using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.EndLevel.Installers
{
    public class EndLevelPopupInstaller:MonoInstaller<EndLevelPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private EndLevelPopup _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<EndLevelPopup, EndLevelPopup.EndLevelPopupFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle();
        }
        #endregion       
    }
}
