
using Daxi.InfrastructureLayer.Audio;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.SettingsPopup
{
    public class SettingsPopup:PopupBase
    {
        private const string CheatCode = "St9095896!";
        #region Factory
        public class SettingsPopupFactory:PlaceholderFactory<SettingsPopup>
        {

        }
        #endregion
        #region Injects
        [Inject]
        private AudioSettingsController _audioSettings;
        #endregion

        #region Fields
        [SerializeField]
        private Button _ContactButton;

        [SerializeField]
        private Button _feedBackButton;

        [SerializeField]
        private Button _TermsButton;

        [SerializeField]
        private Button _musicOnButton;

        [SerializeField]
        private Button _sfxOnButton;

        [SerializeField]
        private Button _musicOffButton;

        [SerializeField]
        private Button _sfxOffButton;

        [SerializeField]
        private Button _cheatButton;

        [SerializeField]
        private Button _cheatInputConfirmBtn;

        [SerializeField]
        private TMP_InputField _cheatInput;


        private bool _contactButtonClicked;

        private bool _feedBackButtonClicked;

        private bool _termsButtonClicked;

        private bool _confirmedCheat;

        private float timeWithFiveFingers;
         

        #endregion

        #region Properties
        public bool ContactButtonClicked => _contactButtonClicked;
        public bool FeedBackButtonClicked => _feedBackButtonClicked;
        public bool TermsButtonClicked => _termsButtonClicked;

        public bool ConfirmedCheat  => _confirmedCheat; 


        #endregion

        #region Methods
        protected override void Start()
        {
            timeWithFiveFingers = 0f;
            base.Start();
            _cheatButton.gameObject.SetActive(false);
            _confirmedCheat = false;
            _cheatInputConfirmBtn.gameObject.SetActive(false);
            _cheatInput.gameObject.SetActive(false);
            _ContactButton.onClick.AddListener(OnContactClick);
            _feedBackButton.onClick.AddListener(OnFeedbackClick);
            _TermsButton.onClick.AddListener(OnTermsClick);
            _cheatButton.onClick.AddListener(OnCheatClick);
            _cheatInputConfirmBtn.onClick.AddListener(OnCheatConfirmedClick);
            _musicOnButton.onClick.AddListener(MusicOn);
            _musicOffButton.onClick.AddListener(MusicOff);
            _sfxOnButton.onClick.AddListener(SfxOn);
            _sfxOffButton.onClick.AddListener(SfxOff);
            if(_audioSettings.GetVolume(AudioSettingsController.MixerType.Sfx)>0)
            {                
                _sfxOnButton.gameObject.SetActive(false);
                _sfxOffButton.gameObject.SetActive(true);
            }
            else
            {
                _sfxOnButton.gameObject.SetActive(true);
                _sfxOffButton.gameObject.SetActive(false);
            }
            if (_audioSettings.GetVolume(AudioSettingsController.MixerType.Music) > 0)
            {
                _musicOnButton.gameObject.SetActive(false);
                _musicOffButton.gameObject.SetActive(true);

            }
            else
            {
                _musicOnButton.gameObject.SetActive(true);
                _musicOffButton.gameObject.SetActive(false);
            }
        }
        private void Update()
        {
            if(SystemInfo.deviceType==DeviceType.Handheld)
            {
                if(Input.touchCount==5)
                {
                    timeWithFiveFingers += Time.deltaTime;
                    if(timeWithFiveFingers >4)
                    {
                        _cheatButton.gameObject.SetActive(true);
                    }
                }
               

            }
            else
            {
                if (!_cheatButton.gameObject.activeInHierarchy)
                {
                    _cheatButton.gameObject.SetActive(true);

                }

            }
        }
        public void OnCheatConfirmedClick()
        {
            if (_cheatInput.text == CheatCode)
            {
                _confirmedCheat = true;
            }
        }
        public void OnCheatClick()
        {
            _cheatButton.gameObject.SetActive(false);
            _cheatInputConfirmBtn.gameObject.SetActive(true);
            _cheatInput.gameObject.SetActive(true);
        }
        private void SfxOn()
        {
            _audioSettings.SetMixerVolume(AudioSettingsController.MixerType.Sfx, 0.71f);
            _sfxOffButton.gameObject.SetActive(true);
            _sfxOnButton.gameObject.SetActive(false);
        }

        private void SfxOff()
        {
            _audioSettings.SetMixerVolume(AudioSettingsController.MixerType.Sfx, 0f);
            _sfxOffButton.gameObject.SetActive(false);
            _sfxOnButton.gameObject.SetActive(true);
        }

        private void MusicOff()
        {
            _audioSettings.SetMixerVolume(AudioSettingsController.MixerType.Music, 0f);
            _musicOffButton.gameObject.SetActive(false);
            _musicOnButton.gameObject.SetActive(true);
        }

        private void MusicOn()
        {
            _audioSettings.SetMixerVolume(AudioSettingsController.MixerType.Music, 0.7f);
            _musicOffButton.gameObject.SetActive(true);
            _musicOnButton.gameObject.SetActive(false);
        }

        private void OnTermsClick()
        {
            _termsButtonClicked = true;
        }

        private void OnFeedbackClick()
        {
            _feedBackButtonClicked = true;
        }

        private void OnContactClick()
        {
            _contactButtonClicked = true;
        }


        #endregion
    }
}
