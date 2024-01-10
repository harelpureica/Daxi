using UnityEngine;

namespace Daxi.DataLayer.Configuration.Player
{
    [CreateAssetMenu (fileName = "PlayerSettings", menuName = "Configuration/Player/PlayerSettings")]
    public class PlayerSettings:ScriptableObject
    {
        #region Fields
        [SerializeField]
        private int _startHealth;

        [SerializeField]
        private LayerMask _groundLayer;

        [SerializeField]
        private float _groundRadius;

        [SerializeField]
        private float _pullForceToRight;

        [SerializeField]

        private Vector2 _overlapOffset;

        #endregion

        #region Properties
        public int StartHealth=>_startHealth;

        public LayerMask GroundLayer => _groundLayer;
        public float GroundRadius => _groundRadius;
        public float PullForceToRight => _pullForceToRight;
        public Vector2 OverlapOffset => _overlapOffset;

        #endregion
    }
}
