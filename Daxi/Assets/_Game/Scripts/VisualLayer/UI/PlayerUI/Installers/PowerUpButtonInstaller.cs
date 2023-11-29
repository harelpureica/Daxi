using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.UI.PlayerUI.Installers
{
    public class PowerUpButtonInstaller:MonoInstaller<PowerUpButtonInstaller>
    {
        #region Fields
        [SerializeField]
        private PowerUpButton _prefab;

        [SerializeField]
        private Transform _parent;

        #endregion

        #region Methods
        public override void InstallBindings()
        {           

            Container
              .BindFactory<PowerUpButton,PowerUpButton.PowerUpButtonFactory>()
              .FromComponentInNewPrefab(_prefab)
              .UnderTransform(_parent)
              .AsSingle();
        }
        #endregion
    }
}
