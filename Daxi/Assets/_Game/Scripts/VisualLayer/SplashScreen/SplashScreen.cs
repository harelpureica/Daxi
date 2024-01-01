
using Cysharp.Threading.Tasks;
using Daxi.InfrastructureLayer.ScenesManagment;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Zenject;

namespace Daxi.VisualLayer.SplashScreen
{
    public class SplashScreen :MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer _player;


        [SerializeField]
        private RawImage _image;

        [SerializeField]
        private int _time=5000;

        [Inject]
        private IScenesLoader _scenesLoader;

        public async void Start()
        {            
            _player.Play();            
            await UniTask.Delay(_time);                              
            _scenesLoader.LoadSceneAsync(ScenesNames.Start);
        }


        
    }
}
