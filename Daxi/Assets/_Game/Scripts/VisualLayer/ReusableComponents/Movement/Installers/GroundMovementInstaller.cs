using Daxi.DataLayer.Configuration.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Movement.Installers
{
    public class GroundMovementInstaller : MonoInstaller<GroundMovementInstaller>
    {
        #region Fields     
        [SerializeField]
        private GroundMovementSettings _settings;
        #endregion
        public override void InstallBindings()
        {
            Container
                .Bind<IGroundMovement>()
                .To<GroundMovement>()
                .AsSingle();

            Container
                .Bind<GroundMovementSettings>()
                .FromInstance(_settings)
                .AsSingle();          
        }
    }
}
