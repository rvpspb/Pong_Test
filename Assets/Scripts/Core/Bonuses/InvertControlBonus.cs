//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
using pong.input;

namespace pong.core
{
    public class InvertControlBonus : Bonus
    {
        private readonly IInput _input;
        
        public InvertControlBonus(BonusType bonusType, IInput input)
        {
            BonusType = bonusType;
            _input = input;
        }

        public override void SwitchBonus(bool enabled)
        {
            _input.SetInverted(enabled);
        }
    }
}
