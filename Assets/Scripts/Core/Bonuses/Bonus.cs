using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong.core
{
    public class Bonus : IBonus
    {
        public BonusType BonusType { get; protected set; }

        public virtual void SwitchBonus(bool enabled)
        {

        }
    }
}
