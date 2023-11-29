

using System;

namespace Daxi.VisualLayer.ReusableComponents.Damaging
{
    public interface IHealthComponent
    {
        void TakeDamage(int amount);

        void AddHealth(int amount);

        void ImmedietKill();

        void Initialize(int startHealth);

        event Action OnDead;

        event Action<int> OnHealthChange;

        public int Health { get; }

        public bool Initialized { get; }
    }
}
