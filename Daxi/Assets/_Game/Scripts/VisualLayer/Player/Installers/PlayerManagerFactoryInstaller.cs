using Daxi.DataLayer.Player;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.Installers
{
    public class PlayerManagerFactoryInstaller:MonoInstaller<PlayerManagerFactoryInstaller>
    {
        #region Fields
        [SerializeField]
        private PlayerManager _prefab;
        #endregion
        public override void InstallBindings()
        {
            Container
                .BindFactory<PlayerManager, PlayerManager.PlayerManagerFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle();


        }
    }
}
