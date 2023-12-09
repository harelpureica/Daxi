

using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Player;
using Daxi.DataLayer.StoreData;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Store
{
    public class StoreController
    {
        #region Injects
        [Inject]
        private PlayerData _playerData;

        [Inject(Id = "Hearts")]
        private List<StoreItem> _hearts;

        [Inject(Id = "Powers")]
        private List<StoreItem> _powers;

        [Inject(Id = "Pets")]
        private List<StoreItem> _pets;

        [Inject(Id = "Skins")]
        private List<StoreItem> _skins;

        public enum StoreState { skins,powers,pets,hearts}

        private StoreState _state;
        #endregion

        #region Methods
        public void SwitchState (StoreState storeState)
        {
            if (_state == storeState)
            {
                return;
            }
            _state =storeState;

        }

        public void SelectItem(StoreItem storeItem)
        {
            switch(_state)
            {
                case StoreState.skins:
                    SelectSkin(storeItem);
                    break;

                case StoreState.powers:
                    SelectPower(storeItem);
                    break;

                case StoreState.pets:
                    SelectPet(storeItem);
                    break;

                case StoreState.hearts:
                    SelectHeart(storeItem);

                    break;

            }
        }

        private void SelectHeart(StoreItem storeItem)
        {            
            _playerData.AddHeart(storeItem.Amount);
        }

        private void SelectPet(StoreItem storeItem)
        {
            var id=int.Parse(storeItem.Id);
            for (int i = 0; i < _playerData.UnlockedPets.Length; i++)
            {
                if (_playerData.UnlockedPets[i]== id)
                {
                    _playerData.SetPet(id);
                    return;
                }
            }
            _playerData.UnlockPet(id);
            _playerData.SetPet(id);
        }

        private void SelectPower(StoreItem storeItem)
        {
            switch(storeItem.Id)
            {
                case "Plank":
                    _playerData.AddPlank(storeItem.Amount);
                    break;

                case "Shield":
                    _playerData.AddShield(storeItem.Amount);
                    break;

                case "Gum":
                    _playerData.AddGum(storeItem.Amount);

                    break;


            }
        }

        private void SelectSkin(StoreItem storeItem)
        {
            var id = int.Parse(storeItem.Id);
            for (int i = 0; i < _playerData.UnlockedCharacters.Length; i++)
            {
                if (_playerData.UnlockedCharacters[i] == id)
                {
                    _playerData.SetCharcter(id);
                    return;
                }
            }
            _playerData.UnlockCharacter(id);
            _playerData.SetCharcter(id);
        }
        private async UniTask<bool> Purchase(StoreItem storeItem)
        {
            var Purchased = false;

            while(!Purchased)
            {
                await UniTask.Yield();
            }
           
            return Purchased;
        }
        #endregion
    }
}
