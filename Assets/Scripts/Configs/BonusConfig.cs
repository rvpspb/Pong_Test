using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong.config
{
    [CreateAssetMenu(fileName = "BonusConfig", menuName = "Configs/BonusConfig", order = 0)]
    public class BonusConfig : ScriptableObject
    {
        [field: SerializeField] public List<BonusType> BonusTypes { get; private set; }        
        [field: SerializeField] public float SpawnPeriod { get; private set; }        
        [field: SerializeField] public float UpPaddleSizeMult { get; private set; }
        [field: SerializeField] public float DownPaddleSizeMult { get; private set; }        
    }
}

public enum BonusType
{
    InvertedControls,
    UpPaddleSize,
    DownPaddleSize,
    CloneBall
}
