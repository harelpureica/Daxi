using Daxi.InfrastructureLayer.Signals;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.ReusableComponents.Interactions;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.PowerUps
{
    public class PowerUpCollectable : MonoBehaviour, IInteractable
    {
        #region Injects
        [Inject]
        private SignalBus _bus;
      
        #endregion

        #region Methods
        public void Interact()
        {
            _bus.Fire<OnPowerUpCollected>(new OnPowerUpCollected());
            Destroy(gameObject);
        }
        #endregion

    }
}
