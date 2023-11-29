using Zenject;
using UnityEngine;

namespace Daxi.VisualLayer.PostProcessing.Installers
{
    public class VolumeControllerInstaller:MonoInstaller<VolumeControllerInstaller>        
    {
        #region Fields
        [SerializeField]
        private VolumeController _controller;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<VolumeController>()
                .FromInstance(_controller)
                .AsSingle();
        }
        #endregion
    }
}
