using Daxi.DataLayer.GameItems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Daxi.DataLayer.MissionsData
{
    [CreateAssetMenu(fileName = "MissionData", menuName = "Data/MissionsData/MissionData")]
    public class MissionData : ScriptableObject
    {
        #region Fields
        [SerializeField]
        private string _description;
     
        [SerializeField]
        private List<GameItemData> _items;       

        [SerializeField]
        private Sprite _itemsCountNumber;


        [SerializeField]
        private int _itemsCount;
  

        [SerializeField]
        private MissionMode _mode;

        [SerializeField]
        private Sprite _popupSprite;

        public enum MissionMode { collectables, dammaging, letters }

        #endregion

        #region Properties
        public string Description => _description;
      
        public List<GameItemData> Items  => _items;

        public Sprite ItemsCountNumber => _itemsCountNumber;

        public int ItemsCount =>_itemsCount;

        public MissionMode Mode => _mode;

        public Sprite PopupSprite => _popupSprite;


        #endregion

    }
  
   
   
}
