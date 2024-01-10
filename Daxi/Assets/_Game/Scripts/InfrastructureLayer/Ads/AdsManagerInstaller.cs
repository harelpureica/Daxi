using Zenject;
using UnityEngine;

namespace Daxi.InfrastructureLayer.Ads
{
    public class AdsManagerInstaller:MonoInstaller<AdsManagerInstaller>
    {
        [SerializeField]
        private bool _inProduction;
        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<AdsManager>()
                .AsSingle();

            Container
                .Bind<bool>()
                .WithId("Production")
                .FromInstance(_inProduction)
                .AsSingle();
               
        }
        #endregion
    }
}
