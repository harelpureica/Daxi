using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.VerifyAge
{
    public class VerifyAgePopup:PopupBase
    {
        #region Factory
        public class VerifyAgePopupFactory: PlaceholderFactory<VerifyAgePopup>
        {

        }
        #endregion

        #region Fields
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private int _maxAge;

        [SerializeField]
        private int _minAge;

        [SerializeField]
        private TextMeshProUGUI _text;

        #endregion

        #region Properties
        public int Age => (int)_slider.value;
        #endregion

        #region Methods
        protected override void Start()
        {
            base.Start();
            _slider.minValue = _minAge;
            _slider.maxValue = _maxAge;
            _slider.onValueChanged.AddListener(OnSliderValueChange);
        }
        public void OnSliderValueChange(float value)
        {
           _text.text=$"{_slider.value}";
        }
        #endregion
    }
}
