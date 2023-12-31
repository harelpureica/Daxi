﻿using Daxi.DataLayer.Configuration.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.Installers
{
    public class PlayerManagerInstaller:MonoInstaller<PlayerManagerInstaller>
    {
        #region Fields
        [SerializeField]
        private PlayerManager _playerManager;

        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private PlayerSettings _playerSettings;

        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private List<PlayersClipInfo> _playersClipsInfos;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
           Container
                .Bind<PlayerManager>()
                .FromInstance(_playerManager)
                .AsSingle();

            Container
               .Bind<Canvas>()
               .FromInstance(_canvas)
               .AsSingle();

            Container
               .Bind<List<PlayersClipInfo>>()
               .FromInstance(_playersClipsInfos)
               .AsSingle();

            Container
               .Bind<Rigidbody2D>()
               .FromInstance(_rb)
               .AsSingle();


            Container
                .Bind<PlayerSettings>()
                .FromInstance(_playerSettings)
                .AsSingle();
        }
        #endregion
    }
}
