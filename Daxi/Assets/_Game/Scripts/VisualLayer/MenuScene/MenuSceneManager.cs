
using Cysharp.Threading.Tasks;
using Daxi.DataLayer.LevelsData;
using Daxi.DataLayer.Player;
using Daxi.InfrastructureLayer.Audio;
using Daxi.InfrastructureLayer.Loading;
using Daxi.InfrastructureLayer.MonoUtils;
using Daxi.InfrastructureLayer.Popups.ConfirmedTerms;
using Daxi.InfrastructureLayer.Popups.DataSetter;
using Daxi.InfrastructureLayer.Popups.SettingsPopup;
using Daxi.InfrastructureLayer.Popups.Terms;
using Daxi.InfrastructureLayer.Popups.VerifyAge;
using Daxi.InfrastructureLayer.ScenesManagment;
using Daxi.Storage;
using Daxi.VisualLayer.PostProcessing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.MenuScene
{
    public class MenuSceneManager : IInitializable
    {

        #region Conts
        private const string PlayerAgreedToTermsPreff = "PlayerAgreedToTerms";
        #endregion

        #region Injects


        [Inject]
        private SettingsPopup.SettingsPopupFactory settingsPopupFactory;

        [Inject(Id ="Play")]
        private Button _playButton;

        [Inject(Id ="Shop")]
        private Button _shopButton;

        [Inject]
        private StorageManager _storageManager;

        [Inject]
        private VerifyAgePopup.VerifyAgePopupFactory _verifyAgePopupFactory;

        [Inject]
        private TermsPopup.TermsPopupFactory _termsPopupFactory; 

        [Inject]
        private MonoService _service;

        [Inject]
        private PlayerData _playerData;

        [Inject]
        private List<LevelData> levelsDatas;

        [Inject]
        private DataSetterPopup.DataSetterPopupFactory _dataSetterPopupFactory;

        [Inject]
        private IScenesLoader _scenesLoader;

        [Inject]
        private ILoadingScreen _loadingScreen;

        [Inject(Id = "ShareText")]
        private string _shareMessage;

        [Inject(Id = "ShareURL")]
        public string _playStoreURL;

        [Inject(Id = "ShareButton")]
        private Button _shareButton;

        [Inject]
        private VolumeController _volumeController;

        [Inject(Id = "Log")]
        private TextMeshProUGUI _logText;

        [Inject(Id = "Settings")]
        private Button _settingsBtn;

        [Inject]
        private ConfirmedTermsPopup.ConfirmedTermsPopupFactory _confirmedTermsPopupFactory;


        #endregion

        #region Methods
        public void ShareGame()
        {

            string shareSubject = "Download Daxi";

#if UNITY_ANDROID
            // Android-specific sharing intent
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), _shareMessage + "\n" + _playStoreURL);
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, shareSubject);
            currentActivity.Call("startActivity", chooser);
#endif
        }
        public void OpenGmailForContacting()
        {
            string email = "Wildgeex.support@gmail.com";  // Replace with your email address
            string subject = "Feedback for Daxi";
            string body = "Hello, I have some feedback for Daxi...";

            string mailtoLink = "mailto:" + email +
                                "?subject=" + UnityWebRequest.EscapeURL(subject) +
                                "&body=" + Uri.EscapeDataString(body);

            Application.OpenURL(mailtoLink);
        }
        public async void Initialize()
        {
            if (!MusicPlayer.IsPlaying)
            {
                MusicPlayer.Instance.PlayMenus();
            }
            await UniTask.Delay(500);
            _settingsBtn.onClick.AddListener(OnSettingsClick);
            _shareButton.onClick.RemoveAllListeners();
            _shareButton.onClick.AddListener(ShareGame);
            if(!PlayerPrefs.HasKey(PlayerAgreedToTermsPreff))
            {
                var playerClickedCloseOnTerms = false;
                while (!playerClickedCloseOnTerms)
                {
                    var verifyAgePopup = _verifyAgePopupFactory.Create();
                    verifyAgePopup.Open();
                    _volumeController.SetDof(true);

                    while (!verifyAgePopup.PlayerClickedClose)
                    {
                        await UniTask.Yield();
                    }
                    verifyAgePopup.Close();
                    _volumeController.SetDof(false);
                    _service.DestroyObject(verifyAgePopup.gameObject);
                    var termsPopup = _termsPopupFactory.Create();
                    termsPopup.Open();
                    _volumeController.SetDof(true);
                    while (!termsPopup.PlayerClickedClose && !termsPopup.PlayerClikedBack)
                    {
                        await UniTask.Yield();
                    }
                    playerClickedCloseOnTerms = termsPopup.PlayerClickedClose;
                    if(playerClickedCloseOnTerms)
                    {
                        PlayerPrefs.SetString(PlayerAgreedToTermsPreff, PlayerAgreedToTermsPreff);

                    }
                    termsPopup.Close();
                    _volumeController.SetDof(false);
                    _service.DestroyObject(termsPopup.gameObject);
                }
            }
           
           

          
            
            _shopButton.onClick.AddListener(OnShopClick);
            _playButton.onClick.AddListener(OnPlayClick);
        }
        public async UniTask CheatPopup()
        {
            var dataPopup = _dataSetterPopupFactory.Create();
            dataPopup.Open();
            _volumeController.SetDof(true);
            dataPopup.Initialize();
            while (!dataPopup.PlayerClickedClose)
            {
                await UniTask.Yield();
            }
            _playerData.SetDataFromPopup(dataPopup.CharacterIndex, dataPopup.UnlockedLevels, dataPopup.Gums, dataPopup.Planks, dataPopup.Shields, dataPopup.Hearts, dataPopup.PetIndex);
            for (int i = 0; i < levelsDatas.Count; i++)
            {
                if (i < dataPopup.UnlockedLevels)
                {
                    levelsDatas[i].Locked = false;
                }
                else
                {
                    levelsDatas[i].Locked = true;
                }
            }
            dataPopup.Close();
            _volumeController.SetDof(false);
            await UniTask.Delay(500);
            _service.DestroyObject(dataPopup.gameObject);
        }

        public async void OnSettingsClick()
        {
            _settingsBtn.interactable = false;
            var popup=settingsPopupFactory.Create();
            popup.Open();            
            while(!popup.ConfirmedCheat&&!popup.PlayerClickedClose&&!popup.ContactButtonClicked&&!popup.TermsButtonClicked&&!popup.FeedBackButtonClicked)
            {
                await UniTask.Yield();

            }  
            popup.Close();
            if (popup.TermsButtonClicked)
            {
                await ConfirmedTermsPopupRoutine();
            }
            if (popup.FeedBackButtonClicked)
            {
                Application.OpenURL(_playStoreURL);

            }
            if (popup.ContactButtonClicked)
            {
                OpenGmailForContacting();

            }
            if (popup.ConfirmedCheat)
            {
                await CheatPopup();

            }
            _service.DestroyObject(popup.gameObject);
            _settingsBtn.interactable = true;

        }

        private async UniTask ConfirmedTermsPopupRoutine()
        {
            var popup = _confirmedTermsPopupFactory.Create();
            popup.Open();
            while(!popup.PlayerClickedClose)
            {
                await UniTask.Yield();
            }
            popup.Close();
            _service.DestroyObject(popup.gameObject);
        }

        public async void OnPlayClick()
        {
            _scenesLoader.LoadSceneAsync(ScenesNames.WorldSelection);


        }

        public async void OnShopClick()
        {
           _scenesLoader.LoadSceneAsync( ScenesNames.Store);
        }
      
        
        #endregion
    }
}
