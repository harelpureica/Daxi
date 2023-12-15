using Daxi.DataLayer.Player;
using Daxi.DataLayer.StoreData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.Store
{
    public class StoreMenu:MonoBehaviour
    {

        #region Injects
        [Inject]
        private StoreItemUi.StoreItemUiFactory _storeItemUiFactory;
        #endregion

        #region Fields      

        [SerializeField]
        private float _centeringSpeed;

        [SerializeField]
        private ScrollRect _scrollRect;

        private List<StoreItemUi> itemsUis=new();

        [SerializeField]
        private RectTransform _rectTransformContent;

        [SerializeField]
        private float _itemSize;

        private StoreItemUi currentItem;

        [Inject(Id = "MainBtn")]
        private Button _mainBtn;

        [Inject(Id = "YellowBtn")]
        private Sprite _mainBtnYellow;

        [Inject(Id = "PinkBtn")]
        private Sprite _mainBtnPink;

        [Inject(Id = "MainBtnText")]
        private TextMeshProUGUI _mainBtnText;

        [Inject]
        private PlayerData _playerData;

        [Inject]
        private StoreController _controller;


        private bool _initializing;
        #endregion

        #region Properties
        public StoreItemUi CurrentItem => currentItem;

        #endregion

        #region Methods
        public void SetData(List<StoreItem>storeItems)
        {
            _initializing = true;
            if (itemsUis.Count>0)
            {
                ClearUi();
            }
            for (int i = 0; i<storeItems.Count;i++)
            {
                var uiItem= _storeItemUiFactory.Create();
                uiItem.SetData(storeItems[i]);                
                itemsUis.Add(uiItem);
                currentItem = uiItem;
            }
            _scrollRect.horizontalNormalizedPosition = 0f;
            _initializing = false;


        }
        private void ClearUi()
        {
            for (int i = 0; i < itemsUis.Count; i++)
            {
                Destroy(itemsUis[i].gameObject);
            }
            itemsUis.Clear();            
        }
        private void LateUpdate()
        {
            ScrollItems();
        }

        private void ScrollItems()
        {
            if (itemsUis.Count <= 0||_initializing)
            {
                return;
            }
            float step = 1f / (_rectTransformContent.sizeDelta.x / _itemSize);
            float currentPosition = _scrollRect.horizontalNormalizedPosition;
            float nearestValidPosition = Mathf.Round(currentPosition / step) * step;
            var currentIndex = Mathf.RoundToInt(nearestValidPosition / step);
            currentItem= itemsUis[currentIndex];
            for (int i = 0; i < itemsUis.Count; i++)
            {
                if (i == currentIndex)
                {
                    itemsUis[i].Grow();
                    if(_controller.State == StoreController.StoreState.pets)
                    {
                        char[] chars = _playerData.UnlockedPets.ToCharArray();
                        ChangeBtnBasedOnUnlockedItem(itemsUis[i].MyStoreItem, chars);
                    }
                    else if (_controller.State == StoreController.StoreState.skins)
                    {
                        char[] chars = _playerData.UnlockedCharacters.ToCharArray();
                        ChangeBtnBasedOnUnlockedItem(itemsUis[i].MyStoreItem, chars);

                    }
                    else
                    {

                        _mainBtn.image.sprite = _mainBtnYellow;
                        _mainBtnText.text = itemsUis[i].MyStoreItem.Cost.ToString("0.00") + "$";

                    }

                }
                else

                {
                    itemsUis[i].Shrink();
                }
            }
            if (UnityEngine.SystemInfo.deviceType==DeviceType.Handheld)
            {
                if (Input.touchCount>0)
                {

                    return;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {

                    return;
                }
            }
           
           
           
            if (!Mathf.Approximately(currentPosition, nearestValidPosition))
            {
                _scrollRect.horizontalNormalizedPosition = Mathf.Lerp(currentPosition, nearestValidPosition, Time.deltaTime*_centeringSpeed);
            }
           

        }
        private void ChangeBtnBasedOnUnlockedItem(StoreItem storeItem, char[]unlockedItems)
        {
            var unlocked = false;
            for (int i = 0; i < unlockedItems.Length; i++)
            {
                if (unlockedItems[i] == storeItem.MyId[0])
                {
                    unlocked = true;
                    break;
                }
            }
            if (unlocked)
            {
                _mainBtn.image.sprite = _mainBtnPink;
                _mainBtnText.text = "";

            }
            else
            {
                _mainBtn.image.sprite = _mainBtnYellow;
                _mainBtnText.text =storeItem.Cost.ToString("0.00") + "$";


            }
        }
      
        #endregion
    }
}
