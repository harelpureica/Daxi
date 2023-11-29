using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daxi.DataLayer.Configuration.Movement
{
    [CreateAssetMenu(fileName = "GroundMovementSettings", menuName = "Configuration/Movement/GroundMovementSettings")]
    public class GroundMovementSettings : ScriptableObject
    {
        #region Fields
        [SerializeField]
        private float _overlapRaduis;

        [SerializeField]
        private float _groundDistance;

        [SerializeField]
        private LayerMask _groundMask;

        [SerializeField]
        private float acceleration;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _gravity;

        [SerializeField]
        private Vector2 _groundCheckOffset;

        #endregion

        #region Properties
        public float Speed => _speed;
        public float GroundDistance => _groundDistance;
        public LayerMask GroundLayer => _groundMask;
        public float Acceleration => acceleration;

        public float Gravity => _gravity;

        public Vector2 GroundCheckOffset => _groundCheckOffset;

        public float OverlapRaduis => _overlapRaduis;

        #endregion
    }

}
