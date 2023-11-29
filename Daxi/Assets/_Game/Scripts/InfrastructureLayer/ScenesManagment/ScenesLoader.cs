using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;
using Daxi.InfrastructureLayer.Loading;
using System.Collections.Generic;

namespace Daxi.InfrastructureLayer.ScenesManagment
{
    public class ScenesLoader:IScenesLoader,IInitializable
    {
        #region Injects
        [Inject]
        private ZenjectSceneLoader _sceneLoader;

        [Inject]
        private List<SceneSet> _sceneSets;
        #endregion

        #region Fields

        private bool isLoading;


        private  bool isUnLoading;
        #endregion

        #region Methods
        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode)
        {
            isLoading = true;
            await _sceneLoader.LoadSceneAsync(sceneName, mode);
            isLoading = false;
        }

        public async UniTask LoadSceneAsync(string sceneName)
        {

            if (isLoading)
            {
                Debug.Log("IsLoadingAlready");
                return;
            }

            if (IsSceneActive(sceneName))
            {
                Debug.Log($"{sceneName}scene is allreadyLoaded");
                return;
            }
            isLoading = true;
            var sceneIndex = 0;
            for (int i = 0; i < _sceneSets.Count; i++)
            {
                if (sceneName == _sceneSets[i].MyScene)
                {
                    sceneIndex = i;
                    break;
                }
            }
            var dependencyCount = _sceneSets[sceneIndex].MyDependency.Count;

            if (dependencyCount < 1)
            {
                await _sceneLoader.LoadSceneAsync(_sceneSets[sceneIndex].MyScene, LoadSceneMode.Single);
            }
            else
            {
                for (int i = 0; i < dependencyCount; i++)
                {
                    if (i == 0)
                    {
                        if (IsSceneActive(_sceneSets[sceneIndex].MyDependency[i]))
                        {
                            await UnloadSceneAsync(_sceneSets[sceneIndex].MyDependency[i]);
                        }
                        await _sceneLoader.LoadSceneAsync(_sceneSets[sceneIndex].MyDependency[i], LoadSceneMode.Single);
                    }
                    else
                    {
                        await _sceneLoader.LoadSceneAsync(_sceneSets[sceneIndex].MyDependency[i], LoadSceneMode.Additive);
                    }
                }
                await _sceneLoader.LoadSceneAsync(_sceneSets[sceneIndex].MyScene, LoadSceneMode.Additive);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName( _sceneSets[sceneIndex].MyScene));
            }
            isLoading = false;

        }
        public  AsyncOperation LoadSceneAsyncOperation(string sceneName,LoadSceneMode mode)
        {
            return _sceneLoader.LoadSceneAsync(sceneName, mode);          
        }


        public  async UniTask UnloadSceneAsync(string sceneName)
        {
            if (isUnLoading)
            {
                Debug.Log("EROR");
                return;
            }
            if(!IsSceneActive(sceneName))
            {
                Debug.Log("sceneToUnloadIsntaCTIVE");
                return;
            }
            isUnLoading = true;
            await SceneManager.UnloadSceneAsync(sceneName);
            
            isUnLoading = false;
        }

        public  bool IsSceneActive(string sceneName)
        {
            return SceneManager.GetSceneByName(sceneName).isLoaded;
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
