using Zenject;
using UnityEngine;
using UnityEngine.UI;
using Daxi.DataLayer.Player;
using System.Collections.Generic;
using Daxi.DataLayer.LevelsData;
using TMPro;

namespace Daxi.VisualLayer.MenuScene
{
    public class MenuSceneManagerInstaller:MonoInstaller<MenuSceneManagerInstaller>
    {
        #region Fields
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _shopButton;


        [SerializeField]
        private string _shareMessage;

        [SerializeField]
        public string _playStoreURL;

        [SerializeField]
        private Button _shareButton;

        [SerializeField]
        private Button _SettingsButton;

        [SerializeField]
        private TextMeshProUGUI _logText;

        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
              .Bind<TextMeshProUGUI>()
              .WithId("Log")
              .FromInstance(_logText)
              .AsTransient();

            Container
                .Bind<Button>()
                .WithId("Play")
                .FromInstance(_playButton)
                .AsTransient();
            Container
                .Bind<Button>()
                .WithId("Settings")
                .FromInstance(_SettingsButton)
                .AsTransient();

            Container
               .Bind<Button>()
               .WithId("Shop")
               .FromInstance(_shopButton)
               .AsTransient();

            Container
               .Bind<Button>()
               .WithId("ShareButton")
               .FromInstance(_shareButton)
               .AsTransient();

            Container
                .Bind<string>()
                .WithId("ShareText")
                .FromInstance(_shareMessage)
                .AsTransient();

            Container
               .Bind<string>()
               .WithId("ShareURL")
               .FromInstance(_playStoreURL)
               .AsTransient();


            Container
                .BindInterfacesAndSelfTo<MenuSceneManager>()
                .AsSingle();
        }
        #endregion
    }
}
