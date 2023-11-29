using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.ScenesManagment
{
    [CreateAssetMenu(fileName = "SceneLoaderInstaller",menuName = "Installers/ScenesManagment/SceneLoaderInstaller")]

    public class SceneLoaderInstaller : ScriptableObjectInstaller<SceneLoaderInstaller>
    {

        #region Fields
        [SerializeField]
        private List<SceneSet> _sceneSets;
        #endregion
        public override void InstallBindings()
        {
            Container
                .Bind<IScenesLoader>()
                .To<ScenesLoader>()
                .AsSingle();

            Container
               .Bind<List<SceneSet>>()
               .FromInstance(_sceneSets)
               .AsSingle();
        }
    }
}
