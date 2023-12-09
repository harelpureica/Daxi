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

        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _audioClip;

        #endregion

        #region Methods
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void Interact()
        {
           
            _audioSource.PlayOneShot(_audioClip);
            _bus.Fire<OnPowerUpCollected>(new OnPowerUpCollected());
            Destroy(gameObject);
        }
        #endregion

    }
}
