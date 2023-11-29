using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Daxi.VisualLayer.Player
{
    public class PlayerAnimationComponent : MonoBehaviour
    {
        #region Injects
        

        [Inject(Id = "Shield")]
        private Animator _shieldAnimator;

        [Inject(Id = "Player")]
        private Animator _playerAnimator;

        [Inject]
        private ParticleSystem _slideSmokeparticleSystem;




        #endregion

        #region Fields    
        [SerializeField]
        private SpriteRenderer _spriteRenderer;


        [SerializeField]
        private List<AssetReference> _runtimeAnimatorControllersRefrences;

        private bool _charcterSet;

        private bool _animatingDamaged;

        private bool _shouldAnimateWin;

        private bool _shouldAnimateLose;


        #endregion
        #region Properties

        public bool CharcterSet => _charcterSet;


        #endregion



        #region Methods

        public  async void SetCharacter(int character)
        {
            if (character > _runtimeAnimatorControllersRefrences.Count - 1 || character < 0)
            {
                return;
            }
            _runtimeAnimatorControllersRefrences[character].LoadAssetAsync<RuntimeAnimatorController>().Completed += (operation) =>
            {
                if(operation.Status==AsyncOperationStatus.Succeeded)
                {
                    _playerAnimator.runtimeAnimatorController = operation.Result;
                    _charcterSet = true;
                }
                else
                {
                    Debug.Log("eror while loading charcters RuntimeANimtorController");
                }
            };
           
        }

        
        #region AnimatorAnimation
        public void AnimateRun()
        {
            if (_animatingDamaged)
            {
                return;
            }
            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Run", true);
        }
        public void AnimateIdle()
        {
            if (_animatingDamaged)
            {
                return;
            }
            _playerAnimator.SetBool("Idle", true);
            _playerAnimator.SetBool("Run", false);

        }
        public void AnimateGum()
        {
            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Run", false);
            _playerAnimator.ResetTrigger("EndGum");
            _playerAnimator.ResetTrigger("Gum");
            _playerAnimator.SetTrigger("Gum");
            _playerAnimator.SetLayerWeight( _playerAnimator.GetLayerIndex("Gum"), 1);
        }
        public void AnimateShield()
        {
            _shieldAnimator.SetTrigger("Shield");
        }
        public void AnimateEndShield()
        {
            _shieldAnimator.SetTrigger("EndShield");
        }
        public async UniTask AnimateEndGum()
        {
            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Run", false);
            _playerAnimator.ResetTrigger("EndGum");
            _playerAnimator.ResetTrigger("Gum");
            _playerAnimator.SetTrigger("EndGum");
            await UniTask.Delay(500);
            _playerAnimator.SetLayerWeight(_playerAnimator.GetLayerIndex("Gum"), 0);

        }
        public async void AnimateDamaged(float _timeToAnimate)
        {
            if(_animatingDamaged||_shouldAnimateWin||_shouldAnimateLose)
            {
                return;
            }
            _animatingDamaged=true;
            
            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Run", false);            
            _playerAnimator.SetTrigger("Damaged");
            await SetSpriteAlphaRoutine(_timeToAnimate/4f,0.7f);
            await SetSpriteAlphaRoutine(_timeToAnimate / 4f, 1f);
            await SetSpriteAlphaRoutine(_timeToAnimate / 4f, 0.7f);
            await SetSpriteAlphaRoutine(_timeToAnimate / 4f, 1f);
            _animatingDamaged = false;
        }
        private async UniTask SetSpriteAlphaRoutine(float timeToAnimate,float alpha)
        {
            var lerp = 0f;
            var startColor = _spriteRenderer.material.color;
            while (lerp < 1)
            {
                _spriteRenderer.material.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, alpha, lerp));
                lerp += Time.deltaTime / timeToAnimate;               
                await UniTask.Yield();
            }
            _spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b,alpha);
        }
       
        public void AnimateSlide()
        {

            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Run", false);
            _playerAnimator.SetBool("Slide", true);
            _playerAnimator.ResetTrigger("EndSlide");
            _playerAnimator.ResetTrigger("Slide");
            _playerAnimator.SetTrigger("Slide");
      
        }
        public  void AnimateEndSlide()
        {
            _playerAnimator.ResetTrigger("Slide");
            _playerAnimator.ResetTrigger("EndSlide");
            _playerAnimator.SetTrigger("EndSlide");
          
        }

        public void AnimateJump()
        {
            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Run", false);
            _playerAnimator.SetTrigger("Jump");


        }
        public void AnimateWin()
        {
            _shouldAnimateWin=true;
            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Run", false);
            _playerAnimator.SetTrigger("Win");
            _playerAnimator.SetLayerWeight(_playerAnimator.GetLayerIndex("Gum"), 0);

        }
        public void AnimateLose()
        {
            _shouldAnimateLose = true;
            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Run", false);
            _playerAnimator.SetTrigger("Lose");
            _playerAnimator.SetLayerWeight(_playerAnimator.GetLayerIndex("Gum"), 0);

        }

        #endregion
        
        #endregion
    }
    


}
