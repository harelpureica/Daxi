

using Cysharp.Threading.Tasks;
using Daxi.DataLayer.StoreData;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.Store
{
    public class StoreItemUi:MonoBehaviour
    {

        #region Factory
        public class StoreItemUiFactory:PlaceholderFactory<StoreItemUi>
        {

        }
        #endregion

        #region Fields
        [Inject]
        private StoreController _storeController;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Transform _transformToSize;

        private StoreItem _storeItem;

        private bool big;


        #endregion

        #region Properties
        public StoreItem MyStoreItem => _storeItem;
        #endregion

        #region Methods
        private void Start()
        {
            _transformToSize.localScale= new Vector3(0.8f, 0.8f, 0.8f);

        }

    
        public async void Grow()
        {
            if(big)
            {
                return;
            }
            
            big = true;
            
            var wantedSize = Vector3.one;
            var lerp = 0f;
            while(lerp < 1f)
            {
                _transformToSize.localScale =Vector3.Lerp(new Vector3(0.8f, 0.8f, 0.8f), wantedSize,lerp) ;
                lerp += Time.deltaTime*3;
                await UniTask.Yield();

            }
            _transformToSize.localScale = wantedSize;
        }
        public async void Shrink()
        {
            if (!big)
            {
                return;
            }
            else
            {
                big = false;
            }
            var wantedSize =new Vector3(0.8f, 0.8f, 0.8f);
            var lerp = 0f;
            while (lerp < 1f)
            {
                _transformToSize.localScale = Vector3.Lerp(Vector3.one, wantedSize, lerp);
                lerp += Time.deltaTime*3;
                await UniTask.Yield();
            }
            _transformToSize.localScale = wantedSize;
        }
        public void SetData(StoreItem storeItem)
        {
            _storeItem = storeItem;
            _button.image.sprite = _storeItem.Sprite;           
        }
        #endregion
    }
}
