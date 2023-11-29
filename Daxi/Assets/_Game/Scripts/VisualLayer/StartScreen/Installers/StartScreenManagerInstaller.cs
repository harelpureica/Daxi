using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.StartScreen.Installers
{
    public class StartScreenManagerInstaller:MonoInstaller<StartScreenManagerInstaller>
    {
        #region Fields
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TextMeshProUGUI _text;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<StartScreenManager>()
                .AsSingle();

            Container
                .Bind<Slider>()
                .WithId("Start")
                .FromInstance(_slider)
                .AsTransient();

            Container
               .Bind<TextMeshProUGUI>()
               .WithId("Start")
               .FromInstance(_text)
               .AsTransient();
        }
        #endregion
    }
}
