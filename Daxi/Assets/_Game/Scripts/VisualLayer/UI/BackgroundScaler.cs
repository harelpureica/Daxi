using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Daxi.VisualLayer.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundScaler:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Vector2 offset;


        #endregion
        #region Methods
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            float screenHeight = Camera.main.orthographicSize * 2f;
            float screenWidth = screenHeight * Screen.width/ Screen.height;

            Vector3 newScale = transform.localScale;

            float scaleX = screenWidth / _spriteRenderer.bounds.size.x;
            float scaleY = screenHeight / _spriteRenderer.bounds.size.y;
            float maxScale = Mathf.Max(scaleX, scaleY);


            newScale.x = maxScale;
            newScale.y = maxScale;
            transform.localScale = newScale;

            var leftover = ((screenHeight /  _spriteRenderer.bounds.size.y )-1)* newScale.y;
            newScale.x += leftover;
            newScale.y += leftover;            
            transform.localScale = newScale;
            Anchor();
        }
      

       
        private void Anchor()
        {
            var bounds = _spriteRenderer.bounds;

            Vector3 topLeftCornerScreenSpace = new Vector3(0, Screen.height, 0);

            Vector2 topLeftCornerWorldSpace = Camera.main.ScreenToWorldPoint(topLeftCornerScreenSpace);
            var position = topLeftCornerWorldSpace + offset + new Vector2(bounds.extents.x, -bounds.extents.y);
            _spriteRenderer.transform.position = new Vector3(position.x, position.y, _spriteRenderer.transform.position.z);
        }

        #endregion

    }
}
