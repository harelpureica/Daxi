﻿using Zenject;
using UnityEngine;

namespace Daxi.InfrastructureLayer.Ads
{
    public class AdsManagerInstaller:MonoInstaller<AdsManagerInstaller>
    {
        [SerializeField]
        private bool _inProduction;

        //[SerializeField]
        //private AdsManager _adsManager;
       
        #region Methods
        public override void InstallBindings()
        {
            //Container
            //    .Bind<AdsManager>() 
            //    .FromInstance(_adsManager)
            //    .AsSingle();

            Container
                .Bind<bool>()
                .WithId("Production")
                .FromInstance(_inProduction)
                .AsSingle();
               
        }
        #endregion
    }
}
