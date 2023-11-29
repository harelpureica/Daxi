using Daxi.InfrastructureLayer.Signals;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.ReusableComponents.Interactions;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.SpeedUp
{
    public class SpeedUpComponent:MonoBehaviour,IInteractable
    {
        #region Fields
        [SerializeField]
        private float _speedMultiplier;



        #endregion

        [Inject]
        private SignalBus _signalBus;


        #region Methods      
        public void Interact()
        {
            _signalBus.Fire<OnSpeedUp>(new OnSpeedUp() { Speed=_speedMultiplier});
            Destroy(gameObject);
        }
        #endregion
    }
}
