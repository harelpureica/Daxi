using TMPro;
using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Text
{
    public class TextPopup : PopupBase
    {
        #region Factory
        public class TextPopupFactory:PlaceholderFactory<TextPopup>
        {

        }
        #endregion     

        #region Fields
        [SerializeField]
        private TextMeshProUGUI _textUi;
        #endregion

        #region Methods
        public void SetText(string text)
        {
            _textUi.text= text;
        }
        #endregion


    }
}
