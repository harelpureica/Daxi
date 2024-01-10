
using Daxi.InfrastructureLayer.Popups.Missions;
using Zenject;
using Daxi.DataLayer.MissionsData;
using Cysharp.Threading.Tasks;
using Daxi.VisualLayer.Player;
using UnityEngine;
using Daxi.VisualLayer.UI.CountDown;
using Daxi.InfrastructureLayer.Popups.Pause;
using Daxi.InfrastructureLayer.ScenesManagment;
using UnityEngine.SceneManagement;
using Daxi.InfrastructureLayer.MonoUtils;
using Daxi.InfrastructureLayer.Loading;
using Daxi.InfrastructureLayer.Popups.EndLevel;
using Daxi.DataLayer.LevelsData;
using Daxi.VisualLayer.Missions;
using System;
using Daxi.DataLayer.Player;
using Object = UnityEngine.Object;
using Daxi.InfrastructureLayer.Popups.AdForLife;
using System.Threading.Tasks;
using Daxi.InfrastructureLayer.Popups.OutOfhearts;
using static Cinemachine.DocumentationSortingAttribute;
using Daxi.VisualLayer.UI.Missions;
using UnityEngine.Rendering.Universal;
using Daxi.VisualLayer.PostProcessing;
using Daxi.InfrastructureLayer.Audio;
using System.Collections.Generic;
using Daxi.InfrastructureLayer.Ads;
using GoogleMobileAds.Api;

namespace Daxi.VisualLayer.Levels
{
    public class LevelManager : MonoBehaviour
    {
        #region Injects

        [Inject]
        private ILoadingScreen _loadingScreen;
        [Inject]
        private MissionPopup.MissionPopupFactory _missionPopupFactory;

        [Inject]
        private PauseLevelPopup.PauseLevelPopupFactory  _pausePopupFactory;

        [Inject]
        private MissionData _missionData;

        [Inject]
        private PlayerManager.PlayerManagerFactory _playerManagerFactory;

        [Inject]
        private CountDownComponent _countdownComponent;

        [Inject]
        private IScenesLoader _scenesLoader;

        [Inject]
        private MonoService _monoService;

        [Inject]
        private EndLevelPopup.EndLevelPopupFactory _endLevelPopupFactory;

        [Inject]
        private LevelData _levelData;

        [Inject(Id ="Production")]
        private bool _production;

        [Inject]
        private MissionsTracker _missionsTracker;

        [Inject]
        private MissionsTrackerUi _missionsTrackerUi;

        [Inject]
        private PlayerData _playerData;

        [Inject]
        private AdsManager _adsManager;

        [Inject]
        private AdForLifePopup.AdForLifePopupFactory adForLifePopupFactory;

        [Inject]
        private OutOfHeartsPopup.OutOfHeartsPopupFactory outOfHeartsPopupFactory;

        [Inject]
        private VolumeController _volumeController;

        [Inject(Id ="Win")]
        private AudioClip winLevelClip;

        [Inject(Id = "Lose")]
        private AudioClip loseLevelClip;

        [Inject (Id ="LevelManager")]
        private AudioSource _audioSource;

        [Inject]
        private List<LevelData> _levels;

        private bool adForLifePopupSeen;

        private bool _rewarded;



        #endregion


        #region Fields


        private PlayerManager _playerManager;

        private bool _playerDead;


        public enum LevelState { none, run,pause,ended}

        private LevelState _levelState;

        private UniversalRenderPipelineAsset m_cachedRenderPipeline;
        #endregion

        #region Properties
        public PlayerManager MyPlayerManager => _playerManager;

        public LevelState MyState => _levelState;

        #endregion


        #region Properties
        UniversalRenderPipelineAsset CachedRenderPipeline
        {
            get
            {
                if (m_cachedRenderPipeline == null)
                    m_cachedRenderPipeline = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;

                return m_cachedRenderPipeline;
            }
        }

        public bool PlayerDead => _playerDead; 
        #endregion

        #region Methods
        public async UniTask  InitializePlayer()
        {
            _playerManager = _playerManagerFactory.Create();
            await _playerManager.Initialize(_playerData);
        }
        public async void StartLevel()
        {
            if(MusicPlayer.IsPlaying)
            {
                await MusicPlayer.Instance.Stop();
            }
            _audioSource.volume = 0.4f;
            var missionPopup = _missionPopupFactory.Create();
            missionPopup.SetData(_missionData);
            missionPopup.Open();
            _volumeController.SetDof(true);
            _playerManager.Active = false;
           
            while (!missionPopup.PlayerClickedClose)
            {
                await UniTask.Yield();
            }
            missionPopup.Close();
            _volumeController.SetDof(false);
            await UniTask.Delay(500);
            _monoService.DestroyObject(missionPopup.gameObject);
            await _countdownComponent.TextCountDown();
            if(MusicPlayer.Instance!=null)
            {
                MusicPlayer.Instance.PlayWorlds();

            }
            _playerManager.Active = true;
            _levelState = LevelState.run;
            _missionsTrackerUi.SubscribeToButton(OnMissionButtonPressed);
        }

