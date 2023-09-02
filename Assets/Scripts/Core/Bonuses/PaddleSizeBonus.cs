//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using pong.input;

namespace pong.core
{
    public class PaddleSizeBonus : Bonus
    {
        private readonly float _startSize;
        private readonly float _bonusSize;
        private readonly Paddle _leftPaddle;
        private readonly Paddle _rightPaddle;
        public PaddleSizeBonus(BonusType bonusType, Paddle leftPaddle, Paddle rightPaddle, float startSize, float bonusSize)
        {
            BonusType = bonusType;
            _startSize = startSize;
            _bonusSize = bonusSize;
            _leftPaddle = leftPaddle;
            _rightPaddle = rightPaddle;
        }

        public override void SwitchBonus(bool enabled)
        {
            _leftPaddle.SetSizeMult(enabled ? _bonusSize : _startSize);
            _rightPaddle.SetSizeMult(enabled ? _bonusSize : _startSize);
        }
    }
}
