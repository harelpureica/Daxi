using UnityEngine;
using Zenject;

namespace Daxi.InfrastructureLayer.Popups.Text.Installers
{
    [CreateAssetMenu(fileName = "TextPopupInstaller", menuName = "Installers/Popups/TextPopupInstaller")]
    public class TextPopupInstaller:ScriptableObjectInstaller<TextPopupInstaller>
    {
        #region Fields
        [SerializeField]
        private TextPopup _prefab;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            Container
               .BindFactory<TextPopup, TextPopup.TextPopupFactory>()
               .FromComponentInNewPrefab(_prefab)
               .AsSingle();
           
        }
        #endregion

    }
}
