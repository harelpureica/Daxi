
using Daxi.DataLayer.GameItems;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.UI.PlayerUI
{
    public class PlayerPowerUpUi:MonoBehaviour
    {
        #region Injects       
        [Inject]
        private PowerUpButton.PowerUpButtonFactory _buttonFactory;

        [Inject]
        private List<GameItemData> _powerUpsData;
        #endregion

        #region Fields
        [SerializeField]
        private RectTransform _parent;

        private List<PowerUpButton> _powerupsIndicators=new();
        #endregion

        #region Methods
        public void SetData(int gumsAmount, int shieldsAmount, int planksAmount)
        {
            //delete buttons
            if(_powerupsIndicators.Count>0)
            {
                for (int i = 0; i < _powerupsIndicators.Count; i++)
                {
                    if (_powerupsIndicators[i] == null)
                    {
                        continue;
                    }
                    Destroy(_powerupsIndicators[i].gameObject);
                }
                _powerupsIndicators.Clear();
            }
            //add buttons
            if (gumsAmount > 0)
            {
                AddButton(GetPowerData("Gum"), gumsAmount);
            }
            if (shieldsAmount > 0)
            {
                AddButton(GetPowerData("Shield"), shieldsAmount);
            }
            if (planksAmount > 0)
            {
                AddButton(GetPowerData("Plank"), planksAmount);
            }
        }   
        
        private void AddButton(GameItemData data ,int powerAmount)
        {
            var powerUpButton = _buttonFactory.Create();
            powerUpButton.SetData(data,powerAmount);
            _powerupsIndicators.Add(powerUpButton);
        }
       
        public void UpdateButtonsFillAmount(GameItemData item,float fill)
        {
            for (int i = 0; i < _powerupsIndicators.Count; i++)
            {
                _powerupsIndicators[i].UpdateFillAmount(item,fill);               
            }
        }
        private GameItemData GetPowerData(string name)
        {
            GameItemData data = null;
            for (int i = 0; i < _powerUpsData.Count; i++)
            {
                if (_powerUpsData[i].MyName==name)
                {
                    data= _powerUpsData[i];
                }
            }
            return data;

        }
      
        
        #endregion
    }
}
