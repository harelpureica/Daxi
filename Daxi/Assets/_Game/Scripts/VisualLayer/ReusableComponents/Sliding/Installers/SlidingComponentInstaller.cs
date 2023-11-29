using Zenject;
using UnityEngine;
using Daxi.DataLayer.Configuration.Sliding;

namespace Daxi.VisualLayer.ReusableComponents.Sliding.Installers
{
    public class SlidingComponentInstaller:MonoInstaller<SlidingComponentInstaller>
    {
        #region Fields
        [SerializeField]
        private SlidingSettings _slidingSettings;

        [SerializeField]
        private CapsuleCollider2D _collider;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
               .Bind<SlidingSettings>()
               .FromInstance(_slidingSettings)
               .AsSingle();

            Container
                .Bind<SlidingComponent>()
                .AsSingle();

            Container
              .Bind<CapsuleCollider2D>()
              .FromInstance(_collider)
              .AsSingle();

        }
        #endregion
    }
}
