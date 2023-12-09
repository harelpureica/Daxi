using Zenject;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using Daxi.InfrastructureLayer.ScenesManagment;
using Daxi.InfrastructureLayer.Audio;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.SocialPlatforms;
using Daxi.Storage;

public class StartScreenManager : IInitializable
{
    [Inject(Id ="Start")]
    private Slider _slider;

    [Inject(Id = "Start")]
    private TextMeshProUGUI _text;

    [Inject]
    private IScenesLoader _scenesLoader;

    [Inject]
    private StorageManager _storageManager;

    
    public async void Initialize()
    {
        
        if (SystemInfo.deviceType==DeviceType.Handheld)
        {
            await UniTask.Delay(2500);
            Authenticate();

        }
        else
        {
           
            StartGame();
        }
        

    }
    private async  void StartGame()
    {        
        
        Screen.sleepTimeout = 60;
        MusicPlayer.Instance.PlayMenus();
        var lerp = 0f;
        while (lerp < 1)
        {
            _slider.value = lerp;
            _text.text = $"{Mathf.RoundToInt(lerp * 100)}%";
            lerp += Time.deltaTime / 3;
            await UniTask.Yield();
        }
        _slider.value = 1;
        _text.text = $"100%";
        await _scenesLoader.LoadSceneAsync(ScenesNames.Menu);
    }
   
    public   void Authenticate()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(OnSignInResult);
       
    }

    public async void OnSignInResult(SignInStatus obj)
    {
        if(obj == SignInStatus.Success)
        {           
            await _storageManager.Load();
            StartGame();
        }
        else
        {
            Debug.Log("login failed");
            Application.Quit();
            
        }
    }
}
