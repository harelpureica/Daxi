

using Cysharp.Threading.Tasks;
using Daxi.DataLayer.Player;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Pets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Pet:MonoBehaviour
    {
        #region Factory
      
        #endregion

        #region Injects
        [Inject]
        private LevelManager _levelManager;

        [Inject]
        private PlayerData _playerData;

        #endregion

        #region Fields
        [SerializeField]
        private Vector2 _offset;

        [SerializeField]
        private Vector2 _axisOscilationAmount;

        [SerializeField]
        private Vector2 _axisOscilationSpeed;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private List<GameObject> _gfxs;

      

        private Transform _player;

        private Rigidbody2D _rb;


        #endregion

        #region Methods
        private async void Start()
        {
            while(_levelManager.MyPlayerManager==null)
            {
                await UniTask.Yield();
            }
            _player = _levelManager.MyPlayerManager.transform;
            _rb = GetComponent<Rigidbody2D>();
            SetGfx(_playerData.PetIndex);
        }
        private void FixedUpdate()
        {
            if (_player == null)
            {
                return;
            }

            var position=  (Vector3)((Vector2)_player.position+_offset+new Vector2(Mathf.Sin(Time.time* _axisOscilationSpeed.x) *_axisOscilationAmount.x, Mathf.Sin(Time.time * _axisOscilationSpeed.y) *_axisOscilationAmount.y)) ;
            _rb.position = Vector3.Slerp(transform.position,position, Time.deltaTime*_speed);

        }
        public void SetGfx(int index)
        {
            for (int i = 0; i < _gfxs.Count; i++)
            {
                if(i==index)
                {
                    _gfxs[i].gameObject.SetActive(true);
                }else
                {
                    _gfxs[i].gameObject.SetActive(false);
                }
            }
        }
        #endregion
    }
}
