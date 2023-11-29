using Daxi.InfrastructureLayer.Signals;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.DownHillZone
{
    public class DownHillZoneComponent:MonoBehaviour
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
            _bus.Fire<OnDownHillZoneChange>(new OnDownHillZoneChange { InZone = true });

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            _bus.Fire<OnDownHillZoneChange>(new OnDownHillZoneChange { InZone = false });

        }
        #endregion
    }
}
