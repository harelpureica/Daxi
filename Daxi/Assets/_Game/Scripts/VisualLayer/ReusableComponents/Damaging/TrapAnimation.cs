using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Damaging
{
    public class TrapAnimation:IInitializable
    {
        [Inject]
        private DamagingComponent _damaging;

        [Inject]
        private Animator _animator;

        public void Initialize()
        {
            _damaging.Damaging += AnimateTrap;
        }

        private void AnimateTrap()
        {
            _animator.SetTrigger("OnPlier");
        }
    }
}
