

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
        #region Fields
        [Inject]
        private StoreController _storeController;

        [SerializeField]
        private Button _button;        

        private StoreItem _storeItem;

        #endregion

        #region Methods
        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        public void OnButtonClick()
        {
            _storeController.SelectItem(_storeItem);
        }

        public void SetData(StoreItem storeItem)
        {
            _storeItem = storeItem;
            _button.image.sprite = _storeItem.Sprite;           
        }
        #endregion
    }
}
