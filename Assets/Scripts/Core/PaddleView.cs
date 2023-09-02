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
        //private IInput _input;
        private float _moveSpeed;            
        private float _vertical;
        private float _startSize;

        public PaddleSide PaddleSide { get; private set; }
        public bool IsActive { get; private set; }

        public void Construct(PaddleSide paddleSide, Vector3 startPosition, float moveSpeed, float startSize)
        {
            PaddleSide = paddleSide;
            _startPosition = startPosition;            
            _moveSpeed = moveSpeed;
            _startSize = startSize;
            ApplySize(_startSize);
        }

        public void ResetState()
        {
            //_rigidbody.MovePosition(_startPosition);
            transform.position = _startPosition;
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

        public void ApplySize(float size)
        {
            transform.localScale = new Vector3(1f, size, 1f);
        }

        public void SetSizeMult(float mult)
        {
            float size = _startSize * mult;
            ApplySize(size);            
        }
    }
}
