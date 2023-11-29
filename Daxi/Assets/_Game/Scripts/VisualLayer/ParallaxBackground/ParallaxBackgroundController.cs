

using Daxi.VisualLayer.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ParallaxBackground
{
    public class ParallaxBackgroundController : ITickable,IInitializable
    {
        #region Injects
        [Inject]
        private List<ParallaxLayer>_layers;

        [Inject]
        private PlayerManager _playerManager;       
       
        #endregion

        #region Fields
        private Vector2 _startPosition;

        public void Initialize()
        {
            _startPosition=_playerManager.transform.position;
        }
        #endregion

        #region Methods
        public void Tick()
        {
            for (int i = 0; i < _layers.Count; i++)
            {
                var offset = Vector2.zero;
                offset = (Vector2)_playerManager.transform.position - _startPosition;
                offset.x *= _layers[i].AxisMultiplier.x;
                offset.y *= _layers[i].AxisMultiplier.y;
                _layers[i].MyMaterial.SetVector("_Offset", offset);
            }
           
        }
        #endregion
    }
    [Serializable]
    public struct ParallaxLayer
    {
        public Vector2 AxisMultiplier;

        public Material MyMaterial;       
    }
}
