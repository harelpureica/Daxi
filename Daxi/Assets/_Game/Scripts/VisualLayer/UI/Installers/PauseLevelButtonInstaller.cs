using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.UI.Installers
{
    public class PauseLevelButtonInstaller:MonoInstaller<PauseLevelButtonInstaller>
    {
        #region Fields
        [SerializeField]
        private DaxiButton _button;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PauseLevelButton>()
                .AsSingle();

            Container
                .Bind<DaxiButton>()
                .WithId("PauseLevel")
                .FromInstance(_button)
                .AsTransient();
        }
        #endregion
    }
}
