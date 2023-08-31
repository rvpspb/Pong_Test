using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong.config
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Configs/BallConfig", order = 0)]
    public class BallConfig : ScriptableObject
    {
        [field: SerializeField] public float StartSpeed { get; private set; }
        [field: SerializeField] public float BounceSpeedAdd { get; private set; }
        [Range(0f, 1f)] [SerializeField] private float _bounceSideRandom;
        public float BounceSideRandom => _bounceSideRandom;
    }
}
