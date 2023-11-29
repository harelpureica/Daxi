

using UnityEngine;

namespace Daxi.DataLayer.Configuration.PowerUps
{
    [CreateAssetMenu(fileName = "PowerupsComponentSettings", menuName = "Configuration/PowerUps/PowerupsComponentSettings")]
    public class PowerupsComponentSettings:ScriptableObject
    {
        #region Fields
        [SerializeField]
        private float _gumTime;

        [SerializeField]
        private Vector2 _plankOffset;

        [SerializeField]
        private float _shieldTime;

        [SerializeField]
        private float _gumChargingTime;

        [SerializeField]
        private float _plankChargingTime;

        [SerializeField]
        private float _shieldChargingTime;

        #endregion

        #region Properties
        public float GumTime => _gumTime;
        public Vector2 PlankOffset => _plankOffset; 
        public float ShieldTime  => _shieldTime;

        public float GumChargingTime => _gumChargingTime;
        public float PlankChargingTime  => _plankChargingTime;
        public float ShieldChargingTime => _shieldChargingTime; 

        #endregion
    }
}
