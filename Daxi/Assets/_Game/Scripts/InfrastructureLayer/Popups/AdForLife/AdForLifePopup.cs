using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.AdForLife
{
    public class AdForLifePopup:PopupBase
    {
        #region Factory
        public class AdForLifePopupFactory:PlaceholderFactory<AdForLifePopup>
        {

        }
        #endregion

        #region Fields
        [SerializeField]
        private Button _watchAdButton;

        private bool _playerWantsAd;

        public bool PlayerWantsAd=> _playerWantsAd;
        #endregion
        protected override void  Start()
        {
            base.Start();
            _watchAdButton.onClick.AddListener(() => { _playerWantsAd = true; });
        }
    }
}
