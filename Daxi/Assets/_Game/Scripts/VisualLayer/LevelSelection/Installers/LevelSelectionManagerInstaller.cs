using Daxi.DataLayer.LevelsData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.LevelSelection.Installers
{
    public class LevelSelectionManagerInstaller:MonoInstaller<LevelSelectionManagerInstaller>
    {
        #region Fields      

        [SerializeField]
        private string _currentSceneName;

        [SerializeField]
        private Button _backButton;
             
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<LevelSelectionManager>()
                .AsSingle();

            Container
               .Bind<string>()
               .WithId("LevelSelectionSceneName")
               .FromInstance(_currentSceneName)
               .AsTransient();

            Container
              .Bind<Button>()
              .WithId("Back")
              .FromInstance(_backButton)
              .AsTransient();
        }
        #endregion
    }
}
