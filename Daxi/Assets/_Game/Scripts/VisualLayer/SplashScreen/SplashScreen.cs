
using Cysharp.Threading.Tasks;
using Daxi.InfrastructureLayer.ScenesManagment;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace Daxi.VisualLayer.SplashScreen
{
    public class SplashScreen :MonoBehaviour,IInitializable
    {
        [SerializeField]
        private VideoPlayer _player;

        [SerializeField]
        private int _time=5000;

        [Inject]
        private IScenesLoader _scenesLoader;

        public async void Initialize()
        {
            _player.Play();
            await UniTask.Delay(_time);
            _scenesLoader.LoadSceneAsync(ScenesNames.Start);

        }
    }
}
