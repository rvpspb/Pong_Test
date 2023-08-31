using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong.core
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform Player1StartPoint { get; private set; }
        [field: SerializeField] public Transform Player2StartPoint { get; private set; }


        public Vector3 GetStartPosition(PlayerSide playerSide)
        {
            return playerSide == PlayerSide.Left ? Player1StartPoint.position : Player2StartPoint.position;
        }

    }
}
