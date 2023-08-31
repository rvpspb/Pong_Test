using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using npg.bindlessdi;
using pong.states;
using pong.config;
using pong.core;

namespace pong.di
{
    public class EnterPoint : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private input.Input _input;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private BallConfig _ballConfig;

        private void Awake()
        {
            Container container = Container.Initialize();

            _gameController.Construct(_gameConfig, _input, _ballConfig);

            container.BindInstance(_input);
            container.BindInstance(_gameConfig);
            container.BindInstance(_gameController);

            GameStateMachine gameStateMachine = container.Resolve<GameStateMachine>();
            gameStateMachine.Enter<LoadGameState>();
        }
    }
}
