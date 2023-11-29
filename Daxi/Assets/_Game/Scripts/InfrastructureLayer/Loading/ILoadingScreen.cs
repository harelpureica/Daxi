using Cysharp.Threading.Tasks;

namespace Daxi.InfrastructureLayer.Loading
{
    public interface ILoadingScreen
    {
        void UpdateProgress(float progress);

        void Show();

        void Hide();

    }
}
