

using Daxi.DataLayer.MissionsData;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Missions.Installers
{
    public class MissionsTrackerInstaller:MonoInstaller<MissionsTrackerInstaller>
    {
       
        #region Methods
        public override void InstallBindings()
        {
            

            Container
                .BindInterfacesAndSelfTo<MissionsTracker>()
                .AsSingle();
        }
        #endregion

    }
}
