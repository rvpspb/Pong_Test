using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.input;

namespace pong.core
{
    public class PlayerView : MonoBehaviour
    {
        private Vector3 _startPosition;
        private PlayerSide _playerSide;
        private IInput _input;
        private float _moveSpeed;
        private Rigidbody _rigidbody;
        private bool _useInput;
        private float _vertical;

        public bool IsActive { get; private set; }

        public void Construct(Vector3 startPosition, float moveSpeed, IInput input = null)
        {
            _startPosition = startPosition;
            _input = input;
            _moveSpeed = moveSpeed;
            _rigidbody = GetComponent<Rigidbody>();
            _useInput = _input != null;            
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
                _vertical = 0f;
                return;
            }

            if (_useInput)
            {
                _vertical = _input.Vertical;
            }
            
            SetSpeed(_vertical);
        }

        public void SetSpeed(float vertical)
        {
            _rigidbody.velocity = vertical * _moveSpeed * Vector3.up;
        }

        public void SetActive(bool value)
        {
            IsActive = value;
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
