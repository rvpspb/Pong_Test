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

        public event Action<PaddleSide> OnGoalHit;
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

        private void FixedUpdate()
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
                PaddleSide side = collision.transform.position.x > 0 ? PaddleSide.Right : PaddleSide.Left;
                OnGoalHit?.Invoke(side);                
            }
            else if (collision.gameObject.TryGetComponent(out PaddleView playerView))
            {
                //MoveDirection = Bounce(MoveDirection, collision.GetContact(0).normal);
                OnPaddleHit?.Invoke();                
            }
            else
            {
                MoveDirection = Bounce(MoveDirection, collision.GetContact(0).normal);
            }
        }

        private Vector3 Bounce(Vector3 moveDirection, Vector3 normal)
        {
            Vector3 proection = Vector3.Project(moveDirection, normal);            
            return moveDirection - 2f * proection;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
