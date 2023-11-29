using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.MonoUtils.Installers
{
    public class MonoServiceInstaller:MonoInstaller<MonoServiceInstaller>
    {
        #region Fields
        [SerializeField]
        private MonoService _monoService;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<MonoService>()
                .FromComponentInNewPrefab(_monoService)
                .AsSingle();
        }
        #endregion
    }
}
