
using Daxi.DataLayer.GameItems;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.Player;
using Daxi.VisualLayer.Player.PowerUps;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.UI
{
    public class PowerUpButton :MonoBehaviour
    {
        #region Factory
        public class PowerUpButtonFactory:PlaceholderFactory<PowerUpButton>
        {

        }
        #endregion

        #region Injects
        [Inject]
        private PlayerManager _playerManager;

        [Inject]
        private LevelManager _levelManager;
       
        #endregion

        #region Fields
        private GameItemData _powerUp;

        [SerializeField]
        private DaxiButton _daxiButton;

        [SerializeField]
        private ImageIndicator _imageIndicator;

        [SerializeField]
        private TextMeshProUGUI _amountText;

        [SerializeField]
        private Image _fillImage;
        #endregion

        #region Methods
        public void SetData(GameItemData data,int amount)
        {
            _powerUp = data;
            _imageIndicator.SetImage(_powerUp.Sprite);
            _fillImage.sprite = _powerUp.Sprite;
            _fillImage.fillAmount = 0;
            _daxiButton.OnClickDown += PowerUp;
            if (_powerUp.MyName == "Plank")
            {
                _fillImage.enabled = false;
            }
            if(amount>1)
            {
                _amountText.text = $"{amount}";
            }else
            {
                _amountText.text = $"";
            }
        }
        public void UpdateFillAmount(GameItemData powerUp,float fill)
        {
            if(_powerUp!= powerUp|| _powerUp.MyName == "Plank")
            {
                return;
            }          
            _fillImage.fillAmount= fill;
        }
        public void PowerUp()
        {
            if(_levelManager.MyState==LevelManager.LevelState.run)
            {
                _playerManager.Powerup(_powerUp);
                if (_powerUp.MyName == "Plank")
                {
                    Destroy(gameObject);
                }
            }
            
        }
        #endregion
    }
}
