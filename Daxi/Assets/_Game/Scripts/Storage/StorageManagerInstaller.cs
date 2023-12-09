
using Zenject;

namespace Daxi.Storage
{
    public class StorageManagerInstaller:MonoInstaller<StorageManagerInstaller>
    {
        #region Methods

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<StorageManager>()
                .AsSingle();
        }
        #endregion
    }
}
