using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using npg.bindlessdi.UnityLayer;
using pong.core;

namespace pong.di
{
    public class GameSceneContext : SceneContext
    {
        [SerializeField] private PlayerView _playerPrefab;
        //[SerializeField] private BallView _playerPrefab;

        public override IEnumerable<Object> GetObjects()
        {
            return new Object[] { _playerPrefab };
        }
    }
}
