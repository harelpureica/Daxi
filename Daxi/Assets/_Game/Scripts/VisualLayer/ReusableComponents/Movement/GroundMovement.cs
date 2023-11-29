

using Daxi.DataLayer.Configuration.Movement;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Movement
{
    public class GroundMovement : IGroundMovement
    {
        #region Injects
        [Inject]
        private Rigidbody2D _rb;

        [Inject]
        private GroundMovementSettings _settings;

        #endregion

        #region Methods
        public void Move(float x)
        {
            var groundedCollider = Physics2D.OverlapCircle(_rb.position+_settings.GroundCheckOffset, _settings.OverlapRaduis, _settings.GroundLayer);
            if (groundedCollider == null)
            {
                return;
            }
            var hit = Physics2D.Raycast(_rb.position ,Vector2.down, _settings.GroundDistance, _settings.GroundLayer);
            var Gravity = (-hit.normal.normalized) * _settings.Gravity;
            _rb.gravityScale = 0;
            if (hit.collider==null)
            {              
                return;
            }
           
            var slopDirection = -Vector2.Perpendicular(hit.normal);          
            var watntedVelocity = (slopDirection.normalized * _settings.Speed)+Gravity;
            _rb.velocity = Vector2.Lerp(_rb.velocity, watntedVelocity, _settings.Acceleration * Time.deltaTime);

        }       
        #endregion

    }
}
