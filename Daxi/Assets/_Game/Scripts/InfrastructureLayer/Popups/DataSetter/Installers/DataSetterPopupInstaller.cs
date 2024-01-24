using Zenject;
using UnityEngine;
using System.Collections.Generic;
using Daxi.DataLayer.StoreData;

namespace Daxi.InfrastructureLayer.Popups.DataSetter.Installers
{
    public class DataSetterPopupInstaller:MonoInstaller<DataSetterPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private DataSetterPopup _prefab;

        [SerializeField]
        private List<StoreItem> _skins;

        [SerializeField]
        private List<StoreItem> _pets;

        [SerializeField]
        private List<StoreItem> _hearts;

        [SerializeField]
        private List<StoreItem> _powers;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<DataSetterPopup, DataSetterPopup.DataSetterPopupFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle();
            
            Container
                .Bind<List<StoreItem>>()
                .WithId("Skins")
                .FromInstance(_skins) 
                .AsTransient();

            Container
               .Bind<List<StoreItem>>()
               .WithId("Pets")
               .FromInstance(_pets)
               .AsTransient();

            Container
               .Bind<List<StoreItem>>()
               .WithId("Powers")
               .FromInstance(_powers)
               .AsTransient();

            Container
               .Bind<List<StoreItem>>()
               .WithId("Hearts")
               .FromInstance(_hearts)
               .AsTransient();
        }
        #endregion
    }
}
