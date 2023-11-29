using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Daxi.VisualLayer.Player.Installers
{
    public class PlayerAnimationComponentInstaller:MonoInstaller<PlayerAnimationComponentInstaller>
    {
        #region Fields       
        [SerializeField]
        private Animator _playerAnimator;

        [SerializeField]
        private Animator _shieldAnimator;     

        [SerializeField]
        private ParticleSystem _slideSmokeParticleSytem;

        [SerializeField]
        private PlayerAnimationComponent _component;
        #endregion
        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerAnimationComponent>()  
                .FromInstance(_component)              
                .AsSingle();

            Container
                .Bind<Animator>()
                .WithId("Player")
                .FromInstance(_playerAnimator)                
                .AsTransient();

            Container
              .Bind<Animator>()
              .WithId("Shield")
              .FromInstance(_shieldAnimator)
              .AsTransient();
           

            Container
             .Bind<ParticleSystem>()
             .FromInstance(_slideSmokeParticleSytem)
             .AsTransient();

        }
        #endregion
    }
}
