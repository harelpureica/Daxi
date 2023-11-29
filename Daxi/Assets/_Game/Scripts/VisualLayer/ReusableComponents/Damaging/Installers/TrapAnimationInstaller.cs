

using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Damaging.Installers
{
    public class TrapAnimationInstaller:MonoInstaller<TrapAnimationInstaller>
    {
        #region Fields
        [SerializeField]
        private Animator _animator;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<Animator>()
                .FromInstance(_animator)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<TrapAnimation>()
                .AsSingle();
        }
        #endregion
    }
}
