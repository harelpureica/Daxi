using Daxi.VisualLayer.ReusableComponents.Damaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DamagingComponentInstaller : MonoInstaller<DamagingComponentInstaller>
{
    #region Fields
    [SerializeField]
    private DamagingComponent _damagingComponent;
    #endregion

    #region Methods
    public override void InstallBindings()
    {
        Container
            .Bind<DamagingComponent>()
            .FromInstance(_damagingComponent)
            .AsSingle();
     
    }
    #endregion
}
