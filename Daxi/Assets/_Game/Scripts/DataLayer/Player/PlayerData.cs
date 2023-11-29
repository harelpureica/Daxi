

using UnityEngine;

namespace Daxi.DataLayer.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player/PlayerData")]
    public class PlayerData:ScriptableObject
    {
        #region Fields      

        [SerializeField]
        [Range(0f, 6f)]
        private int _characterIndex;

        [SerializeField]
        private int _unlockedLevels;

        [SerializeField]
        private int _gums;

        [SerializeField]
        private int _planks;

        [SerializeField]
        private int _shields;

        [SerializeField]
        private int _hearts;

        [SerializeField]
        private int _petIndex;    

        #endregion

        #region Properties
        public int CharacterIndex => _characterIndex;

        public int UnlockedLevels=>_unlockedLevels;
        public int Gums=>_gums;
        public int Planks=>_planks;
        public int Shields=>_shields;
        public int Hearts=>_hearts;

        public int PetIndex => _petIndex;
        #endregion

        #region Methods
        public void Initialize(int characterIndex, int unlockedLevels, int gums, int planks, int shields, int hearts,int petIndex)
        {
            _characterIndex = characterIndex;
            _unlockedLevels = unlockedLevels;
            _gums = gums;
            _planks = planks;
            _shields = shields;
            _hearts = hearts;
            _petIndex=petIndex;
        }
        #endregion
    }
}
