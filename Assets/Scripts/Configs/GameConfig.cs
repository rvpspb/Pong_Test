using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong.config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerType Player1Type { get; private set; }
        [field: SerializeField] public PlayerType Player2Type { get; private set; }
        [field: SerializeField] public float PuddleSpeed { get; private set; }
        //[field: SerializeField] public float BallSpawnPeriod { get; private set; }
    }
}

public enum PlayerType
{
    Player,
    Bot
}
