

using Daxi.InfrastructureLayer.Popups.Terms;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.ConfirmedTerms
{
    public class ConfirmedTermsPopup:PopupBase
    {

        #region Factory
        public class ConfirmedTermsPopupFactory : PlaceholderFactory<ConfirmedTermsPopup>
        {

        }
        #endregion   
        [SerializeField]
        private Button termsButton;

        [SerializeField]
        private Button privacyButton;

        [SerializeField]
        private string termsUrl;

        [SerializeField]
        private string privacyUrl;

        protected override void Start()
        {
            base.Start();
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

       
    }
}
