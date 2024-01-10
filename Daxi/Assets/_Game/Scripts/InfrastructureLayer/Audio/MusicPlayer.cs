using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Daxi.InfrastructureLayer.Audio
{

    public class MusicPlayer : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private AudioClip _menusMusic;

        [SerializeField]
        private AudioClip _worldOneMusic;

        [SerializeField]
        private AudioClip _worldTwoMusic;

        [SerializeField]
        private AudioClip _worldThreeMusic;

        [SerializeField]
        private AudioClip _endGameClip;

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
            _audioSource.time = 0f;
            _audioSource.clip = clip;
            _audioSource.Play();
            await TransitionVolume(1f, 0.5F);
           
        }
        public  void PlayWorlds()
        {

            var first8CharcterOfScene = SceneManager.GetActiveScene().name.Remove(7);
            Debug.Log(first8CharcterOfScene);
            var clip = _worldOneMusic;
            switch (first8CharcterOfScene)
            {
                case "WorldOn":
                    clip = _worldOneMusic;
                    break;

                case "WorldTw":
                    clip = _worldTwoMusic;
                    break;

                case "WorldTh":
                    clip = _worldThreeMusic;
                    break;

                    default:
                    Debug.Log("eror - musicplayer");
                    break;


            }
            _isPlaying = true;
            PlayClip(clip);
        }
        public async UniTask Stop()
        {
            if (_audioSource.isPlaying)
            {
                await TransitionVolume(0f, 0.5F);
                _audioSource.Stop();
                _audioSource.time = 0f;
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
            _audioSource.UnPause();

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

        public async void PlayEndGame()
        {
            if (_audioSource.isPlaying)
            {
                await TransitionVolume(0f, 0.5F);
                _audioSource.Stop();
            }
            await TransitionVolume(1, 0.5F);
            _audioSource.PlayOneShot(_endGameClip);
        }

        #endregion


    }
}
