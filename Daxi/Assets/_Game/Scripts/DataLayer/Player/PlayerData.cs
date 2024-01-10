

using System;
using UnityEngine;

namespace Daxi.DataLayer.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player/PlayerData")]
    public class PlayerData:ScriptableObject
    {
        #region Fields      

        [SerializeField]
        [Range(0f, 6f)]
        private int _character;

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

        [SerializeField]
        private string _unlockedCharacters;

        [SerializeField]
        private string _unlockedPets;

        #endregion

        #region Events
        public event Action OnDataChenged;
        #endregion

        #region Properties
        public int CharacterIndex => _character;

        public int UnlockedLevels=>_unlockedLevels;
        public int Gums=>_gums;
        public int Planks=>_planks;
        public int Shields=>_shields;
        public int Hearts=>_hearts;

        public int PetIndex => _petIndex;

        public string UnlockedCharacters => _unlockedCharacters;

        public string UnlockedPets => _unlockedPets;
        #endregion

        #region Methods
        #region Stroage
        public void SetDataFromPopup(int characterIndex, int unlockedLevels, int gums, int planks, int shields, int hearts,int petIndex)
        {
            _character = characterIndex;
            UnlockCharacter(characterIndex);
            _unlockedLevels = unlockedLevels;           
            _gums = gums;
            _planks = planks;
            _shields = shields;
            _hearts = hearts;
            _petIndex=petIndex;
            UnlockPet(petIndex);
            OnDataChenged?.Invoke();
        }
        public string GetStringData()
        {
            var data = $"{CharacterIndex}|{_unlockedLevels}|{_gums}|{_planks}|{_shields}|{_hearts}|{_petIndex}|{_unlockedCharacters}|{UnlockedPets}";
            return data;
        }
        public void SetStringData(string data)
        {
            if(string.IsNullOrEmpty(data))
            {
                Debug.LogError("data is empty");
                return;
            }
            var parts=data.Split('|');
            _character = int.Parse(parts[0]) ;
            _unlockedLevels = int.Parse(parts[1]);
            _gums = int.Parse(parts[2]);
            _planks = int.Parse(parts[3]);
            _shields = int.Parse(parts[4]);
            _hearts = int.Parse(parts[5]);
            _petIndex = int.Parse(parts[6]);
            _unlockedCharacters = parts[7];
            _unlockedPets = parts[8];

        }
        #endregion

        #region Adjusments
        public void AddHeart(int amount)
        {
            _hearts += amount;
            OnDataChenged?.Invoke();
        }
        public void RemoveHeart(int amount)
        {
            if(_hearts - amount>0)
            {
                _hearts -= amount;
            }else
            {
                _hearts = 0;
            }
            OnDataChenged?.Invoke();
        }
        public void AddPlank(int amount)
        {
            _planks += amount;
            OnDataChenged?.Invoke();
        }
        public void RemovePlank(int amount)
        {
            if (_planks - amount > 0)
            {

                _planks -= amount;
            }
            else
            {
                _planks = 0;
            }
            OnDataChenged?.Invoke();
        }
        public void AddShield(int amount)
        {
            _shields += amount;
            OnDataChenged?.Invoke();
        }
        public void RemoveShield(int amount)
        {
            if (_shields - amount > 0)
            {

                _shields -= amount;
            }
            else
            {
                _shields = 0;
            }
            OnDataChenged?.Invoke();
        }
        public void AddGum(int amount)
        {
            _gums += amount;
            OnDataChenged?.Invoke();
        }
        public void RemoveGum(int amount)
        {
            if (_gums - amount > 0)
            {

                _gums -= amount;
            }
            else
            {
                _gums = 0;
            }
            OnDataChenged?.Invoke();
        }
        public void UnlockCharacter(int index)
        {
            for (int i = 0; i < _unlockedCharacters.Length; i++)
            {
                if(_unlockedCharacters[i].ToString() == index.ToString())
                {
                    return;
                }
            }
            _unlockedCharacters = _unlockedCharacters + $"{index}";
             OnDataChenged?.Invoke();
        }
        public void SetCharcter(int character)
        {
            for (int i = 0; i < _unlockedCharacters.Length; i++)
            {
                if (_unlockedCharacters[i].ToString() == character.ToString())
                {
                    _character = character;
                    OnDataChenged?.Invoke();
                    return;
                }
            }
            
        }
        public void UnlockPet(int index)
        {
            for (int i = 0; i < _unlockedPets.Length; i++)
            {
                if (_unlockedPets[i].ToString() == index.ToString())
                {
                    return;
                }
            }
            _unlockedPets = _unlockedPets + $"{index}";
            OnDataChenged?.Invoke();
        }
        public void SetPet(int pet)
        {
            for (int i = 0; i < _unlockedPets.Length; i++)
            {
                if (_unlockedPets[i].ToString() == (pet.ToString()))
                {
                    _petIndex = pet;
                    OnDataChenged?.Invoke();
                    return;
                }
            }

        }

        public void UnlockLevel()
        {
            _unlockedLevels=Mathf.Clamp(_unlockedLevels + 1,1, 18);
            OnDataChenged?.Invoke();
        }

        #endregion
        #endregion
    }
}
