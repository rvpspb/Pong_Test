using UnityEngine;
using System;
using pong.config;

namespace pong.core
{
    public class BotAI : MonoBehaviour
    {
        private BotConfig _botConfig;
        private float _checkTime;
        private int _checkDirection;
        private int _lastMoveDirection;
        private float _proxy;
        private bool _ballWasNear;
        
        private Collider[] _colliders = new Collider[1];        
        
        public event Action<float> OnDirectionChange;

        public void Construct(BotConfig botConfig, PaddleSide paddleSide)
        {
            _botConfig = botConfig;
            _checkDirection = paddleSide == PaddleSide.Left ? 1 : -1;
            _proxy = 0.5f * transform.localScale.y;
            _lastMoveDirection = 0;
            _ballWasNear = false;
        }

        private void Update()
        {           
            if (_checkTime > _botConfig.ReactionPeriod)
            {
                Check();
                _checkTime = 0f;
            }
            else
            {
                _checkTime += Time.deltaTime;
            }
        }

        private void Check()
        {
            Vector3 position = new Vector3(transform.position.x + 0.5f * _botConfig.ReactionDistance * _checkDirection, 0f, 0f);
            Vector3 extends = new Vector3(0.5f * _botConfig.ReactionDistance, _botConfig.ReactionDistance, 1f);

            bool foundBall = Physics.OverlapBoxNonAlloc(position, extends, _colliders, Quaternion.identity, _botConfig.TargetLayer) > 0;
            int needDirection = _lastMoveDirection;
            bool ballNear = _ballWasNear;

            if (foundBall)
            {
                float delta = _colliders[0].transform.position.y - transform.position.y;
                ballNear = Mathf.Abs(delta) < _proxy;

                bool moveCloser = _colliders[0].attachedRigidbody.velocity.x * _checkDirection < 0;

                if (ballNear || !moveCloser)
                {
                    needDirection = 0;                  
                }
                else
                {
                    needDirection = (int)Mathf.Sign(delta);
                }                
            }

            if (_lastMoveDirection != needDirection && ballNear != _ballWasNear)
            {
                _lastMoveDirection = needDirection;
            }
            
            OnDirectionChange?.Invoke(needDirection);
        }
    }
}
