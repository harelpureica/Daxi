using Daxi.DataLayer.MissionsData;
using Daxi.InfrastructureLayer.Popups.Missions;
using Daxi.VisualLayer.Missions;
using Daxi.VisualLayer.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.EndLevel
{
    public class EndLevelPopup:PopupBase
    {
        #region Factory
        public class EndLevelPopupFactory : PlaceholderFactory<EndLevelPopup>
        {

        }
        #endregion
        #region Fields
      

        [SerializeField]
        private RectTransform _parent;

        [SerializeField]
        private ImageIndicator _imageIndicatorPrefab;

        [SerializeField]
        private Image _buttonImage;

        [SerializeField]
        private Sprite _levelPassedButtonSprite;

        [SerializeField]
        private Sprite _levelLosedButtonSprite;

        [SerializeField]
        private Button _homeButton;

        [SerializeField]
        private Button _redoButton;

        [SerializeField]
        private GameObject _pageWin;

        [SerializeField]
        private GameObject _pageLose;

        [SerializeField]
        private TextMeshProUGUI _itemsCountUi;

        [SerializeField]
        private Vector2 _closeButtonSizesWin;

        [SerializeField]
        private Vector2 _closeButtonSizesLose;

        [SerializeField]
        private Vector2 _parentLayoutLettersOffset;


        private List<ImageIndicator> _imageIndicators = new();

        private bool _playerClickedHomeButton;

        private bool _playerClickedRedoButton;

        private Vector2 _parentLayoutStartPosition;

        #endregion

        #region Properties
        public bool PlayerClickedHomeButton =>_playerClickedHomeButton;

        public bool PlayerClickedRedoButton =>_playerClickedRedoButton;
        #endregion

        #region Methods
        public void SetData(MissionData missionData,bool levelPassed,int requiermentPassed)
        {            
            var rect = closeBtn.GetComponent<RectTransform>();
            _parentLayoutStartPosition = _parent.position;
            if (levelPassed)
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _closeButtonSizesWin.x);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _closeButtonSizesWin.y);
                _redoButton.gameObject.SetActive(true);
                _redoButton.onClick.RemoveListener(OnRedoClick);
                _redoButton.onClick.AddListener(OnRedoClick);              
                _pageWin.gameObject.SetActive(true);
                _pageLose.gameObject.SetActive(false);
                _buttonImage.sprite = _levelPassedButtonSprite;
            }
            else
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _closeButtonSizesLose.x);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _closeButtonSizesLose.y);
                _redoButton.gameObject.SetActive(false);
                _pageWin.gameObject.SetActive(false);
                _pageLose.gameObject.SetActive(true);
                _buttonImage.sprite = _levelLosedButtonSprite;

            }
            _homeButton.onClick.RemoveListener(OnHomeClick);
            _homeButton.onClick.AddListener(OnHomeClick);
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
                _itemsCountUi.text = $"{requiermentPassed}/{missionData.ItemsCount}";
            }else
            {
                Destroy(_itemsCountUi.transform.parent.gameObject);

            }

            if (missionData.Mode == MissionData.MissionMode.letters)
            {
                _parent.position= _parentLayoutStartPosition+ _parentLayoutLettersOffset;
            }else
            {
                _parent.position = _parentLayoutStartPosition;

            }
            
        }

        public void OnHomeClick()
        {
            _playerClickedHomeButton = true;

        }

        public void OnRedoClick()
        {
            _playerClickedRedoButton = true;
        }
        #endregion
    }
}
