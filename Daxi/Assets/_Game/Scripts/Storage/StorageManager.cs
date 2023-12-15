using Cysharp.Threading.Tasks;
using Daxi.DataLayer.LevelsData;
using Daxi.DataLayer.Player;
using Daxi.VisualLayer.Player.PowerUps;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.Storage
{
    public class StorageManager:IInitializable
    {
        #region Injects
        [Inject]
        private PlayerData _data;

        [Inject]
        private List<LevelData> _levels;
        #endregion

        #region Fields
        private const  string FileName = "DaxiSavedGame";

        private bool _save;

        private bool _proccesing;

        private bool _isLoadedOrFailed;

        #endregion


        #region Methods
        public void Initialize()
        {
            _data.OnDataChenged -= OnDataChange;
            _data.OnDataChenged += OnDataChange;

        }

        public void OnDataChange()
        {
            Save();
        }

        public void Save()
        {
           
            if (_proccesing|| SystemInfo.deviceType != DeviceType.Handheld)
            {
                Debug.Log("eror saving");
                return;
            }
            _save = true;
            _proccesing = true;
            OpenSavedGame();
        }
        public async UniTask Load()
        {

            if (_proccesing||SystemInfo.deviceType!=DeviceType.Handheld)
            {
                Debug.Log("eror Loading");
                return ;
            }
            if(_isLoadedOrFailed)
            {
                Debug.Log("eror loading");
                return;
            }
            if (!PlayerPrefs.HasKey(FileName))
            {
                var data = $"{0}|{1}|{0}|{0}|{0}|{0}|{0}|{0}|{0}";
                _data.SetStringData(data);                
                PlayerPrefs.SetString(FileName, "1");
                PlayerPrefs.Save();
                _isLoadedOrFailed = true;
                return ;
            }
            _save = false;
            _proccesing = true;
            OpenSavedGame();
            while( !_isLoadedOrFailed ) 
            { 
                await UniTask.Yield();
            }
            
        }
        private void OpenSavedGame()
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(FileName, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        }

        public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                if (_save)
                {
                    SaveGame(game, System.Text.ASCIIEncoding.ASCII.GetBytes(_data.GetStringData()));
                }
                else
                {
                    LoadGame(game);

                }
            }
            else
            {
                Debug.Log("eror while opening saved games");
                _proccesing = false;
            }
        }              

        private void SaveGame(ISavedGameMetadata game, byte[] savedData)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
            builder = builder
                .WithUpdatedDescription("Saved game at " + DateTime.Now);
          
            SavedGameMetadataUpdate updatedMetadata = builder.Build();

            savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSaveData);
        }
        private void LoadGame(ISavedGameMetadata game)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.ReadBinaryData(game, OnLoadData);
        }

        private void OnLoadData(SavedGameRequestStatus arg1, byte[] arg2)
        {
            if(arg1 == SavedGameRequestStatus.Success)
            {
                var data = System.Text.ASCIIEncoding.ASCII.GetString(arg2);
                _data.SetStringData(data);
                for (int i = 0; i < _levels.Count; i++)
                {
                    if(i<_data.UnlockedLevels)
                    {
                        _levels[i].Locked = false;
                    }
                    else
                    {
                        _levels[i].Locked = true;
                    }
                }
                Debug.Log("gameLoded");
                

            }
            else
            {
                Debug.Log("eror game data not loaded");

            }
            _isLoadedOrFailed = true;
            _proccesing = false;
        }

        public void OnSaveData(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                Debug.Log("game data Saved");
            }
            else
            {
                Debug.Log("eror game data not Saved");
            }
            _proccesing = false;

        }

        
        #endregion

    }
}
