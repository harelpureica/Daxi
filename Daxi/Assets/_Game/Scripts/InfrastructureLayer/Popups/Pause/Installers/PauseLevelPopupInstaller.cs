using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Pause.Installers
{
    [CreateAssetMenu(fileName = "PauseLevelPopupInstaller", menuName = "Installers/Popups/PauseLevelPopupInstaller")]

    public class PauseLevelPopupInstaller : ScriptableObjectInstaller<PauseLevelPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private PauseLevelPopup _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
               .BindFactory<PauseLevelPopup, PauseLevelPopup.PauseLevelPopupFactory>()
               .FromComponentInNewPrefab(_prefab)
               .AsSingle();

        }
        #endregion
    }
}
