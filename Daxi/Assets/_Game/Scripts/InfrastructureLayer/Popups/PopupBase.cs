using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups
{
    public abstract class PopupBase :MonoBehaviour
    {       
        #region Fields

        [SerializeField]
        protected GameObject uiParent;       

        [SerializeField]
        protected Button closeBtn;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _closeClip;

        [SerializeField]
        private AudioClip _openClip;


        protected bool isOpen = false;

        protected bool _playerClickedClose = false;


        #endregion

        #region Properties
        public bool PlayerClickedClose => _playerClickedClose;
        #endregion


        #region Methods
        protected virtual void Start()
        {
            closeBtn.onClick.AddListener(OnCloseClick);
        }
        public virtual void Close()
        {
            if (isOpen)
            {
                _audioSource.PlayOneShot(_closeClip);
                uiParent.SetActive(false);
                isOpen = false;
            }
        }
        public virtual void OnCloseClick()
        {
            closeBtn.interactable = false;
            _playerClickedClose = true;
        }      

        public virtual void Open()
        {
            if(!isOpen)
            {
                _audioSource.PlayOneShot(_openClip);
                uiParent.SetActive(true);
                isOpen = true;
            }
          
        }       

        #endregion
    }
}
