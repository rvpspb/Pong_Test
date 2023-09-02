using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using npg.bindlessdi;
using pong.states;
using pong.config;
using pong.core;
using pong.factory;

namespace pong.di
{
    public class EnterPoint : MonoBehaviour
    {
        [SerializeField] private input.Input _input;
        [SerializeField] private LevelFactory _levelFactory;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private BallConfig _ballConfig;
        [SerializeField] private BotConfig _botConfig;
        [SerializeField] private BonusConfig _bonusConfig;

        private void Awake()
        {
            Container container = Container.Initialize();

            container.BindInstance(_input);
            container.BindInstance(_gameConfig);
            container.BindInstance(_levelFactory);
            container.BindInstance(_ballConfig);
            container.BindInstance(_botConfig);
            container.BindInstance(_bonusConfig);

            GameStateMachine gameStateMachine = container.Resolve<GameStateMachine>();
            gameStateMachine.Enter<LoadGameState>();
        }
    }
}
