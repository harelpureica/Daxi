using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Daxi.InfrastructureLayer.Audio.Installers
{
   
    public class AudioSettingsInstaller:MonoInstaller<AudioSettingsInstaller>
    {
        [SerializeField]
        private AudioMixer _music;

        [SerializeField]
        private AudioMixer _sfx;

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<AudioMixer>()
                .WithId("Music")
                .FromInstance(_music)
                .AsTransient();

            Container
               .Bind<AudioMixer>()
               .WithId("Sfx")
               .FromInstance(_sfx)
               .AsTransient();

            Container
              .Bind<AudioSettingsController>()
              .AsSingle();
        }
        #endregion
    }
}
