using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

namespace Daxi.VisualLayer.UI
{
    public class ButtonAnimator:MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        #region Fields
        [SerializeField]
        private float _transitionTime;

        [SerializeField]
        private Vector3 _transitionScale;

        private bool _active=true;

        #endregion

        #region Method
        public async void OnPointerEnter(PointerEventData eventData)
        {
            var lerp = 0f;
            while(lerp<1)
            {
                if(!_active)
                {
                    return;
                }
                transform.localScale = Vector3.Lerp(Vector3.one, _transitionScale,lerp);
                lerp += Time.deltaTime/ _transitionTime;
                await UniTask.Yield();
            }
        }

        public async void OnPointerExit(PointerEventData eventData)
        {            
            var lerp = 0f;
            while (lerp < 1)
            {
                if (!_active )
                {
                    return;
                }
                transform.localScale = Vector3.Lerp(_transitionScale, Vector3.one, lerp);
                lerp += Time.deltaTime / _transitionTime;
                await UniTask.Yield();
            }
        }
        private void OnDisable()
        {
            _active = false;
        }
        #endregion

    }
}
