using Cysharp.Threading.Tasks;
using Daxi.VisualLayer.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Daxi.InfrastructureLayer.Loading
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        #region Fields
        [SerializeField]
        private RectTransform _UIpanelRectTransform;


        [SerializeField]
        private GameObject _UIparent;


        [SerializeField]
        private TextMeshProUGUI _loadingPercent;


        [SerializeField]
        private float _fadeSpeed;

        [SerializeField]
        private UiImageAnimator _imageAnimator;
        #endregion

        #region Methods

        public  void Show()
        {
            _imageAnimator.enabled = true;         
            _UIparent.SetActive(true);         
        }

        public  void Hide()
        {
            _UIparent.SetActive(false);
            _imageAnimator.enabled = false;
        }

        public void UpdateProgress(float progress)
        {
            var p =  Mathf.RoundToInt(progress * 100f);
            _loadingPercent.text = $"{p}%";
        }
      
        #endregion
    }
}
