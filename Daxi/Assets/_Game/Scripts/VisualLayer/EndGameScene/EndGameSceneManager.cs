

using Daxi.InfrastructureLayer.Audio;
using Daxi.InfrastructureLayer.ScenesManagment;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.EndGameScene
{
    public class EndGameSceneManager : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private Button _homeBtn;

        private bool _loadinMenu = false;
        #endregion

        #region Injects
        [Inject]
        private IScenesLoader _scenesLoader;
        #endregion

        #region Methods
        private void Start()
        {
            MusicPlayer.Instance.PlayEndGame();
            _homeBtn.onClick.AddListener(() =>
            {
                if( _loadinMenu )
                {
                    return;
                }
                _loadinMenu = true;
                if ( MusicPlayer.Instance != null)
                {
                    MusicPlayer.Instance.Stop();
                }
                _scenesLoader.LoadSceneAsync(ScenesNames.Menu);
              
            });
        }
        #endregion
    }
}
