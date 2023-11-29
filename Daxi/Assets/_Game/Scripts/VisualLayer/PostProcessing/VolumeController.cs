

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Daxi.VisualLayer.PostProcessing
{
    public class VolumeController:MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private Volume _volume;

        private DepthOfField _dof;
        #endregion

        #region Methods
        private void Start()
        {
            _volume.profile.TryGet<DepthOfField>(out _dof);
        }
        public void SetDof(bool enabled )
        {                  
            _dof.active = enabled;            
        }
        #endregion
    }
}
