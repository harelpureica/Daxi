

using Cysharp.Threading.Tasks;
using Daxi.InfrastructureLayer.Signals;
using Daxi.VisualLayer.Player.PowerUps;
using Daxi.VisualLayer.ReusableComponents.Interactions;
using System;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Damaging
{
    public class DamagingComponent : MonoBehaviour, IInteractable,IDamaging
    {
        #region Fields      
        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _damageInterval;

        [SerializeField]
        private bool immedietKill;

        private bool _active = true;
        #endregion

        #region Injects
        [Inject]
        private SignalBus _signalBus;
        #endregion

        #region Events
        public event Action Damaging;
        #endregion

        #region Methods
        public async void Interact()
        {
            if(!_active)
            {
                return;
            }
            _active = false;
            if(immedietKill)
            {
                _signalBus.Fire<OnImmedietKill>(new OnImmedietKill());
            }
            else
            {
                _signalBus.Fire<OnDamagingPlayer>(new OnDamagingPlayer { Amount = _damage });
            }
            Damaging?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(_damageInterval));

            _active = true;
        }       
        #endregion

    }
}
