

using Daxi.VisualLayer.Levels;
using Zenject;

namespace Daxi.VisualLayer.UI
{
    public class PauseLevelButton:IInitializable
    {
        [Inject(Id = "PauseLevel")]
        private DaxiButton _button;

        [Inject]
        private LevelManager _levelManager;

        public  void Initialize()
        {
            _button.OnClickDown += PauseLevel;
        }
        public void PauseLevel()
        {
            if(_levelManager.PlayerDead)
            {
                return;
            }
            _levelManager.PauseLevel();
        }
    }
}
