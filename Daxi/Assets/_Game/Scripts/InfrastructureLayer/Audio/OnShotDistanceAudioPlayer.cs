using Cysharp.Threading.Tasks;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.Player;
using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Audio
{
    public class OnShotDistanceAudioPlayer:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private float _distanceToPlay;

        [SerializeField]
        private AudioClip _clip;

        [Inject]
        private LevelManager _levelManager;

        private bool _playedOnce;

        private Transform _player;
        #endregion

        #region Methods
        private async void Start()
        {
            while(_levelManager.MyState!= LevelManager.LevelState.run)
            {
                await UniTask.Yield();
            }
            _player = _levelManager.MyPlayerManager.transform;
        }
        private void Update()
        {
            if(_player==null) return;

            var distanceToPlayer = _player.position - transform.position;
            if(distanceToPlayer.magnitude<_distanceToPlay)
            {
                if (!_playedOnce)
                {
                    _playedOnce = true;
                    _audioSource.PlayOneShot(_clip);

                }
            }
          
        }
        #endregion
    }
}
