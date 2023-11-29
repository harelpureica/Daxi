using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Configuration.Camera;
using Daxi.InfrastructureLayer.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player
{
    public class PlayersCameraController : IFixedTickable,IInitializable
    {
        #region Fields  
        [Inject]
        private Camera _camera;

        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private PlayersCameraControllerSettings _settings;

        private Transform _target;

        private Transform _camTransfrom;

        private PlayerManager _playerManager;

        private bool slowYAxis;

        private Vector3 _offset;


        #endregion

        #region Properties
        public bool SlowYAxis { get => slowYAxis; set => slowYAxis = value; }

        #endregion

        #region Methods
        public void SetPlayer(PlayerManager player)
        {
            _target = player.transform;
            _playerManager = player;
            _camTransfrom=_camera.transform;
            _offset = _settings.CameraOffset;
        }

        public void FixedTick()
        {
            if (_target == null)
            {
                return;
            }
            var  wantedPosition = _target.position + _offset;
            var smoothedPosition=Vector3.zero;
            var vel = Vector3.zero;

            if(slowYAxis&&_target.position.y>_camTransfrom.position.y-_settings.CameraOffset.y)
            {
                wantedPosition = new Vector3(wantedPosition.x,Mathf.MoveTowards(_camTransfrom.position.y,wantedPosition.y,_settings.SlowedYAxisSpeed*Time.fixedDeltaTime),wantedPosition.z);
            }
            switch (_settings.Mode)
            {
                case PlayersCameraControllerSettings.CameraMovementMode.lerp:
                    smoothedPosition = Vector3.Lerp(_camTransfrom.position, wantedPosition, Time.fixedDeltaTime * _settings.Speed);

                    break;
                case PlayersCameraControllerSettings.CameraMovementMode.damp:
                    smoothedPosition = Vector3.SmoothDamp(_camTransfrom.position, wantedPosition,ref vel, Time.fixedDeltaTime * _settings.Speed);

                    break;
                case PlayersCameraControllerSettings.CameraMovementMode.constent:
                    smoothedPosition = wantedPosition;
                    break;              
            }
            _camTransfrom.position = smoothedPosition;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OnDownHillZoneChange>(OnDownHillZoneChangeHandler);
        }

        private void OnDownHillZoneChangeHandler(OnDownHillZoneChange args)
        {
            if(args.InZone)
            {
                LerpOffset( _settings.CameraOffset + new Vector3(0, -4, 0));

            }
            else
            {
                LerpOffset( _settings.CameraOffset);

            }
        }
        private async void LerpOffset(Vector3 wantedOffset)
        {
            var lerp = 0f;
            var startOffset = _offset;
            while(lerp<1)
            {
                _offset = Vector3.Lerp(startOffset, wantedOffset, lerp);
                lerp += Time.deltaTime/0.75f;
                await UniTask.Yield();
            }
            _offset = wantedOffset;
        }
        #endregion

    }
}
