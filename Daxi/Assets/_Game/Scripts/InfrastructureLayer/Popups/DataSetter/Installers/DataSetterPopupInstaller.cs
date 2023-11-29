using Zenject;
using UnityEngine;
using Assets._Game.Scripts.InfrastructureLayer.Popups.DataSetter;

namespace Daxi.InfrastructureLayer.Popups.DataSetter.Installers
{
    public class DataSetterPopupInstaller:MonoInstaller<DataSetterPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private DataSetterPopup _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<DataSetterPopup, DataSetterPopup.DataSetterPopupFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle();            
        }
        #endregion
    }
}
