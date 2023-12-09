using Daxi.DataLayer.StoreData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Daxi.VisualLayer.Store
{
    public class StoreMenu:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private RectTransform _parent;

        [SerializeField]
        private StoreItemUi _uiPrefab;

        [SerializeField]
        private Scrollbar scrollbar;

        private List<StoreItemUi> itemsUis;
        
        #endregion


        #region Methods
        public void SetData(List<StoreItem>storeItems)
        {
            ClearUi();
            for (int i = 0;storeItems.Count > 0;i++)
            {
                var uiItem= Instantiate(_uiPrefab, _parent);
                uiItem.SetData(storeItems[i]);
            }

        }
        private void ClearUi()
        {
            for (int i = 0; i < itemsUis.Count; i++)
            {
                Destroy(itemsUis[i].gameObject);
            }
            itemsUis.Clear();
        }
        private void Update()
        {
            ScrollItems();
        }

        private void ScrollItems()
        {
            if(itemsUis.Count <=0)
            {
                return;
            }
            var scrollStep = 1f / itemsUis.Count;            

            if(scrollbar.value%scrollStep!=0)
            {
                var wantedValue = scrollStep *(scrollbar.value / scrollStep);
                scrollbar.value = Mathf.Lerp(scrollbar.value, wantedValue,Time.deltaTime);
            }


        }
        #endregion
    }
}
