using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.UI.CountDown.Installers
{
    public class CountDownInstaller:MonoInstaller<CountDownInstaller>
    {
        #region Fields
        [SerializeField]
        private Transform _ready;

        [SerializeField]
        private Transform _set;

        [SerializeField]
        private Transform _go;

        [SerializeField]
        private Transform _three;

        [SerializeField]
        private Transform _two;

        [SerializeField]
        private Transform _one;

        [SerializeField]
        private GameObject _panel;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _endClip;

        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<Transform>()
                .WithId("Ready")
                .FromInstance(_ready)
                .AsTransient();

            Container
               .Bind<AudioSource>()
               .WithId("CountDown")
               .FromInstance(_audioSource)
               .AsTransient();

            Container
               .Bind<AudioClip>()
               .WithId("CountDown")
               .FromInstance(_endClip)
               .AsTransient();

            Container
              .Bind<GameObject>()
              .WithId("CountDownPanel")
              .FromInstance(_panel)
              .AsTransient();

            Container
               .Bind<Transform>()
               .WithId("Set")
               .FromInstance(_set)
               .AsTransient();

            Container
               .Bind<Transform>()
               .WithId("Go")
               .FromInstance(_go)
               .AsTransient();

            Container
               .Bind<Transform>()
               .WithId("3")
               .FromInstance(_three)
               .AsTransient();

            Container
               .Bind<Transform>()
               .WithId("2")
               .FromInstance(_two)
               .AsTransient();

            Container
               .Bind<Transform>()
               .WithId("1")
               .FromInstance(_one)
               .AsTransient();

            Container
               .Bind<CountDownComponent>()
               .AsSingle();
        }
        #endregion
    }
}
