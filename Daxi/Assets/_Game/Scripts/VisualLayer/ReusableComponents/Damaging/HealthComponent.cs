using System;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Damaging
{
    public class HealthComponent:IHealthComponent
    {

        #region Events
        public event Action OnDead;

        public event Action<int> OnHealthChange;

        public event Action OnExtraHealthUsed;
        #endregion

        #region Fields           
        private int _health;

        private int _extrahealth;

        private bool _initialized;
        #endregion

        #region Injects   
        #endregion

        #region Properties
        public int Health => _health;

        public bool Initialized => _initialized;
        #endregion

        #region Methods       
        public void TakeDamage(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (_health > 3)
                {
                    OnExtraHealthUsed?.Invoke();
                    _health--;
                }
                else if(_health > 0)
                {
                    _health --;                   
                }
            }
            if (_health <=0)
            {
                OnDead?.Invoke();
            }
            OnHealthChange?.Invoke(_health);
        }
        public void AddHealth(int amount)
        {
            _health+= amount;
            OnHealthChange?.Invoke(_health);
        }

        public void Initialize(int startHealth)
        {
            _health=startHealth;
            OnHealthChange?.Invoke(_health);
            _initialized = true;
        }

        public void ImmedietKill()
        {
            _health = 0;
            OnHealthChange?.Invoke(_health);
        }
        #endregion
    }
}
