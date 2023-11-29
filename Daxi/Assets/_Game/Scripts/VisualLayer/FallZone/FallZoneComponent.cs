using Daxi.InfrastructureLayer.Signals;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.FallZone
{
    public class FallZoneComponent:MonoBehaviour
    {
        #region Injects
        [Inject]
        private SignalBus _bus;
        #endregion

        #region Methods
       
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            _bus.Fire<OnFallZoneChange>(new OnFallZoneChange { InFallZone = true });

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            _bus.Fire<OnFallZoneChange>(new OnFallZoneChange { InFallZone = false });

        }      
        #endregion
    }
}
