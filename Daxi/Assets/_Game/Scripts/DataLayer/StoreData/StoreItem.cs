

using UnityEngine;

namespace Daxi.DataLayer.StoreData
{
    [CreateAssetMenu(fileName = "StoreItem", menuName = "Data/StoreData/StoreItem")]
    public class StoreItem:ScriptableObject
    {
        #region Fields
        [SerializeField]
        private string _id;        

        [SerializeField]
        private float _cost;       

        [SerializeField]
        private Sprite _descriptionSprite;

        [SerializeField]
        private Sprite _sprite;

        [SerializeField]
        private int _amount;

       
        #endregion

        #region Propeties
        public string Id  => _id; 
        public float Cost=> _cost; 
        public Sprite DescriptionSprite => _descriptionSprite;
        public Sprite Sprite  => _sprite;

        public int Amount => _amount; 

        #endregion

    }
}