        private async void OnMissionButtonPressed()
        {
            if(MyState!=LevelState.run)
            {
                return;
            }
            _levelState = LevelState.pause;
            _playerManager.Active = false;
            var missionPopup = _missionPopupFactory.Create();
            missionPopup.SetData(_missionData);
            missionPopup.Open();
            _volumeController.SetDof(true);
            _playerManager.Active = false;

            while (!missionPopup.PlayerClickedClose)
            {
                await UniTask.Yield();
            }
            missionPopup.Close();
            _volumeController.SetDof(false);
            await UniTask.Delay(500);
            _monoService.DestroyObject(missionPopup.gameObject);
            await _countdownComponent.NumbersCountDown();
            _playerManager.Active = true;
            _levelState = LevelState.run;
        }

        public async void PauseLevel()
        {
            if (_levelState == LevelState.pause)
            {
                return;
            }
            if(MusicPlayer.Instance!=null)
            {
                MusicPlayer.Instance.Pause();
            }
            _levelState = LevelState.pause;
            _playerManager.Active = false;
            var popup = _pausePopupFactory.Create();            
            popup.Open();
            _volumeController.SetDof(true);
            Time.timeScale = 0f;
            CachedRenderPipeline.renderScale = 0.3f;
            while (!popup.PlayerClickedClose&&!popup.LevelButtonPressed&&!popup.restartButtonPressed)
            {                
                await UniTask.Yield();
            }
            Time.timeScale = 1f;
            CachedRenderPipeline.renderScale = 1f;
            popup.Close();
            _volumeController.SetDof(false);
            if (popup.PlayerClickedClose)
            {
                await _countdownComponent.NumbersCountDown();
                if(MusicPlayer.Instance!=null)
                {
                    MusicPlayer.Instance.Resume();
                }
                _levelState = LevelState.run;
                _playerManager.Active = true;
                _playerManager.SetInvinsible(2500);
                await UniTask.Delay(500);
                _monoService.DestroyObject(popup.gameObject);

            }
            if (popup.LevelButtonPressed)
            {
                await _scenesLoader.LoadSceneAsync(_levelData.LevelSelctionSceneName);
                
            }
            if (popup.restartButtonPressed)
            {
               ResetLevel();
            }
            
            


        }
        public async UniTask LoadSceneWithLoadingScreenAsync(string sceneToLoad, string sceneToUnload)
        {

            _loadingScreen.Show();
            await _scenesLoader.UnloadSceneAsync(sceneToUnload);
            var lerp = 0f;
            while (lerp < 0.9f)
            {
                _loadingScreen.UpdateProgress(lerp);
                lerp += (Time.deltaTime / 4);
                await UniTask.Yield();
            }           
            await _scenesLoader.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
            await UniTask.Delay(500);
            _loadingScreen.Hide();


        }
        private void OnApplicationQuit()
        {
            CachedRenderPipeline.renderScale = 1f;

        }
        public async UniTask LoadLevelSceneWithLoadingScreenAsync(string levelToLoad, string levelToUnload)
        {
            
            _loadingScreen.Show();
            await _scenesLoader.UnloadSceneAsync(levelToUnload);
            var lerp = 0f;
            while (lerp < 0.9f)
            {
                _loadingScreen.UpdateProgress(lerp);
                lerp += (Time.deltaTime / 4);
                await UniTask.Yield();
            }
            await _scenesLoader.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);
            _loadingScreen.UpdateProgress(0.95f);
            _loadingScreen.UpdateProgress(1);
            _loadingScreen.Hide();
            var levelManager = Object.FindObjectOfType<LevelManager>();
            if(levelManager != null)
            {
                await levelManager.InitializePlayer();
                levelManager.StartLevel();

            }
          
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelToLoad));


        }
        public void OnReachedFinishLine()
        {
            _playerManager.OnReachedFinishLine();
            EndLevel(true);
        }       
       
        public async void OnPlayerDied(bool immediet)
        {
            if (MusicPlayer.Instance != null)
            {
                 MusicPlayer.Instance.Pause();
            }
            _playerDead = true;
            if (immediet)
            {
                await PlayerStopRoutine(false);
                EndLevelPopupRoutine(false, false);
                return;
            }
            if (adForLifePopupSeen)
            {
                _playerManager.Active = false;
                await _playerManager.AnimateSad();
                await OutOfHeartsPopupRoutine(true);                
                return;
            }
           
            adForLifePopupSeen = true;
            _playerManager.Active = false;
            await _playerManager.AnimateSad();
            var popup = adForLifePopupFactory.Create();
            popup.Open();
            _volumeController.SetDof(true);
            while (!popup.PlayerClickedClose && !popup.PlayerWantsAd)
            {
                await UniTask.Yield();
            }
            popup.Close();
            _volumeController.SetDof(false);
            if (popup.PlayerClickedClose)
            {

                
                if (immediet)
                {
                    EndLevelPopupRoutine(false,false);
                }
                else
                {
                    await OutOfHeartsPopupRoutine(false);
                }
            }
            else
            {
               
                _adsManager.Load_rewardedAd( (isEarned) => 
                {
                    HandleReward(isEarned);
                });
                while (!_adsManager.RewardedAdLoaded)
                {
                    await UniTask.Yield();
                }
                _adsManager.Show_rewardedAd();
                while (_adsManager.RewardedAdLoaded)
                {
                    await UniTask.Yield();
                }
               


            }

        }
        public async void HandleReward(bool isEarned)
        {

            if (isEarned)
            {

                if (_production)
                {
                    _playerManager.AddLife();
                    await _countdownComponent.NumbersCountDown();
                    if (MusicPlayer.Instance != null)
                    {
                        MusicPlayer.Instance.Resume();
                    }
                    _playerManager.SetInvinsible(2500);
                    _playerManager.Active = true;
                    _playerDead = false;
                }
            }
            else
            {

                if (!_production)
                {
                    _playerManager.AddLife();
                    await _countdownComponent.NumbersCountDown();
                    if (MusicPlayer.Instance != null)
                    {
                        MusicPlayer.Instance.Resume();
                    }
                    _playerManager.SetInvinsible(2500);
                    _playerManager.Active = true;
                    _playerDead = false;
                }
            }
        }
        private async UniTask OutOfHeartsPopupRoutine(bool watchAd)
        {
            
            var OutOfHeartsPopup = outOfHeartsPopupFactory.Create();
            OutOfHeartsPopup.Open();
            _volumeController.SetDof(true);

            while (!OutOfHeartsPopup.PlayerClickedClose && !OutOfHeartsPopup.playerClickedHome)
            {
                await UniTask.Yield();
            }
            OutOfHeartsPopup.Close();
            _volumeController.SetDof(false);
            await UniTask.Delay(500);
            if(watchAd)
            {
                _adsManager.LoadInterstitialAd();
                while (!_adsManager.InterstitialAdLoaded)
                {
                    await UniTask.Yield();
                }
                _adsManager.ShowInterstitialAd();
                while (_adsManager.InterstitialAdLoaded)
                {
                    await UniTask.Yield();
                }
            }
            if (OutOfHeartsPopup.playerClickedHome)
            {
                await _scenesLoader.LoadSceneAsync(ScenesNames.Menu);
            }
            else
            {
                ResetLevel();
            }
        }
        private async UniTask PlayerStopRoutine(bool win )
        {

            _playerManager.Active = false;
            if (win)

            {
               
                _audioSource.PlayOneShot(winLevelClip);
                await _playerManager.Win();                
            }
            else
            {
                _audioSource.PlayOneShot(loseLevelClip);
                await _playerManager.Lose();
            }
        }
        private async void EndLevel(bool completedTrack)
        {
            if (_levelState == LevelState.ended)
            {
                return;
            }
           
            _levelState = LevelState.ended;
          
             var levelPassed = false;
            if(completedTrack && _missionsTracker.Completed)
            {
                levelPassed = true;
                _playerData.UnlockLevel();                
                for (int i = 0; i < _levels.Count; i++)
                {
                    if (_levelData.NextlevelSceneName == _levels[i].SceneName)
                    {
                        _levels[i].Locked = false; 
                        break;

                    }
                }
            }
            if (MusicPlayer.Instance != null)
            {
                MusicPlayer.Instance.Stop();
            }
            if(levelPassed&& _levelData.SceneName=="WorldThreeLevelSix")
            {
                LoadLevelSceneWithLoadingScreenAsync(_levelData.NextlevelSceneName, _levelData.SceneName);
                return;
            }
            await PlayerStopRoutine(levelPassed);
            EndLevelPopupRoutine(levelPassed,true);

        }
        private async  void EndLevelPopupRoutine(bool levelPassed,bool watchAd)
        {
            
            var popup = _endLevelPopupFactory.Create();
            popup.SetData(_missionData, levelPassed, _missionsTracker.ItemsRequirmentPassed);
            popup.Open();
            _volumeController.SetDof(true);
            while (!popup.PlayerClickedClose && !popup.PlayerClickedHomeButton && !popup.PlayerClickedRedoButton)
            {
                await UniTask.Yield();
            }
            popup.Close();
            _volumeController.SetDof(false);
            await UniTask.Delay(500);   
            if(watchAd)
            {
                _adsManager.LoadInterstitialAd();
                while (!_adsManager.InterstitialAdLoaded)
                {
                    await UniTask.Yield();
                }
                _adsManager.ShowInterstitialAd();
                while (_adsManager.InterstitialAdLoaded)
                {
                    await UniTask.Yield();
                }
            }
            if (popup.PlayerClickedHomeButton)
            {
                await _scenesLoader.LoadSceneAsync(ScenesNames.Menu);

            }
            else if (popup.PlayerClickedRedoButton)
            {
                ResetLevel();
            }
            else
            {
                if (levelPassed)
                {
                    LoadLevelSceneWithLoadingScreenAsync(_levelData.NextlevelSceneName, _levelData.SceneName);

                }
                else
                {
                    ResetLevel();

                }
            }
        }

        private void ResetLevel()
        {
            LoadLevelSceneWithLoadingScreenAsync(_levelData.SceneName, _levelData.SceneName);
        }
        #endregion
    }
}
