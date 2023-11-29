using Daxi.DataLayer.MissionsData;
using Daxi.VisualLayer.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
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
        private TextMeshProUGUI _descriptionText;

        [SerializeField]
        private TextMeshProUGUI _itemsCountUi;

        [SerializeField]
        private RectTransform _parent;       
      
        [SerializeField]
        private ImageIndicator _imageIndicatorPrefab;

        [SerializeField]
        private Vector2 _parentLettersOffset;

        private List<ImageIndicator> _imageIndicators = new();

        private Vector2 _parentLayoutStartPosition;


        #endregion

        #region Methods
        public void SetData(MissionData missionData)
        {
            _parent.localScale *= missionData.PopupsLayoutScaleMultiplier;
            _parentLayoutStartPosition = _parent.position;
            _descriptionText.text = missionData.Description;
            if (_imageIndicators.Count > 0)
            {
                for (int i = 0; i < _imageIndicators.Count; i++)
                {
                    Destroy(_imageIndicators[i].gameObject);
                }
                _imageIndicators.Clear();
            }
            var spritesToDisplay = new List<Sprite>();

            var itemsNames = new List<string>();
            for (int i = 0; i < missionData.Items.Count; i++)
            {
                if (!itemsNames.Contains(missionData.Items[i].MyName))
                {
                    itemsNames.Add(missionData.Items[i].MyName);
                    spritesToDisplay.Add(missionData.Items[i].Sprite);
                }
            }
            for (int i = 0; i < spritesToDisplay.Count; i++)
            {
                var indicator = Instantiate(_imageIndicatorPrefab, _parent);
                indicator.SetImage(spritesToDisplay[i]);
                indicator.transform.SetParent(_parent);
                _imageIndicators.Add(indicator);
            }


            if (missionData.Mode == MissionData.MissionMode.collectables)
            {
                _itemsCountUi.text = $"{missionData.ItemsCount}";
            }
            else
            {
                Destroy(_itemsCountUi.transform.parent.gameObject);
            }
          
            if (missionData.Mode == MissionData.MissionMode.letters)
            {

                _parent.position = _parentLayoutStartPosition + _parentLettersOffset;
            }
            else
            {
                _parent.position = _parentLayoutStartPosition;
            }
        }     
        #endregion
    }
}
