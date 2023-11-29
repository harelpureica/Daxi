using UnityEngine;

namespace Daxi.DataLayer.GameItems
{
    [CreateAssetMenu(fileName = "GameItemData", menuName = "Data/GameItems/GameItemData")]
    public class GameItemData:ScriptableObject
    {
        #region Fields
        [SerializeField]
        private Sprite _sprite;

        [SerializeField]
        private string _name;

       
        #endregion

        #region Properties       
        public Sprite Sprite  => _sprite;
        public string MyName => _name;
        #endregion
    }
}
