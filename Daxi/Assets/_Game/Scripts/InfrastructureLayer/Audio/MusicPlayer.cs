using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Daxi.InfrastructureLayer.Audio
{

    public class MusicPlayer : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private AudioClip _menusMusic;

        [SerializeField]
        private AudioClip _worldMusic;

        [SerializeField]
        private AudioSource _audioSource;

        private static MusicPlayer instance;

        private static bool _isPlaying;

        #endregion

        #region Properties
        public static bool IsPlaying=> _isPlaying;
        public static MusicPlayer Instance => instance;
        #endregion


        #region Methods

        private void OnEnable()
        {
            MusicPlayer[] objs = GameObject.FindObjectsOfType<MusicPlayer>();

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
                return;
            }
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        private void OnDisable()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
        private void OnApplicationQuit()
        {
            if (instance == this)
            {
                instance = null;
            }
        }


        public  void PlayMenus()
        {
            _isPlaying = true;
            PlayClip(_menusMusic);
        }
        private async void PlayClip(AudioClip clip)
        {
            if (_audioSource.isPlaying)
            {
                await TransitionVolume(0f, 0.5F);
                _audioSource.Stop();
            }

            _audioSource.clip = clip;
            _audioSource.Play();
            await TransitionVolume(1f, 0.5F);
           
        }
        public  void PlayWorlds()
        {
            _isPlaying = true;
            PlayClip(_worldMusic);
        }
        public async UniTask Stop()
        {
            if (_audioSource.isPlaying)
            {
                await TransitionVolume(0f, 0.5F);
                _audioSource.Stop();
                _isPlaying = false;
            }
        }
        public async void Pause()
        {
            _audioSource.Pause();
            _isPlaying = false;

        }
        public async void Resume()
        {
            _isPlaying = true;
            _audioSource.Play();

        }
        private async UniTask TransitionVolume(float wantedVolume, float time)
        {
            var lerp = 1f;
            var startVolume = _audioSource.volume;
            while (lerp < 1)
            {
                _audioSource.volume = Mathf.Lerp(startVolume, wantedVolume, lerp);
                lerp += Time.deltaTime / time;
                await UniTask.Yield();
            }
            _audioSource.volume = wantedVolume;
        }

        #endregion


    }
}
