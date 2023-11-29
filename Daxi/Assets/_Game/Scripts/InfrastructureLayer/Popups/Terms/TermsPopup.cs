using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Terms
{
    public class TermsPopup:PopupBase
    {
        #region Factory
        public class TermsPopupFactory:PlaceholderFactory<TermsPopup>
        {
            
        }
        #endregion
        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private Button termsButton;

        [SerializeField]
        private Button privacyButton;

        [SerializeField]
        private string termsUrl;

        [SerializeField]
        private string privacyUrl;

        private bool _playerClickedBack;

        public bool PlayerClikedBack=> _playerClickedBack;

        protected override void Start()
        {
            base.Start();
            _backButton.onClick.AddListener(OnBackClick);
            privacyButton.onClick.AddListener(OpenBrowserPrivacy);
            termsButton.onClick.AddListener(OpenBrowserTerms);
        }

        private void OpenBrowserTerms()
        {
            Application.OpenURL(termsUrl);
        }

        private void OpenBrowserPrivacy()
        {
            Application.OpenURL(privacyUrl);
        }

        private void OnBackClick()
        {
            _playerClickedBack = true;
        }
    }
}
