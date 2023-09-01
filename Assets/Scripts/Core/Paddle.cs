using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.input;
using pong.config;

namespace pong.core
{
    public class Paddle
    {        
        protected PaddleView _playerView;        

        

        public void ResetState()
        {
            _playerView.ResetState();
        }

        public virtual void SetActive(bool value)
        {
            _playerView.SetActive(value);
        }

        protected void SetMoveDirection(float direction)
        {
            _playerView.SetVertical(direction);
        }

        public void ClearView()
        {
            _playerView.Dispose();
        }
    }
}
