using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Player;
using Daxi.InfrastructureLayer.ScenesManagment;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Daxi.VisualLayer.WorldSelection
{
    public class WorldSelectionSceneManager : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private Button _worldOneButton;

        [SerializeField]
        private Button _worldTwoButton;

        [SerializeField]
        private Button _worldThreeButton;

       
        [SerializeField]
        private TextMeshProUGUI _worldOneText;

        [SerializeField]
        private TextMeshProUGUI _worldTwoText;

        [SerializeField]
        private TextMeshProUGUI _worldThreeText;

        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private GameObject _worldTwoLockedParent;

        [SerializeField]
        private GameObject _worldThreeLockedParent;

        [SerializeField]
        private GameObject _worldtwoBlackPanel;

        [SerializeField]
        private GameObject __worldthreeBlackPanel;

        [SerializeField]
        private GameObject _worldTwoOpenedParent;

        [SerializeField]
        private GameObject _worldThreeOpenedParent;

        [Inject]
        private PlayerData _playerData;


        [Inject]
        private IScenesLoader _scenesLoader;

        #endregion
        #region Methods
        private void Start()
        {
            _worldOneButton.onClick.AddListener(() => OnWorldSelected(1));
            _worldTwoButton.onClick.AddListener(() => OnWorldSelected(2));
            _worldThreeButton.onClick.AddListener(() => OnWorldSelected(3));
            _backButton.onClick.AddListener(OnBackClick);
            _worldTwoButton.interactable = false;
            _worldThreeButton.interactable = false;
            if (_playerData.UnlockedLevels > 6)
            {
                _worldTwoLockedParent.SetActive(false);
                _worldtwoBlackPanel.SetActive(false);
                _worldTwoOpenedParent.SetActive(true);
                


            }
            else
            {
                _worldTwoLockedParent.SetActive(true);
                _worldtwoBlackPanel.SetActive(true);
                _worldTwoOpenedParent.SetActive(false);
               

            }
            if (_playerData.UnlockedLevels > 12)
            {
                _worldThreeLockedParent.SetActive(false);
                __worldthreeBlackPanel.SetActive(false);
                _worldThreeOpenedParent.SetActive(true);

            }
            else
            {
                _worldThreeLockedParent.SetActive(true);
                __worldthreeBlackPanel.SetActive(true);
                _worldThreeOpenedParent.SetActive(false);

            }
            _worldOneText.text = $"";
            _worldTwoText.text = $"";
            _worldThreeText.text = $"";
            for (int i = 0; i < 18; i++)
            {
                if (_playerData.UnlockedLevels < 6)
                {
                    _worldOneText.text = $"{_playerData.UnlockedLevels}/6";
                    _worldOneButton.interactable = true;


                }
                else if (_playerData.UnlockedLevels < 12)
                {
                    _worldOneText.text = $"{6}/6";
                    _worldTwoText.text = $"{_playerData.UnlockedLevels - 6}/6";
                    _worldOneButton.interactable = true;
                    _worldTwoButton.interactable = true;

                }
                else
                {
                    _worldOneText.text = $"{6}/6";
                    _worldTwoText.text = $"{6}/6";
                    _worldThreeText.text = $"{_playerData.UnlockedLevels - 12}/6";
                    _worldTwoButton.interactable = true;
                    _worldThreeButton.interactable = true;

                }
            }
           
        }

        public async void OnBackClick()
        {
            _backButton.interactable = false;
          _scenesLoader.LoadSceneAsync(ScenesNames.Menu);
        }

        public async void OnWorldSelected(int worldIndex)
        {           
            var sceneName = "";
            switch (worldIndex)
            {
                case 1:
                    sceneName = ScenesNames.WorldOneLevelSelection;
                    break;
                case 2:
                    sceneName = ScenesNames.WorldTwoLevelSelection ;

                    break;
                case 3:
                    sceneName = ScenesNames.WorldThreeLevelSelection;

                    break;

            }          
            _scenesLoader.LoadSceneAsync(sceneName);

        }
        #endregion

    }
}
