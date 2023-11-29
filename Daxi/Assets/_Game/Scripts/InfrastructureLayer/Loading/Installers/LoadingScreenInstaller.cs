
using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Loading.Installers
{
    [CreateAssetMenu(fileName = "LoadingScreenLocatorInstaller", menuName = "Installers/Loading/LoadingScreenLocatorInstaller")]
    public class LoadingScreenInstaller:ScriptableObjectInstaller<LoadingScreenInstaller>
    {
        #region Fields
        [SerializeField]
        private LoadingScreen _loadingScreen;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<ILoadingScreen>()
                .FromComponentInNewPrefab(_loadingScreen)
                .AsSingle();
        }
        #endregion
    }
}
