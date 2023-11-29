using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Pause
{
    public class PauseLevelPopup:PopupBase
    {
        #region Factory
        public class PauseLevelPopupFactory:PlaceholderFactory<PauseLevelPopup>
        {
            
        }
        #endregion

        #region Fields
        [SerializeField]
        private Button _restartButton;      

        [SerializeField]
        private Button _levelsButton;

        [SerializeField]
        private Button _XButton;

        private bool _restartButtonPressed;

        private bool _levelButtonPressed;

        #endregion

        #region Properties
        public bool restartButtonPressed => _restartButtonPressed;

        public bool LevelButtonPressed => _levelButtonPressed;
        #endregion

        #region Methods
        protected override void Start()
        {
            base.Start();
            _restartButton.onClick.AddListener(OnRestartClick);
            _XButton.onClick.AddListener(OnCloseClick);
            _levelsButton.onClick.AddListener(OnLevelsButtonClick);
        }

        public void OnLevelsButtonClick()
        {
            _levelsButton.interactable = false;
            _levelButtonPressed = true;
        }

        public void OnRestartClick()
        {
            _restartButton.interactable = false;
            _restartButtonPressed = true;
        }

        #endregion
    }
}
