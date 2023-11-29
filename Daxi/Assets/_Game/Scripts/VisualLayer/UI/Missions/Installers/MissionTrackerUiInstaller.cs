using Zenject;
using UnityEngine;

namespace Daxi.VisualLayer.UI.Missions.Installers
{
    public class MissionTrackerUiInstaller:MonoInstaller<MissionTrackerUiInstaller>
    {
        #region Fields
        [SerializeField]
        private MissionsTrackerUi _missionsTrackerUi;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<MissionsTrackerUi>()
                .FromInstance(_missionsTrackerUi)
                .AsSingle();
        }
        #endregion
    }
}
