using Zenject;
using UnityEngine;
namespace Daxi.VisualLayer.Player.PowerUps.Installers
{
    public class ShieldComponentInstaller:MonoInstaller<ShieldComponentInstaller>
    {
        #region Fields
        [SerializeField]
        private GameObject _shieldObj;
        #endregion


        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<ShieldComponent>()
                .AsSingle();

            Container
                .Bind<GameObject>()
                .WithId("Shield")
                .FromInstance(_shieldObj)
                .AsTransient();
              
        }
        #endregion
    }
}
