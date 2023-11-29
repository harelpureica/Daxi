using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.OutOfhearts
{
    public class OutOfHeartsPopup:PopupBase
    {
        #region Factory
        public class OutOfHeartsPopupFactory:PlaceholderFactory<OutOfHeartsPopup>
        {

        }
        #endregion
        #region Fields
        [SerializeField]
        private Button _homebutton;

        private bool _playerClickedHome;

        public bool playerClickedHome=> _playerClickedHome;
        #endregion

        protected override void Start()
        {
            base.Start();
            _homebutton.onClick.AddListener(OnHomeClick);
        }

        private void OnHomeClick()
        {
            _playerClickedHome = true;
        }
    }
}
