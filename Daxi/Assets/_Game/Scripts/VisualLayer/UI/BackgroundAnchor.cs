using UnityEngine;

namespace Daxi.VisualLayer.UI
{
    public class BackgroundAnchor:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Vector2 offset;
        #endregion

        #region Methods
        private void Start ()
        {
            var bounds = _spriteRenderer.bounds;

            Vector3 topLeftCornerScreenSpace = new Vector3(0, Screen.height, 0);

            Vector2 topLeftCornerWorldSpace = Camera.main.ScreenToWorldPoint(topLeftCornerScreenSpace);
            var position = topLeftCornerWorldSpace + offset + new Vector2(bounds.extents.x, -bounds.extents.y);
            _spriteRenderer.transform.position = new Vector3( position.x,position.y,_spriteRenderer.transform.position.z);
        }
      
        #endregion
    }
}
