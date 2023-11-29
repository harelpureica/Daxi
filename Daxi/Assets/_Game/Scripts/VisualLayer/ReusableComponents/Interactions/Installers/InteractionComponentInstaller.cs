using Zenject;
using UnityEngine;

namespace Daxi.VisualLayer.ReusableComponents.Interactions.Installers
{
    public class InteractionComponentInstaller:MonoInstaller<InteractionComponentInstaller>
    {
        #region Fields
        [SerializeField]
        private InteractionComponent _interactionComponent;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<InteractionComponent>()
                .FromInstance(_interactionComponent)
                .AsSingle();
        }
        #endregion
    }
}
