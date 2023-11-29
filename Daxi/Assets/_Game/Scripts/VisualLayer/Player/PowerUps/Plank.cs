using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Player.PowerUps
{
    public class Plank : PowerUpBase
    {
        #region Factory
        public class Factory:PlaceholderFactory<Plank>
        {

        }
        #endregion

        #region Fields
        [SerializeField]
        private float _transitionTime;

        #endregion

        #region Methods
        public async void ScaleUp()
        {
            var lerp = 0f;
            var startScale = transform.localScale;
            while (lerp < 1)
            {
                transform.localScale=Vector3.Lerp(startScale, Vector3.one, lerp);
                lerp += Time.deltaTime/_transitionTime;
                await UniTask.Yield();
            }
            transform.localScale = Vector3.one;

        }

        public override  void PowerUp()
        {
            ScaleUp();
        }
        #endregion
    }
}
