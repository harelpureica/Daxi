using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.PowerUps.Installers
{
    public class PlankFactoryInstaller:MonoInstaller<PlankFactoryInstaller>
    {
        #region Fields
        [SerializeField]
        private Plank _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<Plank, Plank.Factory>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle();
        }
        #endregion
    }
}
