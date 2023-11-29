using UnityEngine;

namespace Daxi.DataLayer.Configuration.Camera
{
    [CreateAssetMenu(fileName = "PlayersCameraControllerSettings", menuName = "Configuration/Camera/PlayersCameraControllerSettings")]
    public class PlayersCameraControllerSettings:ScriptableObject
    {
        #region Fields
        [SerializeField]
        private float speed;

        [SerializeField]
        private CameraMovementMode mode;

        [SerializeField]
        private Vector3 _cameraOffset;

        [SerializeField]
        private float slowedYAxisSpeed;


        public enum CameraMovementMode { lerp,damp,constent}
        #endregion

        #region Properties
        public float Speed => speed;
        public CameraMovementMode Mode => mode;

        public Vector3 CameraOffset=> _cameraOffset;

        public float SlowedYAxisSpeed  => slowedYAxisSpeed; 

        #endregion
    }
}
