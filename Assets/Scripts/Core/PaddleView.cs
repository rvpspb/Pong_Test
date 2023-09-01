using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.input;

namespace pong.core
{
    public class PaddleView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _startPosition;
        private PaddleSide _playerSide;
        private IInput _input;
        private float _moveSpeed;            
        private float _vertical;

        public bool IsActive { get; private set; }

        public void Construct(Vector3 startPosition, float moveSpeed)
        {
            _startPosition = startPosition;            
            _moveSpeed = moveSpeed;                    
        }

        public void ResetState()
        {
            _rigidbody.MovePosition(_startPosition);
            _rigidbody.velocity = Vector3.zero;
            IsActive = false;
        }

        private void Update()
        {
            if (!IsActive)
            {                
                return;
            }            
            
            SetSpeed();
        }

        public void SetSpeed()
        {
            _rigidbody.velocity = _vertical * _moveSpeed * Vector3.up;
        }

        public void SetActive(bool value)
        {
            IsActive = value;

            if (!value)
            {
                _vertical = 0f;
                SetSpeed();
            }
        }

        public void SetVertical(float vertical)
        {
            _vertical = vertical;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
