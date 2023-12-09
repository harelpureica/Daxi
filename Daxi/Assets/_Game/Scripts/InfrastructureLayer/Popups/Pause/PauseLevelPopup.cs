using Daxi.InfrastructureLayer.Audio;
using Daxi.InfrastructureLayer.Audio.Installers;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Pause
{
    public class PauseLevelPopup:PopupBase
    {
        #region Factory
        public class PauseLevelPopupFactory:PlaceholderFactory<PauseLevelPopup>
        {
            
        }
        #endregion

        #region Injects
        [Inject]
        private AudioSettingsController _controller;
        #endregion

        #region Fields
        [SerializeField]
        private Button _restartButton;      

        [SerializeField]
        private Button _levelsButton;

        [SerializeField]
        private Button _XButton;

        [SerializeField]
        private Button _toggleMusic;

        [SerializeField]
        private Button _toggleSfx;

        [SerializeField]
        private Sprite _musicOn;

        [SerializeField]
        private Sprite _musicOf;

        [SerializeField]
        private Sprite _sfxOn;

        [SerializeField]
        private Sprite _sfxOf;

        private bool _restartButtonPressed;

        private bool _levelButtonPressed;

        #endregion

        #region Properties
        public bool restartButtonPressed => _restartButtonPressed;

        public bool LevelButtonPressed => _levelButtonPressed;
        #endregion

        #region Methods
        protected override void Start()
        {
            base.Start();
            _restartButton.onClick.AddListener(OnRestartClick);
            _XButton.onClick.AddListener(OnCloseClick);
            _levelsButton.onClick.AddListener(OnLevelsButtonClick);
            _toggleSfx.onClick.AddListener(()=>ToggleMixer(AudioSettingsController.MixerType.Sfx));
            _toggleMusic.onClick.AddListener(() => ToggleMixer(AudioSettingsController.MixerType.Music));
            if (_controller.GetVolume(AudioSettingsController.MixerType.Music) > 0)
            {
                UpdateButton(AudioSettingsController.MixerType.Music, true);

            }
            else
            {
                UpdateButton(AudioSettingsController.MixerType.Music, false);
            }

            if (_controller.GetVolume(AudioSettingsController.MixerType.Sfx) > 0)
            {
                UpdateButton(AudioSettingsController.MixerType.Sfx, true);

            }
            else
            {
                UpdateButton(AudioSettingsController.MixerType.Sfx, false);
            }

           
        }

        private void ToggleMixer(AudioSettingsController.MixerType mixer )
        {
            
            

            if(_controller.GetVolume(mixer) >0)
            {
                _controller.SetMixerVolume(mixer, 0);
                UpdateButton(mixer, false);

            }
            else
            {
                _controller.SetMixerVolume(mixer, 0.8f);
                UpdateButton(mixer, true);

            }
        }
        private void UpdateButton(AudioSettingsController.MixerType mixer,bool On)
        {
            if (mixer == AudioSettingsController.MixerType.Sfx)
            {
                if(On)
                {
                    _toggleSfx.image.sprite = _sfxOn;
                }
                else
                {
                    _toggleSfx.image.sprite = _sfxOf;
                }
            }
            else
            {
                if (On)
                {
                    _toggleMusic.image.sprite= _musicOn;
                }
                else
                {
                    _toggleMusic.image.sprite = _musicOf;

                }
            }
        }
        
        public void OnLevelsButtonClick()
        {
            _levelsButton.interactable = false;
            _levelButtonPressed = true;
        }

        public void OnRestartClick()
        {
            _restartButton.interactable = false;
            _restartButtonPressed = true;
        }

        #endregion
    }
}
