
using Daxi.DataLayer.LevelsData;
using Daxi.DataLayer.MissionsData;
using Daxi.DataLayer.Player;
using UnityEngine;
using Zenject;


namespace Daxi.VisualLayer.Levels.Installers
{
    public class LevelManagerInstaller:MonoInstaller<LevelManagerInstaller>
    {
        #region Fields
        [SerializeField]
        private MissionData _missionData;

        [SerializeField]
        private LevelData _levelData;

        [SerializeField]
        private LevelManager _levelManager;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _winClip;

        [SerializeField]
        private AudioClip _loseClip;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
               .Bind<AudioSource>()
               .WithId("LevelManager")
               .FromInstance(_audioSource)
               .AsTransient();

            Container
              .Bind<AudioClip>()
              .WithId("Win")
              .FromInstance(_winClip)
              .AsTransient();

            Container
             .Bind<AudioClip>()
             .WithId("Lose")
             .FromInstance(_loseClip)
             .AsTransient();

            Container
                .Bind<LevelManager>()
                .FromInstance(_levelManager)
                .AsSingle();

            Container
                .Bind<MissionData>()
                .FromInstance(_missionData)
                .AsSingle();

            Container
               .Bind<LevelData>()
               .FromInstance(_levelData)
               .AsSingle();
        }
        #endregion
    }
}
