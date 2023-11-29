using Cysharp.Threading.Tasks;
using Daxi.DataLayer.GameItems;
using Daxi.InfrastructureLayer.Signals;
using Daxi.VisualLayer.UI.PlayerUI;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Daxi.DataLayer.Configuration.PowerUps;
using System;
using Daxi.DataLayer.Player;
using System.Linq;

namespace Daxi.VisualLayer.Player.PowerUps
{
    public class PlayerPowerUpComponent:IInitializable
    {
        #region Injects
        [Inject]
        private PowerupsComponentSettings _settings;

        [Inject]
        private PlayerManager _playerManager;      

        [Inject]
        private PlayerPowerUpUi _playerPowerUpUi;

        [Inject]
        private Plank.Factory _plankFactory;

        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private ShieldComponent _shieldComponent;

        [Inject]
        private PlayerAnimationComponent _playerAnimationComponent;
       
        #endregion

        #region Fields
        private List<GameItemData> _collectedPowerUps = new();

        private bool _gumAvailable=true;

        private bool _shieldAvailable = true;

        private int _gumAmount;

        private int _shieldAmount;

        private int _planksAmount;

        #endregion

        #region Methods
        public void SetData(PlayerData playerData)
        {
            _gumAmount = playerData.Gums;
           _planksAmount= playerData.Planks;
            _shieldAmount= playerData.Shields;
            _playerPowerUpUi.SetData(_gumAmount, _shieldAmount, _planksAmount);
        }
        public void AddPowerUp()
        {
            if(_gumAmount>0&&_planksAmount>0&&_shieldAmount>0)
            {
                return;
            }
            var powerupAdded = false;
            while(!powerupAdded)
            {
                var random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                       
                        if (_gumAmount <= 0)
                        {
                            _gumAmount++;
                            powerupAdded = true;
                        }
                        break;
                    case 1:
                        if (_shieldAmount <= 0)
                        {
                            _shieldAmount++;
                            powerupAdded = true;
                        }
                        break;
                    case 2:
                        if (_planksAmount <= 0)
                        {
                            _planksAmount++;
                            powerupAdded = true;

                        }
                        break;
                }
            }
                                          
            _playerPowerUpUi.SetData(_gumAmount,_shieldAmount,_planksAmount);

        }
      
        public void Initialize()
        {
            _signalBus.Subscribe<OnPowerUpCollected>(AddPowerUp);
        }
        private async UniTask UpdateButtonsFillAmount(GameItemData powerup,float time)
        {
            var lerp = 0f;
            while(lerp<1)
            {
                _playerPowerUpUi.UpdateButtonsFillAmount(powerup,lerp);
                lerp += Time.deltaTime/time;
                await UniTask.Yield();
            }
        }
        public  async void PowerUp(GameItemData powerup)
        {
            switch (powerup.MyName)
            {
                case "Plank":
                   
                    Plank();
                    _planksAmount--;                   
                    break;

                case "Gum":
                    if (!_gumAvailable)
                    {
                        return;
                    }
                    _gumAvailable = false;
                    Gum();
                    await UpdateButtonsFillAmount(powerup, _settings.GumChargingTime);
                    _gumAmount--;
                    await _playerAnimationComponent.AnimateEndGum();
                    _gumAvailable = true;
                    break;

                case "Shield":
                    if (!_shieldAvailable)
                    {
                        return;
                    }
                    _shieldAvailable = false;
                    Shield();
                    await UpdateButtonsFillAmount(powerup, _settings.ShieldChargingTime);
                    _shieldAmount--;
                    await _shieldComponent.EndShield();
                    _shieldAvailable = true;


                    break;
            }
            _playerPowerUpUi.SetData(_gumAmount, _shieldAmount, _planksAmount);
          

        }
      
     
        private async void Gum()
        {
            _playerManager.HaveGum = true;
            _playerAnimationComponent.AnimateGum();
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.GumTime));            
            _playerManager.HaveGum = false;

        }
        private  void Plank()
        {
            var plank = _plankFactory.Create();
            plank.transform.position = _playerManager.transform.position + new Vector3(_settings.PlankOffset.x, _settings.PlankOffset.y, 0);
            plank.PowerUp();
        }
        private async void Shield()
        {
            _shieldComponent.Shield();
            _playerManager.HaveShield = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.ShieldTime));
            var colidersNear= Physics2D.OverlapCircleAll(_playerManager.transform.position, 2);
            var damagingElemetnsNear = 0;
            for (int i = 0; i < colidersNear.Length; i++)
            {
                if (colidersNear[i].TryGetComponent<IDamaging>(out var damaging))
                {
                    damagingElemetnsNear++;
                }
            }           
            while(damagingElemetnsNear>0)
            {
                damagingElemetnsNear = 0;
                for (int i = 0; i < colidersNear.Length; i++)
                {
                    if (colidersNear[i].TryGetComponent<IDamaging>(out var damaging))
                    {
                        damagingElemetnsNear++;
                    }
                }
                await UniTask.Yield();
            }
            _playerManager.HaveShield = false;
          
        }

        #endregion
    }
}
