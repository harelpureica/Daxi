using Daxi.DataLayer.Configuration.Camera;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.Installers
{
    public class PlayersCameraControllerInstaller:MonoInstaller<PlayersCameraControllerInstaller>
    {
        #region Fields
        [SerializeField]
        private Camera _cam;       

        [SerializeField]
        private PlayersCameraControllerSettings _settings;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PlayersCameraController>()
                .AsSingle();

            Container
                .Bind<Camera>()
                .FromInstance(_cam)
                .AsSingle();

            Container
               .Bind<PlayersCameraControllerSettings>()
               .FromInstance(_settings)
               .AsSingle();
        }
        #endregion
    }
}
