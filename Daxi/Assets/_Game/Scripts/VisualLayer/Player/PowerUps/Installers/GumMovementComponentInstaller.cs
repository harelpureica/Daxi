

using Zenject;

namespace Daxi.VisualLayer.Player.PowerUps.Installers
{
    public class GumMovementComponentInstaller:MonoInstaller<GumMovementComponentInstaller>
    {
        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GumMovementComponent>()
                .AsSingle();
        }
        #endregion
    }
}
