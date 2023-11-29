using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine;

namespace Daxi.VisualLayer.UI
{
     
    public class AtlasSpriteLoader:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private SpriteAtlas _spriteAtlas;

        [SerializeField]
        private string _spriteName;

        [SerializeField]
        private bool _useImage;
        #endregion

        #region Methods
        private void Start()
        {
            if(_useImage)
            {
                GetComponent<Image>().sprite = _spriteAtlas.GetSprite(_spriteName);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _spriteAtlas.GetSprite(_spriteName);
            }
        }
        #endregion
    }
}
