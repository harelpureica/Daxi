

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
        private Vector3 _growOffset;

       


        private Vector3 _startPosition;
        private StoreItem _storeItem;

        private bool big;


        #endregion

        #region Properties
        public StoreItem MyStoreItem => _storeItem;
        #endregion

        #region Methods
        private void Awake()
        {
            _transformToSize.localScale= new Vector3(0.65f, 0.65f, 0.65f);
            _startPosition = _transformToSize.localPosition;

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
                if(_transformToSize==null)
                {
                    return;
                }
                _transformToSize.localPosition=Vector3.Lerp(_startPosition+_storeItem.PivotOffset, _startPosition+_growOffset+_storeItem.PivotOffset,lerp);
                _transformToSize.localScale =Vector3.Lerp(new Vector3(0.65f, 0.65f, 0.65f), wantedSize,lerp) ;
                lerp += Time.deltaTime*3;
                await UniTask.Yield();

            }
            if (_transformToSize == null)
            {
                return;
            }
            _transformToSize.localPosition = _startPosition + _growOffset + _storeItem.PivotOffset;
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
            
            var wantedSize =new Vector3(0.65f, 0.65f, 0.65f);
            var lerp = 0f;
            var growPosition = _startPosition+_growOffset + _storeItem.PivotOffset;
            while (lerp < 1f)
            {
                if (_transformToSize == null)
                {
                    return;
                }
                _transformToSize.localPosition = Vector3.Lerp(growPosition,_startPosition + _storeItem.PivotOffset, lerp);
                _transformToSize.localScale = Vector3.Lerp(Vector3.one, wantedSize, lerp);
                lerp += Time.deltaTime*3;
                await UniTask.Yield();
            }
            _transformToSize.localScale = wantedSize;
            _transformToSize.localPosition = _startPosition + _storeItem.PivotOffset;
        }
        public void SetData(StoreItem storeItem)
        {
            _storeItem = storeItem;
            _button.image.sprite = _storeItem.Sprite;
            _button.image.preserveAspect = true;
           
            _transformToSize.localPosition =_startPosition + _storeItem.PivotOffset;
        }
        #endregion
    }
}
