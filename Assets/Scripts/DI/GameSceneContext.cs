using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using npg.bindlessdi.UnityLayer;
using pong.core;
using pong.ui;

namespace pong.di
{
    public class GameSceneContext : SceneContext
    {
        [SerializeField] private PlayPanel _playPanel;
        [SerializeField] private ResultPanel _resultPanel;
        

        public override IEnumerable<Object> GetObjects()
        {
            return new Object[] { _playPanel, _resultPanel };
        }
    }
}
