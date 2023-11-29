using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.UI.CountDown
{
    public class CountDownComponent
    {
        #region Injects
        [Inject(Id ="Ready")]
        private Transform _ready;

        [Inject(Id = "Set")]
        private Transform _set;

        [Inject(Id = "Go")]
        private Transform _go;

        [Inject(Id = "3")]
        private Transform _three;

        [Inject(Id = "2")]
        private Transform _two;

        [Inject(Id = "1")]
        private Transform _one;

        [Inject(Id = "CountDownPanel")]
        private GameObject _panel;

        [Inject(Id ="CountDown")]
        private AudioSource _audioSource;

        [Inject(Id = "CountDown")]
        private AudioClip _endClip;

        private bool _counting;
        #endregion

        #region Methods
        public async UniTask TextCountDown()
        {
            if(_counting)
            {
                return;
            }
            _counting=true;
            _panel.SetActive(true);
            await ShowUiRoutine(_ready, 0.2f);
            await UniTask.Delay(500);
            await HideUiRoutine(_ready, 0.2f);
            await ShowUiRoutine(_set, 0.2f);
            await UniTask.Delay(500);
            await HideUiRoutine(_set, 0.2f);
            await ShowUiRoutine(_go, 0.2f);
            await UniTask.Delay(500);
            await HideUiRoutine(_go, 0.2f);
            _panel.SetActive(false);
            _audioSource.PlayOneShot(_endClip);
            await UniTask.Delay(400);
            _counting = false;            

        }
        public async UniTask NumbersCountDown()
        {
            if (_counting)
            {
                return;
            }
            _counting = true;
            _panel.SetActive(true);
            await ShowUiRoutine(_three, 0.2f);
            await UniTask.Delay(500);
            await HideUiRoutine(_three, 0.2f);
            await ShowUiRoutine(_two, 0.2f);
            await UniTask.Delay(500);
            await HideUiRoutine(_two, 0.2f);
            await ShowUiRoutine(_one, 0.2f);
            await UniTask.Delay(500);
            await HideUiRoutine(_one, 0.2f);
            _panel.SetActive(false);
            _audioSource.PlayOneShot(_endClip);
            await UniTask.Delay(400);
            _counting = false;
        }
        private async UniTask ShowUiRoutine(Transform transform, float transitionTime)
        {
            if (transform == null)
            {
                return;
            }
            transform.gameObject.SetActive(true);   
            _audioSource.Stop();
            _audioSource.Play();
            
            var lerp = 0f;
            while(lerp < 1f)
            {
                if(transform== null)
                {
                    return;
                }
                transform.localScale=Vector3.Lerp(Vector3.zero,Vector3.one,lerp);
                lerp += Time.deltaTime/ transitionTime;
                await UniTask.Yield();
            }

        }
        private async UniTask HideUiRoutine(Transform transform, float transitionTime)
        {
            var lerp = 0f;
            while (lerp < 1f)
            {
                if(transform==null)
                {
                    return;
                }
                transform.localScale = Vector3.Lerp( Vector3.one, Vector3.zero, lerp);
                lerp += Time.deltaTime / transitionTime;
                await UniTask.Yield();
            }
            transform.gameObject.SetActive(false);

        }
        #endregion
    }
}
