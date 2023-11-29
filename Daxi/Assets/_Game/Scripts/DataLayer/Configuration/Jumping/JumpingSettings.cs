
using UnityEngine;

namespace Daxi.DataLayer.Configuration.Jumping
{
    [CreateAssetMenu(fileName = "JumpingSettings", menuName = "Configuration/Jumping/JumpingSettings")]
    public class JumpingSettings:ScriptableObject
    {
        #region Fields
        [SerializeField]
        private float _jumpForce;

        [SerializeField]
        private Vector2 _jumpDirection;

        [SerializeField]
        private LayerMask _groundLayer;

        [SerializeField]
        private float _groundDistance;

        [SerializeField]
        private int _jumpTimes;

        #endregion

        #region Methods
        public float JumpForce  => _jumpForce; 
        public Vector2 JumpDirection  => _jumpDirection; 
        public LayerMask GroundLayer  => _groundLayer; 
        public float GroundDistance  => _groundDistance; 
        public int JumpTimes  => _jumpTimes; 
        #endregion
    }
}
