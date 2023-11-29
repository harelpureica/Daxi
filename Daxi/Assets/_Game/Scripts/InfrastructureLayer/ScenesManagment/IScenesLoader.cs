
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Daxi.InfrastructureLayer.ScenesManagment
{
    public interface IScenesLoader
    {
        UniTask LoadSceneAsync(string sceneName);
        UniTask LoadSceneAsync(string sceneName,LoadSceneMode mode);

        UniTask UnloadSceneAsync(string sceneName);

        AsyncOperation LoadSceneAsyncOperation(string sceneName, LoadSceneMode mode);
    }
}
