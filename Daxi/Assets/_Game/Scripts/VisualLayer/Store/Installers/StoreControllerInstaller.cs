﻿using Daxi.DataLayer.StoreData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField]
        private StoreMenu _menu;

        [SerializeField]
        private Button _heartBtn;

        [SerializeField]
        private Button _powersBtn;

        [SerializeField]
        private Button _petsBtn;

        [SerializeField]
        private Button _skinsBtn;

        [SerializeField]
        private Button _selectBtn;

        [SerializeField]
        private StoreItemUi _storeItemUiPrefab;

        [SerializeField]
        private RectTransform _parent;

        [SerializeField]
        private Button _backBtn;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<Button>()
                .WithId("Select")
                .FromInstance(_selectBtn)
                .AsTransient();

            Container
               .Bind<Button>()
               .WithId("Back")
               .FromInstance(_backBtn)
               .AsTransient();

            Container
               .Bind<Button>()
               .WithId("Hearts")
               .FromInstance(_heartBtn)
               .AsTransient();

            Container
              .Bind<Button>()
              .WithId("Powers")
              .FromInstance(_powersBtn)
              .AsTransient();

            Container
              .Bind<Button>()
              .WithId("Pets")
              .FromInstance(_petsBtn)
              .AsTransient();

            Container
              .Bind<Button>()
              .WithId("Skins")
              .FromInstance(_skinsBtn)
              .AsTransient();

            Container
                .BindInterfacesAndSelfTo<StoreController>()
                .AsSingle();

            Container
                .Bind<List<StoreItem>>()
                .WithId("Hearts")
                .FromInstance(_hearts)
                .AsTransient();

            Container
               .Bind<StoreMenu>()
               .FromInstance(_menu)
               .AsSingle();

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

            Container
               .BindFactory<StoreItemUi,StoreItemUi.StoreItemUiFactory>()
               .FromComponentInNewPrefab(_storeItemUiPrefab)
               .UnderTransform(_parent)
               .AsSingle();
        }
        #endregion
    }
}
