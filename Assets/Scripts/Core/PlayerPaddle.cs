using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.input;
using pong.config;

namespace pong.core
{
    public class PlayerPaddle : Paddle
    {
        private IInput _input;

        public PlayerPaddle(PaddleView playerView, IInput input)
        {
            _playerView = playerView;
            _input = input;
        }

        public override void SetActive(bool value)
        {
            base.SetActive(value);

            if (value)
            {
                _input.OnUpdate += OnInputUpdate;
            }
            else
            {
                _input.OnUpdate -= OnInputUpdate;
            }
        }

        private void OnInputUpdate()
        {
            SetMoveDirection(_input.Vertical);
        }
    }
}