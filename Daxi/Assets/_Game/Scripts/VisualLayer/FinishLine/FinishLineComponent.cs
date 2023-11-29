using Cysharp.Threading.Tasks;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.ReusableComponents.Interactions;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.FinishLine
{
    public class FinishLineComponent : MonoBehaviour, IInteractable
    {
        #region Injects
        [Inject]
        private LevelManager _levelManager;
        #endregion

        #region Fields
        private bool _isFinished;
        #endregion

        #region Methods

        public  void Interact()
        {
            if(_isFinished)
            {
                return;
            }
            _isFinished = true;
            _levelManager.OnReachedFinishLine();
        }
        #endregion
    }
}
