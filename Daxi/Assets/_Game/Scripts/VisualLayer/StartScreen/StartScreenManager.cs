using Zenject;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using Daxi.InfrastructureLayer.ScenesManagment;
using Daxi.InfrastructureLayer.Audio;

public class StartScreenManager : IInitializable
{
    [Inject(Id ="Start")]
    private Slider _slider;

    [Inject(Id = "Start")]
    private TextMeshProUGUI _text;

    [Inject]
    private IScenesLoader _scenesLoader;
    public async void Initialize()
    {
        Screen.sleepTimeout =60;
        MusicPlayer.Instance.PlayMenus();
        var lerp = 0f;
        while(lerp < 1)
        {
            _slider.value = lerp;
            _text.text = $"{Mathf.RoundToInt(lerp * 100)}%";
            lerp+= Time.deltaTime/3;
            await UniTask.Yield();
        }
        _slider.value = 1;
        _text.text = $"100%";
        await Login();
        await _scenesLoader.LoadSceneAsync(ScenesNames.Menu);
   

    }   
    private  async UniTask Login()
    {

    }

}
