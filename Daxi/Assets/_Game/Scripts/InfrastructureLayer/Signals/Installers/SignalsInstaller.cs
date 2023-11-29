

using Zenject;

namespace Daxi.InfrastructureLayer.Signals.Installers
{
    public class SignalsInstaller:MonoInstaller<SignalsInstaller>
    {
        #region Methods
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<OnPowerUpCollected>();

            Container.DeclareSignal<OnCollectableCollected>();

            Container.DeclareSignal<OnDamagingPlayer>();

            Container.DeclareSignal<OnSpring>();

            Container.DeclareSignal<OnSpeedUp>();

            Container.DeclareSignal<OnImmedietKill>();

            Container.DeclareSignal<OnFallZoneChange>();

            Container.DeclareSignal<OnDownHillZoneChange>();

        }
        #endregion
    }
}
