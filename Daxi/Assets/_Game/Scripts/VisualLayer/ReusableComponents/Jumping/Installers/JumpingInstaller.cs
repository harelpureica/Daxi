using Daxi.DataLayer.Configuration.Jumping;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.ReusableComponents.Jumping.Installers
{
    [CreateAssetMenu(fileName = "JumpingInstaller", menuName = "Installers/Jumping/JumpingInstaller")]
    public class JumpingInstaller:ScriptableObjectInstaller<JumpingInstaller>
    {
        #region Fields
        [SerializeField]
        private JumpingSettings _jumpingSettings;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<JumpingComponent>()
                .AsSingle();

            Container
                .Bind<JumpingSettings>()
                .FromInstance( _jumpingSettings )
                .AsSingle();
        }
        #endregion
    }
}
