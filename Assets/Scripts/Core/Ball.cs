using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.config;

namespace pong.core
{
    public class Ball
    {
        public BallView BallView { get; private set; }
        private BallConfig _ballConfig;

        public Ball(BallView ballView, BallConfig ballConfig)
        {
            _ballConfig = ballConfig;
            BallView = ballView;
            Vector3 startDirection = new Vector3(Random.value > 0.5f ? 1f : -1f, Random.Range(-1f, 1f), 0f);
            BallView.Construct(ballConfig.StartSpeed, startDirection);            
            BallView.OnPaddleHit += PaddleBounce;
        }

        private void PaddleBounce()
        {
            Vector3 newDirection = BallView.MoveDirection;
            newDirection.x *= -1f;
            float sideOffset = _ballConfig.BounceSideRandom * (-0.5f + Random.value);
            newDirection.y += sideOffset;
            BallView.SetDirection(newDirection);
            BallView.SetSpeed(BallView.MoveSpeed + _ballConfig.BounceSpeedAdd);
        }

        public void ClearView()
        {
            BallView.OnPaddleHit += PaddleBounce;

            BallView.Dispose();
        }
    }
}