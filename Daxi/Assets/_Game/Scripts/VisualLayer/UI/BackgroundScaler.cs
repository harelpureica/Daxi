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
        [SerializeField]
        private float parallaxCompensation;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
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

            var leftover = screenHeight -  _spriteRenderer.bounds.size.y;
            newScale.x += leftover;
            newScale.y += leftover;
            

            newScale += new Vector3(parallaxCompensation, parallaxCompensation, 0);
            transform.localScale = newScale;
        }
      
    }
}
