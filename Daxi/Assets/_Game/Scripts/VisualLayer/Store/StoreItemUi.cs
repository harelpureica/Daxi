

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

        [SerializeField]
        private TextMeshProUGUI _nametext;

        [SerializeField]
        private TextMeshProUGUI _pricetext;



        private StoreItem _storeItem;

        private bool big;


        #endregion

        #region Properties
        public StoreItem MyStoreItem => _storeItem;
        #endregion

        #region Methods
        private void Start()
        {
            _transformToSize.localScale= new Vector3(0.65f, 0.65f, 0.65f);

        }

    
        public async void Grow()
        {
            if(big)
            {
                return;
            }
            
            big = true;
            _nametext.gameObject.SetActive(true);
            _pricetext.gameObject.SetActive(true);
            var wantedSize = Vector3.one;
            var lerp = 0f;
            while(lerp < 1f)
            {
                if(_transformToSize==null)
                {
                    return;
                }
                _transformToSize.localScale =Vector3.Lerp(new Vector3(0.65f, 0.65f, 0.65f), wantedSize,lerp) ;
                lerp += Time.deltaTime*3;
                await UniTask.Yield();

            }
            if (_transformToSize == null)
            {
                return;
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
            _nametext.gameObject.SetActive(false);
            _pricetext.gameObject.SetActive(false);

            var wantedSize =new Vector3(0.65f, 0.65f, 0.65f);
            var lerp = 0f;
            while (lerp < 1f)
            {
                if (_transformToSize == null)
                {
                    return;
                }
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
            _button.image.preserveAspect = true;
            _nametext.text = _storeItem.name;
            if(_storeItem.Cost>0)
            {
                _pricetext.text = _storeItem.Cost.ToString("0.00") + "$";

            }else
            {
                _pricetext.text ="free";
            }

            _nametext.gameObject.SetActive(false);
            _pricetext.gameObject.SetActive(false);
        }
        #endregion
    }
}
