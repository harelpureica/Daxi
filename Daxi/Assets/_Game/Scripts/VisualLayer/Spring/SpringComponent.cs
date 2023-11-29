

using Cysharp.Threading.Tasks;
using Daxi.InfrastructureLayer.Signals;
using Daxi.VisualLayer.ReusableComponents.Interactions;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Spring
{
    public class SpringComponent : MonoBehaviour, IInteractable
    {
        #region Fields
        [SerializeField]
        private float _springForce;

        private bool _active;
       
        #endregion

        #region Injects
        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private Animator _animator;
        #endregion
        #region Methods
        public async void Interact()
        {
            if(_active)
            {
                return;
            }
            _active = true;
            _animator.SetTrigger("OnIt");
            _signalBus.Fire<OnSpring>(new OnSpring { SpringForce= _springForce });
            await UniTask.Delay(150);
            _active = false;
        }
        #endregion
    }
}
