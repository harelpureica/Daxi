using Daxi.InfrastructureLayer.Popups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._Game.Scripts.InfrastructureLayer.Popups.DataSetter
{
    public class DataSetterPopup:PopupBase
    {
        #region Factory
        public class DataSetterPopupFactory:PlaceholderFactory<DataSetterPopup>
        {

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
