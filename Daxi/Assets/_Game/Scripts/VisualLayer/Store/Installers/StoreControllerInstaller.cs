using Daxi.DataLayer.StoreData;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Store.Installers
{
    public class StoreControllerInstaller:MonoInstaller
    {
        #region Fields
        [SerializeField]
        private List<StoreItem> _hearts;

        [SerializeField]
        private List<StoreItem> _powers;

        [SerializeField]
        private List<StoreItem> _pets;

        [SerializeField]
        private List<StoreItem> _skins;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<StoreController>()
                .AsSingle();

            Container
                .Bind<List<StoreItem>>()
                .WithId("Hearts")
                .FromInstance(_hearts)
                .AsTransient();

            Container
                .Bind<List<StoreItem>>()
                .WithId("Powers")
                .FromInstance(_powers)
                .AsTransient();

            Container
                .Bind<List<StoreItem>>()
                .WithId("Pets")
                .FromInstance(_pets)
                .AsTransient();

            Container
                .Bind<List<StoreItem>>()
                .WithId("Skins")
                .FromInstance(_skins)
                .AsTransient();
        }
        #endregion
    }
}
