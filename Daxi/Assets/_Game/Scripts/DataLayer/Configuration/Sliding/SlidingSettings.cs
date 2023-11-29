
using UnityEngine;

namespace Daxi.DataLayer.Configuration.Sliding
{
    [CreateAssetMenu(fileName = "SlidingSettings", menuName = "Configuration/Sliding/SlidingSettings")]
    public class SlidingSettings:ScriptableObject
    {
        #region Fields
        [SerializeField]
        private float _slidingtime;

        [SerializeField]
        private Vector2 _colliderShrinkSize;

        [SerializeField]
        private Vector2 _colliderStartSize;

        [SerializeField]
        private Vector2 _colliderStartOffset;

        [SerializeField]
        private Vector2 _colliderShrinkOffset;

        [SerializeField]
        private LayerMask _groundLayer;

        [SerializeField]
        private float _groundDistance;

        #endregion

        #region Properties
        public float Slidingtime => _slidingtime; 
        public Vector2 ColliderShrinkSize  => _colliderShrinkSize;

        public LayerMask GroundLayer => _groundLayer;
        public float GroundDistance => _groundDistance;

        public Vector2 ColliderStartSize  => _colliderStartSize;

        public Vector2 ColliderStartOffset => _colliderStartOffset;
        public Vector2 ColliderShrinkOffset => _colliderShrinkOffset;
        #endregion
    }
}
