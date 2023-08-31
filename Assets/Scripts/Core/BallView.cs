using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace pong.core
{
    public class BallView : MonoBehaviour
    {
        private const string _goal = "Goal";
        private Rigidbody _rigidbody;
        public float MoveSpeed { get; private set; }
        public Vector3 MoveDirection { get; private set; }

        public event Action<PlayerSide> OnGoalHit;
        public event Action OnPaddleHit;

        public bool IsActive { get; private set; }

        public void Construct(float startSpeed, Vector3 direction)
        {
            _rigidbody = GetComponent<Rigidbody>();            
            SetSpeed(startSpeed);
            SetDirection(direction);
            IsActive = true;
        }

        public void SetSpeed(float speed)
        {
            MoveSpeed = speed;            
        }

        public void SetDirection(Vector3 direction)
        {
            MoveDirection = direction;            
        }

        private void Update()
        {
            if (!IsActive)
            {
                MoveSpeed = 0f;
                return;
            }

            _rigidbody.velocity = MoveSpeed * MoveDirection;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(_goal))
            {
                PlayerSide side = collision.transform.position.x > 0 ? PlayerSide.Right : PlayerSide.Left;
                OnGoalHit?.Invoke(side);                
            }
            else if (collision.gameObject.TryGetComponent(out PlayerView playerView))
            {
                

                OnPaddleHit?.Invoke();                
            }
            else
            {
                Vector3 newDirection = MoveDirection;
                newDirection.y *= -1f;
                SetDirection(newDirection);

                //OnPaddleHit?.Invoke();
            }
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
