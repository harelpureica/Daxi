using Daxi.DataLayer.GameItems;
using Daxi.InfrastructureLayer.Signals;
using Daxi.VisualLayer.Player;
using Daxi.VisualLayer.ReusableComponents.Interactions;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Collectables.Candies
{
    public class CollectableCandy : MonoBehaviour, IInteractable
    {
        [Inject]
        private SignalBus _signalBus;

        [SerializeField]
        private GameItemData _itemData;
       
        public void Interact()
        {
            _signalBus.Fire<OnCollectableCollected>(new OnCollectableCollected() { Data = _itemData });
            Destroy(gameObject);
        }
    }
}
