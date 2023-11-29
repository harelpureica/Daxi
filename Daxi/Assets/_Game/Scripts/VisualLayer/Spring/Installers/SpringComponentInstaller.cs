using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Spring.Installers
{
    public class SpringComponentInstaller:MonoInstaller<SpringComponentInstaller>
    {
        #region Fields
        [SerializeField]
        private SpringComponent _springComponent;

        [SerializeField]
        private Animator _animator;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<SpringComponent>()
                .FromInstance(_springComponent)
                .AsSingle();

             Container
                .Bind<Animator>()
                .FromInstance(_animator)
                .AsSingle();
        }
        #endregion
    }
}
