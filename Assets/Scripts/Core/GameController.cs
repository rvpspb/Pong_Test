using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using pong.config;
using pong.input;
using pong.factory;

namespace pong.core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerPrefab;
        [SerializeField] private Level _levelPrefab;

        private PlayerController _player1;
        private PlayerController _player2;
        private List<Ball> _balls; 
        private Level _currentLevel;
        private BallSpawner _ballSpawner;
        private GameConfig _gameConfig;
        private BallConfig _ballConfig;
        private IInput _input;

        public event Action<PlayerSide> OnGoalHit;

        public void Construct(GameConfig gameConfig, IInput input, BallConfig ballConfig)
        {
            _gameConfig = gameConfig;
            _ballConfig = ballConfig;
            _input = input;
            _balls = new List<Ball>();
        }

        public void LoadLevel()
        {
            _currentLevel = Instantiate(_levelPrefab);
            _ballSpawner = _currentLevel.GetComponent<BallSpawner>();

            
        }

        private void OnDisable()
        {
            
        }


        public void ResetLevel()
        {
            _player1.ResetState();
            _player2.ResetState();
        }

        private void SpawnBall()
        {
            BallView ballView = _ballSpawner.Spawn();
            Ball ball = new Ball(ballView, _ballConfig);
            _balls.Add(ball);
            ballView.OnGoalHit += GoalHit;
        }

        private void GoalHit(PlayerSide playerSide)
        {
            OnGoalHit?.Invoke(playerSide);
        }

        public void ClearLevel()
        {
            //_player1.ClearView();
            //_player1 = null;
            //_player2.ClearView();
            //_player2 = null;

            if (_balls == null || _balls.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _balls.Count; i++)
            {
                _balls[i].BallView.OnGoalHit -= GoalHit;
                _balls[i].ClearView();
                _balls[i] = null;
            }

            _balls.Clear();
        }

        public void SpawnPlayers()
        {            
            _player1 = SpawnPlayer(_gameConfig.Player1Type, PlayerSide.Left);
            _player2 = SpawnPlayer(_gameConfig.Player2Type, PlayerSide.Right);
        }

        public void StartGame()
        {
            _player1.SetActive(true);
            _player2.SetActive(true);

            SpawnBall();
        }

        private PlayerController SpawnPlayer(PlayerType playerType, PlayerSide playerSide)
        {
            PlayerView playerView = Instantiate(_playerPrefab);
            Vector3 startPosition = _currentLevel.GetStartPosition(playerSide);
            playerView.Construct(startPosition, _gameConfig.PuddleSpeed, playerType == PlayerType.Player ? _input : null);
            PlayerController player = new PlayerController(playerType, playerView, _input);
            player.ResetState();
            return player;
        }
    }
}

public enum PlayerSide
{
    Left,
    Right
}
