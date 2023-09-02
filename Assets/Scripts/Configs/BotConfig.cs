using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong.config
{
    [CreateAssetMenu(fileName = "BotConfig", menuName = "Configs/BotConfig", order = 0)]
    public class BotConfig : ScriptableObject
    {
        [field: SerializeField] public LayerMask TargetLayer { get; private set; }         
        [field: SerializeField] public float ReactionDistance { get; private set; }
        [field: SerializeField] public float ReactionPeriod { get; private set; }
    }
}

