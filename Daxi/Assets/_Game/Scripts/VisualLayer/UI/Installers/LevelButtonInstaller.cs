using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.UI.Installers
{
    public class LevelButtonInstaller:MonoInstaller<LevelButtonInstaller>
    {
        #region  Fields        
        [SerializeField]
        private LevelButton _prefab;

        [SerializeField]
        private Transform _transform;
        #endregion
        #region  Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<LevelButton, LevelButton.Factory>()
                .FromComponentInNewPrefab(_prefab)
                .UnderTransform(_transform)
                .AsSingle();

        }
        #endregion
    }
}
