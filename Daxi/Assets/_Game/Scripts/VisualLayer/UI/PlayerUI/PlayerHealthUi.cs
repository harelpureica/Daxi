using Cysharp.Threading.Tasks;
using Daxi.InfrastructureLayer.Signals;
using Daxi.VisualLayer.ReusableComponents.Damaging;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.UI.PlayerUI
{
    public class PlayerHealthUi:MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Sprite _heartSprite;

        [SerializeField]
        private ImageIndicator _imageIndicatorPrefab;

        [SerializeField]
        private Transform _parentLayout;

        [SerializeField]
        private TextMeshProUGUI _extraHeartsText;


        private List<ImageIndicator> indicators = new();

        #endregion

        #region Injects
        [Inject]
        private IHealthComponent _healthComponent;
        #endregion


        #region Methods
        private async void Start()
        {
            _healthComponent.OnHealthChange += UpdateUi;
            while(!_healthComponent.Initialized)
            {
                await UniTask.Yield();
            }
            UpdateUi(_healthComponent.Health);
            
        }
        public void UpdateUi(int health)
        {
            for (int i = 0; i < indicators.Count; i++)
            {
                Destroy(indicators[i].gameObject);
            }
            indicators.Clear();

            var extraHearts = 0;
            for (int i = 0; i < health; i++)
            {
                if(i<3)
                {
                    var indicator = Instantiate(_imageIndicatorPrefab, _parentLayout);
                    indicator.SetImage(_heartSprite);
                    indicators.Add(indicator);
                }
                else
                {
                   
                    extraHearts++;
                }
               
            }
            if(extraHearts > 0)
            {
                _extraHeartsText.text = $"{extraHearts}+";
            }else
            {
                _extraHeartsText.text = $"";

            }

        }
        #endregion
    }
}
