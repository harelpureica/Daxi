using Zenject;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using Daxi.InfrastructureLayer.ScenesManagment;
using Daxi.InfrastructureLayer.Audio;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using Daxi.Storage;
using System;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private TextMeshProUGUI _text;

    [Inject]
    private IScenesLoader _scenesLoader;

    [Inject]
    private StorageManager _storageManager;

    [SerializeField]
    private Button _loginBtn;

    [SerializeField]
    private Button _masterloginBtn;

    private float timeSinceCheat;
    public   void Initialize()
    {
        
        _slider.gameObject.SetActive(false);
        _loginBtn.onClick.AddListener(OnLogin);
        _masterloginBtn.onClick.AddListener(OnMasterLogin);
       
    }

    private async void OnMasterLogin()
    {
       
        _masterloginBtn.gameObject.SetActive(false);
        _slider.gameObject.SetActive(true);
        await _storageManager.Load(true);
        StartGame();
    }

    private void OnLogin()
    {
        _loginBtn.gameObject.SetActive(false);
        _slider.gameObject.SetActive(true);
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Authenticate();
           
        }else
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
            await _storageManager.Load(false);
            StartGame();
        }
        else
        { 
            
            
             _text.text=$"failed :{obj}";
          
            
        }
    }

    public void Tick()
    {
        if(Input.touchCount==5)
        {
            timeSinceCheat += Time.deltaTime;
        }else
        {
            timeSinceCheat = 0;
        }
        if(timeSinceCheat>4)
        {
            if(!_masterloginBtn.gameObject.activeInHierarchy)
            {
                _masterloginBtn.gameObject.SetActive(true);
                _loginBtn.gameObject.SetActive(false);

            }
        }
    }
}
