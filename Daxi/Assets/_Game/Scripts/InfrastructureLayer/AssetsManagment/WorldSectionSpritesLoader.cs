
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Daxi.InfrastructureLayer.AssetsManagment
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WorldSectionSpritesLoader:MonoBehaviour
    {
        [SerializeField]
        private List<RendererImageSet> _sets;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Dictionary<RendererImageSet, bool> _SetsLoaded=new  ();

        private bool _loaded;

        private void Start()
        {
            for (int i = 0; i < _sets.Count; i++)
            {
                _SetsLoaded.Add(_sets[i], false);
            }
            if (_spriteRenderer.isVisible && !_loaded)
            {
                _loaded = true;
                for (int i = 0; i < _sets.Count; i++)
                {
                    LoadSprite(i);
                    _SetsLoaded[_sets[i]] = true;

                }
            }
            if (!_spriteRenderer.isVisible && _loaded)
            {
                _loaded = false;
                for (int i = 0; i < _sets.Count; i++)
                {
                    UnloadSprite(i);
                    _SetsLoaded[_sets[i]] = false;
                }
            }

        }

        private void Update()
        {
            if(_spriteRenderer.isVisible&& !_loaded)
            {
                
                    _loaded = true;
                for (int i = 0; i < _sets.Count; i++)
                {
                    if (_SetsLoaded[_sets[i]])
                    {
                        continue;
                    }
                        LoadSprite(i);
                    _SetsLoaded[_sets[i]] = true;
                }
            }
            if (!_spriteRenderer.isVisible && _loaded)
            {
                _loaded = false;
                for (int i = 0; i < _sets.Count; i++)
                {

                    if (!_SetsLoaded[_sets[i]])
                    {
                        continue;
                    }
                    UnloadSprite(i);
                    _SetsLoaded[_sets[i]] = false;
                }
            }

        }
      



        private void LoadSprite(int index)
        {
            if (_SetsLoaded[_sets[index]])
            {
                return; // Skip loading if already loaded
            }
            AsyncOperationHandle<Sprite> handle = _sets[index].spriteRefrence.LoadAssetAsync<Sprite>();
            if(!handle.IsValid())
            {
                return;
            }
            handle.Completed += (opHandle) =>
            {
                if (opHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    _sets[index].MyRenderer.sprite = opHandle.Result;
                }
                else
                {
                    Debug.LogError("Failed to load sprite: " + opHandle.OperationException);
                }
            };
            _sets[index].opHandle = handle;

        }

        private void UnloadSprite(int index)
        {
            if (!_SetsLoaded[_sets[index]])
            {
                return; // Skip loading if already loaded
            }
            if (_sets[index].opHandle.IsValid())
            {
                 Addressables.Release(_sets[index].opHandle);         
                _sets[index].MyRenderer.sprite = null;
            }
           

        }
    }
    [Serializable]
    public class RendererImageSet
    {
        public SpriteRenderer MyRenderer;
        public AssetReferenceSprite spriteRefrence;
        public AsyncOperationHandle<Sprite> opHandle;
    }
}
