using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Inputs.Installers
{
    [CreateAssetMenu(fileName = "InputManagerInstaller", menuName = "Installers/Inputs/InputManagerInstaller")]

    public class InputManagerInstaller:ScriptableObjectInstaller<InputManagerInstaller>
    {
        #region Methods

        public override void InstallBindings()
        {
            Container
                .Bind<IInputManager>()
                .To<InputManager>()
                .AsSingle();
        }
        #endregion

    }
}
