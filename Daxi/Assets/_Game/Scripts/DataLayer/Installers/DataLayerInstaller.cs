using Daxi.DataLayer.LevelsData;
using Daxi.DataLayer.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.DataLayer.Installers
{
    public class DataLayerInstaller:MonoInstaller<DataLayerInstaller>
    {

        #region Fields
        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private List<LevelData> _levelDatas;


        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerData>()
                .FromInstance(_playerData)
                .AsSingle();

            Container
               .Bind<List<LevelData>>()
               .FromInstance(_levelDatas)
               .AsSingle();
              
        }
        #endregion
    }
}
