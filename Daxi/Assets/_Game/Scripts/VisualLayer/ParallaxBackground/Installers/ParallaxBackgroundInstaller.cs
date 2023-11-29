

using Daxi.VisualLayer.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ParallaxBackground.Installers
{
    public class ParallaxBackgroundInstaller:MonoInstaller<ParallaxBackgroundInstaller>
    {
        #region Fields
        [SerializeField]
        private List<ParallaxLayer> _layers;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ParallaxBackgroundController>()
                .AsSingle();

            Container
                .Bind<List<ParallaxLayer>>()
                .FromInstance(_layers)
                .AsTransient();
           
        }
        #endregion

    }
}
