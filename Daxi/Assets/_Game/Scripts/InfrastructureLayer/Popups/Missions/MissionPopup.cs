using Daxi.DataLayer.MissionsData;
using Daxi.VisualLayer.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Missions
{
    public class MissionPopup:PopupBase
    {
        #region Factory
        public class MissionPopupFactory:PlaceholderFactory<MissionPopup>
        {

        }
        #endregion

        #region Fields

     
        [SerializeField]
        private RectTransform _parent;

        [SerializeField]
        private Image _image;
            

        private MissionData _missionData;

        #endregion

        #region Methods
      
       
        public void SetData(MissionData missionData)
        {
            _missionData = missionData;
            _image.sprite = missionData.PopupSprite;                           
        }     
        #endregion
    }
}
