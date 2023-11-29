using Daxi.DataLayer.LevelsData;
using Daxi.VisualLayer.LevelSelection;
using UnityEngine;
using Zenject;
using UnityEngine.UI;


namespace Daxi.VisualLayer.UI
{
    public class LevelButton:MonoBehaviour
    {
        #region Factory
        public class Factory:PlaceholderFactory<LevelButton>
        {

        }

        #endregion

        #region Fields
        [SerializeField]
        private DaxiButton _button;

        [SerializeField]
        private  Image _image;
        #endregion

        #region Injects
        [Inject]
        private LevelSelectionManager _levelSelectionManager;
               

        private LevelData _levelData;
        #endregion

        #region Methods       
        public void SetData(LevelData levelData)
        {
            _levelData = levelData;
            if(levelData.Locked)
            {
                _image.enabled = false;
            }else
            {
                _image.enabled = true;
                _image.sprite = levelData.LevelbuttonSprite;
            }
            _image.sprite = levelData.LevelbuttonSprite;
            _button.OnClickDown -= OnClickLevel;
            _button.OnClickDown += OnClickLevel;
        }
        public void OnClickLevel()
        {
            _levelSelectionManager.OnLevelSelected(_levelData);
        }
        #endregion
    }
}
