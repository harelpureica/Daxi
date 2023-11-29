using Cysharp.Threading.Tasks;
using Daxi.DataLayer.LevelsData;
using Daxi.InfrastructureLayer.Loading;
using Daxi.InfrastructureLayer.ScenesManagment;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;
using System.Linq;
using Daxi.InfrastructureLayer.Audio;

namespace Daxi.VisualLayer.LevelSelection
{
    public class LevelSelectionManager:IInitializable
    {
        #region Injects
        [Inject]
        private IScenesLoader _scenesLoader;

        [Inject]
        private ILoadingScreen _loadingScreen;

        [Inject]
        private List<LevelData> _levelDatas;

        [Inject]
        private LevelButton.Factory _levelButtonFactory;

        [Inject(Id ="LevelSelectionSceneName")]
        private string _currentSceneName;

        [Inject(Id ="Back")]
        private Button _backButton;

        private bool _levelSelected;       

        public void Initialize()
        {
           if(! MusicPlayer.IsPlaying)
           {
                MusicPlayer.Instance.PlayMenus();
           }
            _backButton.onClick.AddListener(OnBackClick);
            var startLevelIndex = 0;
            var endLevelIndex = 5;
            switch (_currentSceneName)
            {
                case ScenesNames.WorldOneLevelSelection:
                    startLevelIndex = 0;
                    endLevelIndex = 5;
                    break;
                case ScenesNames.WorldTwoLevelSelection:
                    startLevelIndex = 6;
                    endLevelIndex = 11;
                    break;
                case ScenesNames.WorldThreeLevelSelection:
                    startLevelIndex = 12;
                    endLevelIndex = 17;
                    break;
            }
            for (int i = startLevelIndex; i <= endLevelIndex; i++)
            {
                var button = _levelButtonFactory.Create();
                button.SetData(_levelDatas[i]);                                     
            }
        }

        public async void OnBackClick()
        {
            _backButton.interactable = false;
            _scenesLoader.LoadSceneAsync(ScenesNames.WorldSelection);
        }
        #endregion

        #region Methods
        public async void OnLevelSelected(LevelData data)
        {
            if (_levelSelected)
            {
                return;
            }

            _levelSelected = true;
            if (!SceneManager.GetSceneByName(ScenesNames.Popups).isLoaded)
            {
                await _scenesLoader.LoadSceneAsync(ScenesNames.Popups, LoadSceneMode.Additive);
            }
            if (!SceneManager.GetSceneByName(ScenesNames.Loading).isLoaded)
            {
                await _scenesLoader.LoadSceneAsync(ScenesNames.Loading, LoadSceneMode.Additive);
            }

            _loadingScreen.Show();
            await _scenesLoader.UnloadSceneAsync(_currentSceneName);
            var operation = _scenesLoader.LoadSceneAsyncOperation(data.SceneName, LoadSceneMode.Additive);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }
                _loadingScreen.UpdateProgress(operation.progress);
                await UniTask.Yield();
            }
            _loadingScreen.UpdateProgress(0.95f);
            var levelManager = Object.FindObjectOfType<LevelManager>();
            await levelManager.InitializePlayer();
            _loadingScreen.UpdateProgress(1);
            _loadingScreen.Hide();
            levelManager.StartLevel();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(data.SceneName));
           
        }
      
        #endregion

    }
}
