using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Interactions
{
    public class InteractionComponent:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private float _interactionDistance;
        #endregion

        #region Methods      
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer!=LayerMask.NameToLayer("Interaction"))
            {
                return;
            }
            if (collision.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Interaction"))
            {
                return;
            }
            if (collision.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
        #endregion
    }
}
