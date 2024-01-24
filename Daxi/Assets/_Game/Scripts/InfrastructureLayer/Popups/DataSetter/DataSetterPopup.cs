using Daxi.DataLayer.Player;
using Daxi.DataLayer.StoreData;
using Daxi.InfrastructureLayer.Popups;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.DataSetter
{
    public class DataSetterPopup:PopupBase
    {
        [Inject(Id = "Skins")]
        private List<StoreItem> _skinsData;

        [Inject(Id = "Hearts")]
        private List<StoreItem> _heartsData;

        [Inject(Id = "Pets")]
        private List<StoreItem> _petsData;

        [Inject(Id = "Powers")]
        private List<StoreItem> _powersData;


        #region Factory
        public class DataSetterPopupFactory:PlaceholderFactory<DataSetterPopup>
        {

        }
        [Inject]
        private PlayerData _playerData;
        public void Initialize()
        {
            _characterIndex.value = _playerData.CharacterIndex;
            _unlockedLevels.value = _playerData.UnlockedLevels;
            _gums.value = _playerData.Gums;
            _planks.value = _playerData.Planks;
            _shields.value = _playerData.Shields;
            _hearts.value = _playerData.Hearts;
            _petIndex.value = _playerData.PetIndex;

            _characterIndexDescription.text = $"{_skinsData[_playerData.CharacterIndex].name}";
            _unlockedLevelsDescription.text = $"{_playerData.UnlockedLevels}";
            _gumsDescription.text = $"{_playerData.Gums}";
            _planksDescription.text =$"{_playerData.Planks}";
            _shieldsDescription.text = $"{_playerData.Shields}";
            _heartsDescription.text = $"{_playerData.Hearts}";
            _petIndexDescription.text =$"{_playerData.PetIndex}";
            _characterIndex.onValueChanged.AddListener((index) =>
            {
                _characterIndexDescription.text = $"{_skinsData[(int)index].name}";
            });
            _unlockedLevels.onValueChanged.AddListener((index) =>
            {
                _unlockedLevelsDescription.text = $"{(int)index}";
            });
            _gums.onValueChanged.AddListener((index) =>
            {
                _gumsDescription.text = $"{(int)index}";
            });
            _planks.onValueChanged.AddListener((index) =>
            {
                _planksDescription.text = $"{(int)index}";
            });
            _shields.onValueChanged.AddListener((index) =>
            {
                _shieldsDescription.text = $"{(int)index}";
            });
            _hearts.onValueChanged.AddListener((index) =>
            {
                _heartsDescription.text = $"{(int)index}";
            });
            _petIndex.onValueChanged.AddListener((index) =>
            {
                _petIndexDescription.text = $"{_petsData[(int)index].name}";
            });
        }
                  
        #endregion

        #region Fields
        [SerializeField]
        private Slider _characterIndex;

        [SerializeField]
        private Slider _unlockedLevels;

        [SerializeField]
        private Slider _gums;

        [SerializeField]
        private Slider _planks;

        [SerializeField]
        private Slider _shields;

        [SerializeField]
        private Slider _hearts;

        [SerializeField]
        private Slider _petIndex;


        [SerializeField]
        private TextMeshProUGUI _characterIndexDescription;

        [SerializeField]
        private TextMeshProUGUI _unlockedLevelsDescription;

        [SerializeField]
        private TextMeshProUGUI _gumsDescription;

        [SerializeField]
        private TextMeshProUGUI _planksDescription;

        [SerializeField]
        private TextMeshProUGUI _shieldsDescription;

        [SerializeField]
        private TextMeshProUGUI _heartsDescription;

        [SerializeField]
        private TextMeshProUGUI _petIndexDescription;


        #endregion

        #region Properties
        public int CharacterIndex => (int)_characterIndex.value;
        public int UnlockedLevels => (int)_unlockedLevels.value;
        public int Gums => (int)_gums.value;
        public int Planks => (int)_planks.value;
        public int Shields => (int)_shields.value;
        public int Hearts => (int)_hearts.value;

        public int PetIndex =>(int)_petIndex.value;
        #endregion
    }
}
