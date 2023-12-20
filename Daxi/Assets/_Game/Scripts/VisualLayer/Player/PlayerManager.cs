
using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Configuration.Player;
using Daxi.DataLayer.GameItems;
using Daxi.DataLayer.Player;
using Daxi.InfrastructureLayer.Signals;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.Player.PowerUps;
using Daxi.VisualLayer.ReusableComponents.Damaging;
using Daxi.VisualLayer.ReusableComponents.Jumping;
using Daxi.VisualLayer.ReusableComponents.Movement;
using Daxi.VisualLayer.ReusableComponents.Sliding;
using Daxi.VisualLayer.UI.PlayerUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerManager:MonoBehaviour
    {
        #region Factory
        public class PlayerManagerFactory:PlaceholderFactory<PlayerManager>
        {

        }                
        #endregion

        #region Injects
        [Inject]
        private PlayerSettings _playerSettings;
      
        [Inject]
        private IGroundMovement _groundMovement;

        [Inject]
        private IHealthComponent _healthComponent;

        [Inject]
        private JumpingComponent _jumpingComponent;

        [Inject]
        private SlidingComponent _slidingComponent;

        [Inject]
        private Rigidbody2D _rb;

        [Inject]
        private PlayerAnimationComponent _playerAnimation;

        [Inject]
        private PlayerPowerUpUi _playerPowerUpui;

        [Inject]
        private PlayerPowerUpComponent _playerPowerUpcomponent;       

        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private PlayerMovementUi _playerMovementUi;

        [Inject]
        private GumMovementComponent _gumMovement;

        [Inject]
        private LevelManager _levelManager;

        [Inject]
        private PlayersCameraController _playersCameraController;

        [Inject]
        private Canvas _canvas;

        [Inject]
        private Camera _camera;

        [Inject]
        private List<PlayersClipInfo> _clipsInfos;


        #endregion

        #region Fields

        private AudioSource _audioSource;

        private PlayerData _playerData;

        private bool _active;

        private bool _haveGum;

        private bool _haveShield;

        private bool _grounded;

        private bool _invinsible;

        private bool _finished;

        private bool _inFallZone;

        private bool _animatingSad;

        private bool recentlySpring;

        #endregion

        #region Properties

        public PlayerPowerUpComponent MyPowerUpComponent =>_playerPowerUpcomponent;

        public AudioSource AudioSource => _audioSource;

        public bool Active { get => _active; set => _active = value; }

        public bool HaveGum { get => _haveGum; set => _haveGum = value; }
        public bool HaveShield { get => _haveShield; set => _haveShield = value; }

        #endregion

        #region Methods

        #region Mono
        public async UniTask Initialize(PlayerData data)
        {
            _audioSource=GetComponent<AudioSource>();
            _canvas.worldCamera = _camera;
            _playerData = data;
             _playerAnimation.SetCharacter(_playerData.CharacterIndex);           
            _playerPowerUpcomponent.InitializeData(_playerData);
            _playerPowerUpcomponent.OnExtraPlankUsed -= OnExtraPlanksUsed;
            _playerPowerUpcomponent.OnExtraPlankUsed += OnExtraPlanksUsed;
            _playerPowerUpcomponent.OnExtraGumUsed -= OnExtraGumUsed;
            _playerPowerUpcomponent.OnExtraGumUsed += OnExtraGumUsed;
            _playerPowerUpcomponent.OnExtraShieldUsed -= OnExtraShieldUsed;
            _playerPowerUpcomponent.OnExtraShieldUsed += OnExtraShieldUsed;
            _healthComponent.Initialize(_playerSettings.StartHealth+_playerData.Hearts);
            _healthComponent.OnDead -= OnDead;
            _healthComponent.OnDead += OnDead;
            _healthComponent.OnExtraHealthUsed -= OnExtraHealthUsed;
            _healthComponent.OnExtraHealthUsed += OnExtraHealthUsed;
            _signalBus.Subscribe<OnDamagingPlayer>(TakeDamage);
            _signalBus.Subscribe<OnSpring>(Spring);
            _signalBus.Subscribe<OnSpeedUp>(OnSpeedUpHandler);
            _signalBus.Subscribe<OnImmedietKill>(OnImmedietKillHandler);
            _playerMovementUi.OnDownClickDown -= Slide;
            _playerMovementUi.OnDownClickDown += Slide;
            _playerMovementUi.OnUpClickDown -= Jump;
            _playerMovementUi.OnUpClickDown += Jump;
            _playerMovementUi.OnDownClickUp -= StopSlide;
            _playerMovementUi.OnDownClickUp += StopSlide;
            _playersCameraController.SetPlayer(this);
            _signalBus.Subscribe<OnFallZoneChange>(SetInFallZone);
            
            while(!_playerAnimation.CharcterSet)
            {
                await UniTask.Yield();
            }

        }

        public void OnExtraHealthUsed()
        {
            _playerData.RemoveHeart(1);
            Debug.Log("ExtraHealthUsed");
        }
        public void OnExtraShieldUsed()
        {
            _playerData.RemoveShield(1);
            Debug.Log("ExtraShieldUsed");


        }
        public void OnExtraPlanksUsed()
        {
            _playerData.RemovePlank(1);
            Debug.Log("ExtraPlanksUsed");


        }
        public void OnExtraGumUsed()
        {
            _playerData.RemoveGum(1);
            Debug.Log("ExtraGumUsed");


        }

        public async void OnImmedietKillHandler()
        {
            if (_haveShield || !_active || _invinsible)
            {
                return;
            }
            _playerAnimation.AnimateDamaged(0.5f);
            await UniTask.Delay(550);
            _healthComponent.ImmedietKill();
            _levelManager.OnPlayerDied(true);
        }

        public async void OnSpeedUpHandler(OnSpeedUp args)
        {
            var time = 0f;
            while(time<1.7f)
            {

                _rb.velocity = new Vector2(args.Speed, _rb.velocity.y);                             
                time += Time.deltaTime;
                await UniTask.Yield();
            }
        }

        private void Update()
        {
            if (_animatingSad)
            {
                return;
            }
            if (_finished)
            {
                _rb.velocity = Vector2.Lerp(_rb.velocity,Vector2.zero,Time.deltaTime*2);
                return;
            }
            
            if (!Active)
            {
               
                _finished = false;
                _playerAnimation.AnimateIdle();
                _rb.gravityScale = 0f;
                _rb.velocity = Vector2.zero;
                return;
            }
            if (_haveGum)
            {
                _gumMovement.SetActive(true);
                if (_rb.velocity.x < _playerSettings.PullForceToRight)
                {
                    _rb.AddForce(new Vector2(_playerSettings.PullForceToRight, 0), ForceMode2D.Force);
                }
                return;
            }
            else
            {
                _gumMovement.SetActive(false);
            }
            _grounded = Physics2D.OverlapCircle(_rb.position + _playerSettings.OverlapOffset, _playerSettings.GroundRadius, _playerSettings.GroundLayer) == null ? false : true;
            if (_grounded)
            {
                _rb.gravityScale = 0f;
                if (!_slidingComponent.Sliding&&!_jumpingComponent.RecentlyJumped)
                {
                    _playerAnimation.AnimateRun();
                }
            }
            else if(!_inFallZone)
            {
                _rb.gravityScale = 1;                

            }
            if (!_inFallZone)
            {
                if (_rb.velocity.x < _playerSettings.PullForceToRight)
                {
                    _rb.AddForce(new Vector2(_playerSettings.PullForceToRight, 0), ForceMode2D.Force);

                }
            }

           
            if (_jumpingComponent.LastJump&&!recentlySpring)
            {
                _playersCameraController.SlowYAxis = true;
            }else
            {
                _playersCameraController.SlowYAxis = false;
            }

        }
        public void SetInFallZone(OnFallZoneChange args)
        {
            _inFallZone = args.InFallZone;
        }
        public async void SetInvinsible(int timeMilSec)
        {
            if(_invinsible)
            {
                return;
            }
            _invinsible = true;
            await UniTask.Delay(timeMilSec);
            _invinsible = false;
        }
      
        private void FixedUpdate()
        {
            if (!_active||HaveGum||_jumpingComponent.RecentlyJumped)
            {
                return;
            }     
            
            _groundMovement.Move(1);          
            if(_inFallZone )
            {
                _rb.gravityScale = 1.4f;
            }
        }
        #endregion
        public void TakeDamage(OnDamagingPlayer args)
        {

            if(_haveShield||!_active|| _invinsible)
            {
                return;
            }
            _healthComponent.TakeDamage(args.Amount);
            _playerAnimation.AnimateDamaged(1f);
           
        }
        public void AddLife()
        {
            _healthComponent.AddHealth(1);
        }
        public void OnDead()
        {
            _levelManager.OnPlayerDied(false);
        }
        public void Jump()
        {
            if (!_active||_haveGum)
            {
                return;
            }
            _jumpingComponent.Jump();
            if(_jumpingComponent.RecentlyJumped)
            {
                _playerAnimation.AnimateJump();
            }
        }
        private async void Spring(OnSpring args)
        {
            if (!_active||_grounded)
            {
                return;
            }
            recentlySpring = true;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(((Vector2.up )+(Vector2.right*0.2f))*args.SpringForce, ForceMode2D.Impulse);
            PlayClip(PlayersClipInfo.PlayersClipType.spring);
            while(!_grounded)
            {
                await UniTask.Yield();
            }
            recentlySpring = false;
        }
        public void Slide()
        {
            if (!_active||_haveGum)
            {
                return;
            }
            _slidingComponent.Slide();
            if(_slidingComponent.Sliding)
            {
                _playerAnimation.AnimateSlide();
            }

        }
        public void PlayClip(PlayersClipInfo.PlayersClipType type)
        {
            for (int i = 0; i < _clipsInfos.Count; i++)
            {
                if (type == _clipsInfos[i].MyType)
                {
                    _audioSource.PlayOneShot(_clipsInfos[i].Clip);
                    Debug.Log(type);
                    break;
                }
            }               
        }
        public void StopSlide()
        {
            if (!_active || _haveGum)
            {
                return;
            }
            if (_slidingComponent.Sliding)
            {
                _playerAnimation.AnimateEndSlide();
                _slidingComponent.StopSliding();
            }
        }
        public void Powerup(GameItemData powerUp)
        {
            if (!_active)
            {
                return;
            }
            _playerPowerUpcomponent.PowerUp(powerUp);
        }
        public async UniTask Win()
        {
            _finished = true;
            _playerAnimation.AnimateWin();
            await UniTask.Delay(6000);

        }
        public async UniTask Lose()
        {
            _finished = true;
            _playerAnimation.AnimateLose();
            await UniTask.Delay(6000);

        }
        public async UniTask AnimateSad()
        {
            var startGravity = _rb.gravityScale;
            _rb.gravityScale = 0f;
            _rb.velocity = Vector2.zero;
            _animatingSad = true;
            _playerAnimation.AnimateLose();
            await UniTask.Delay(2000);
            _rb.gravityScale = startGravity;
            _animatingSad = false;

        }

        public void OnReachedFinishLine()
        {
            _playersCameraController.StopFollow();
        }       


        #endregion
    }
    [Serializable]
    public struct PlayersClipInfo
    {
        public enum PlayersClipType { slide, power, gum,endgum,shield,endShield,spring,jump,collect,
            plank
        }
        public PlayersClipType MyType;
        public AudioClip Clip;
    }
}
