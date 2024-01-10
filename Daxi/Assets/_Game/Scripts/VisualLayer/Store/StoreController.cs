

using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Player;
using Daxi.DataLayer.StoreData;
using Daxi.InfrastructureLayer.ScenesManagment;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.Store
{
    public class StoreController:IInitializable
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


        [Inject(Id = "Hearts")]
        private Button _heartBtn;

        [Inject(Id = "Powers")]
        private Button _powersBtn;

        [Inject(Id = "Pets")]
        private Button _petsBtn;

        [Inject(Id = "Skins")]
        private Button _skinsBtn;


        [Inject(Id = "Hearts")]
        private GameObject _heartOutline;

        [Inject(Id = "Powers")]
        private GameObject _powersOutline;

        [Inject(Id = "Pets")]
        private GameObject _petsOutline;

        [Inject(Id = "Skins")]
        private GameObject _skinsOutline;

        [Inject(Id="SubTitle")]
        private TextMeshProUGUI _subTitle;

        [Inject(Id = "Select")]
        private Button _selectBtn;

       


        [Inject]
        private StoreMenu _menu;

        [Inject(Id ="Back")]
        private Button _backBtn;

        [Inject]
        private IScenesLoader _scenesLoader;



        public enum StoreState { skins,powers,pets,hearts}

        private StoreState _state;

        public StoreState State  => _state;
        #endregion

        #region Methods

        public void Initialize()
        {
            _heartBtn.onClick.AddListener(() => SwitchState(StoreState.hearts));
            _powersBtn.onClick.AddListener(() => SwitchState(StoreState.powers));
            _petsBtn.onClick.AddListener(() => SwitchState(StoreState.pets));
            _skinsBtn.onClick.AddListener(() => SwitchState(StoreState.skins));
            _selectBtn.onClick.AddListener(SelectItem);
            _menu.SetData(_skins,StoreState.skins);
            _state= StoreState.skins;
            _backBtn.onClick.AddListener(OnBackClick);
            

        }

        private void OnBackClick()
        {
            _scenesLoader.LoadSceneAsync(ScenesNames.Menu);
        }

        public void SwitchState(StoreState storeState)
        {
            if (_state == storeState)
            {
                return;
            }
            _state = storeState;
            var storeitems = new List<StoreItem>();
            switch (_state)
            {
                case StoreState.skins:
                    storeitems = _skins;
                    _subTitle.text = "The main character";
                    break;

                case StoreState.powers:
                    storeitems = _powers;
                    _subTitle.text = "Powers";
                    break;

                case StoreState.pets:
                    storeitems = _pets;
                    _subTitle.text = "The flying pet";
                    break;

                case StoreState.hearts:
                    storeitems = _hearts;
                    _subTitle.text = "Lifes";
                    break;

            }
            _menu.SetData(storeitems,_state);
            SetButtonsOutline(storeState);

        }
        private void SetButtonsOutline(StoreState storeState)
        {
            switch (storeState)
            {
                case StoreState.skins:
                    _skinsOutline.gameObject.SetActive(true);
                    _powersOutline.gameObject.SetActive(false);
                    _petsOutline.gameObject.SetActive(false);
                    _heartOutline.gameObject.SetActive(false);

                    break;

                case StoreState.powers:
                    _skinsOutline.gameObject.SetActive(false);
                    _powersOutline.gameObject.SetActive(true);
                    _petsOutline.gameObject.SetActive(false);
                    _heartOutline.gameObject.SetActive(false);
                    break;

                case StoreState.pets:
                    _skinsOutline.gameObject.SetActive(false);
                    _powersOutline.gameObject.SetActive(false);
                    _petsOutline.gameObject.SetActive(true);
                    _heartOutline.gameObject.SetActive(false);
                    break;

                case StoreState.hearts:
                    _skinsOutline.gameObject.SetActive(false);
                    _powersOutline.gameObject.SetActive(false);
                    _petsOutline.gameObject.SetActive(false);
                    _heartOutline.gameObject.SetActive(true); 
                    break;

            }
        }

        public void SelectItem()
        {
            var storeItem = _menu.CurrentItem.MyStoreItem;
            switch (_state)
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

        private async void SelectHeart(StoreItem storeItem)
        {
            var purchased = await Purchase(storeItem);
            if (purchased)
            {
                _playerData.AddHeart(storeItem.Amount);

            }
        }

        private async void SelectPet(StoreItem storeItem)
        {
            var id=int.Parse(storeItem.MyId);
            for (int i = 0; i < _playerData.UnlockedPets.Length; i++)
            {
                if (_playerData.UnlockedPets[i]== id)
                {
                    _playerData.SetPet(id);
                    return;
                }
            }
            var purchased = await Purchase(storeItem);
            if (purchased)
            {
                _playerData.UnlockPet(id);
                _playerData.SetPet(id);
            }
        }

        private async void SelectPower(StoreItem storeItem)
        {
            var purchased = await Purchase(storeItem);
            if (purchased)
            {
                switch (storeItem.MyId)
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
        }

        private async void SelectSkin(StoreItem storeItem)
        {
            var id = int.Parse(storeItem.MyId);
            for (int i = 0; i < _playerData.UnlockedCharacters.Length; i++)
            {
                if (_playerData.UnlockedCharacters[i] == id)
                {
                    _playerData.SetCharcter(id);
                    return;
                }
            }
            var purchased=await Purchase(storeItem);
            if (purchased)
            {
                _playerData.UnlockCharacter(id);
                _playerData.SetCharcter(id);
            }
           
        }
        private async UniTask<bool> Purchase(StoreItem storeItem)
        {
            var Purchased = true;

           
           
            return Purchased;
        }

      
        #endregion
    }
}
