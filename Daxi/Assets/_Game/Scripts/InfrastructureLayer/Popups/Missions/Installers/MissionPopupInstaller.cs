using Daxi.VisualLayer.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Missions.Installers
{
    public class MissionPopupInstaller:MonoInstaller<MissionPopupInstaller>
    {

        [SerializeField]
        private MissionPopup _prefab;

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindFactory<MissionPopup, MissionPopup.MissionPopupFactory>()
                .FromComponentInNewPrefab(_prefab)
                .AsSingle();
        }
        #endregion
    }
}
