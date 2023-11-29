using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Damaging.Installers
{
    public class HealthComponentInstaller:MonoInstaller<HealthComponentInstaller>
    {
        #region Methods
        public override void InstallBindings()
        {
            Container
                .Bind<IHealthComponent>()
                .To<HealthComponent>()
                .AsSingle();
        }
        #endregion
    }
}
