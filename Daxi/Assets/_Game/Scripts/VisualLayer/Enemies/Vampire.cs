

using UnityEngine;
using Zenject;

namespace Daxi.VisualLayer.Enemies
{
    public class Vampire : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float _height;

        [SerializeField]
        private LayerMask _playerLayer;

        [SerializeField]
        private float _attackRange;

        private Vector3 _startPosition;

        private float _offset;

        private void Start()
        {

            _startPosition = transform.position;
        }
        public void Update()
        {
            _offset += Time.deltaTime * speed;
            var player = Physics2D.OverlapCircle(transform.position, _attackRange, _playerLayer);
            if (player != null)
            {
                var distnace = (player.transform.position - transform.position).magnitude;
                if (distnace > _attackRange)
                {
                    transform.position = _startPosition + new Vector3(0, _height * Mathf.Sin(_offset), 0);

                }
                else
                {
                    var downPosition = _startPosition + new Vector3(0, -(_height+ Mathf.Sin(_offset)*0.5f), 0);
                    transform.position = Vector3.Lerp(transform.position, downPosition, Time.deltaTime);
                }
            }          
            else
            {

                transform.position = _startPosition + new Vector3(0, _height * Mathf.Sin(_offset), 0);
            }



        }
    }
}
