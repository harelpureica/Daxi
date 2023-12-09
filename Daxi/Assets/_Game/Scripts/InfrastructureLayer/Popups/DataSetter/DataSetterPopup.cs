using Daxi.DataLayer.Player;
using Daxi.InfrastructureLayer.Popups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.DataSetter
{
    public class DataSetterPopup:PopupBase
    {
        #region Factory
        public class DataSetterPopupFactory:PlaceholderFactory<DataSetterPopup>
        {

        }
        [Inject]
        private PlayerData playerData;
        public void Initialize()
        {
            _characterIndex.value = playerData.CharacterIndex;
            _unlockedLevels.value = playerData.UnlockedLevels;
            _gums.value = playerData.Gums;
            _planks.value = playerData.Planks;
            _shields.value = playerData.Shields;
            _hearts.value = playerData.Hearts;
            _petIndex.value = playerData.PetIndex;
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
