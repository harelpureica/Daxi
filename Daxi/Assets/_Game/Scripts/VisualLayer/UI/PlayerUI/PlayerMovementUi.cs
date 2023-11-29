
using Daxi.VisualLayer.Player;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.UI.PlayerUI
{
    public class PlayerMovementUi :MonoBehaviour
    {
        #region Injects
        [Inject]
        private PlayerManager _playerManager;
        #endregion

        #region Fields
        [SerializeField]
        private DaxiButton _jumpButton;

        [SerializeField]
        private DaxiButton _slideButton;

        #endregion

        #region Events
        public event Action OnUpClickDown;

        public event Action OnUpClickUp;

        public event Action OnDownClickUp;

        public event Action OnDownClickDown;
        #endregion

        #region Methods
        private void Start()
        {
            _jumpButton.OnClickDown += OnUpClick;
            _jumpButton.OnClickUp += OnJumpClickUp;
            _slideButton.OnClickDown -= OnSlideClickDown;
            _slideButton.OnClickDown += OnSlideClickDown;
            _slideButton.OnClickUp -= OnSlideClickUp;
            _slideButton.OnClickUp += OnSlideClickUp;
        }

        public void OnJumpClickUp()
        {
           OnUpClickUp?.Invoke();
        }

        public void OnUpClick()
        {
            OnUpClickDown?.Invoke();
        }

        public void OnSlideClickUp()
        {
            OnDownClickUp?.Invoke();
        }

        public void OnSlideClickDown()
        {
            OnDownClickDown?.Invoke();
        }


        #endregion
    }
}
