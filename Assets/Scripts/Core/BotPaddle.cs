using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong.core
{
    public class BotPaddle : Paddle
    {
        private BotAI _botAI;

        public BotPaddle(PaddleView playerView, BotAI botAI)
        {
            _playerView = playerView;
            _botAI = botAI;
        }

        public override void SetActive(bool value)
        {
            base.SetActive(value);

            if (value)
            {
                _botAI.OnDirectionChange += SetMoveDirection;
            }
            else
            {
                _botAI.OnDirectionChange -= SetMoveDirection;
            }
        }       
    }
}
