using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.SplashScreen
{
    public class SplashScreenInstaller:MonoInstaller<SplashScreenInstaller>
    {
        #region Fields
        [SerializeField]
        private SplashScreen _screen;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<SplashScreen>()
                .FromInstance(_screen)
                .AsSingle();
        }
        #endregion
    }
}
