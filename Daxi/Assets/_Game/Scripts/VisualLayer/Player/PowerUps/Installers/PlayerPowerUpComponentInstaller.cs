
using Daxi.DataLayer.Configuration.PowerUps;
using Daxi.DataLayer.GameItems;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.PowerUps.Installers
{
    public class PlayerPowerUpComponentInstaller:MonoInstaller<PlayerPowerUpComponentInstaller>
    {
        #region Fields
      

        [SerializeField]
        private PowerupsComponentSettings _settings;
        #endregion

        #region Methods
        public override void InstallBindings()
        {            

            Container
                .BindInterfacesAndSelfTo<PlayerPowerUpComponent>()
                .AsSingle();

            Container
                .Bind<PowerupsComponentSettings>()
                .FromInstance(_settings)
                .AsSingle();
        }
        #endregion
    }
}
