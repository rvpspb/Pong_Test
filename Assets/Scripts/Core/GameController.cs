using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using pong.config;
using pong.input;
using pong.factory;
using pong.data;

namespace pong.core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Level _levelPrefab;

        private Paddle _leftPaddle;
        private Paddle _rightPaddle;
        private List<Ball> _balls; 
        private Level _currentLevel;
        private BallSpawner _ballSpawner;
        private GameConfig _gameConfig;
        private BallConfig _ballConfig;
        private IInput _input;
        private PlayerDataHandler _leftPlayerData;
        private PlayerDataHandler _rightPlayerData;

        public event Action<PaddleSide, int> OnScore;

        public void Construct(GameConfig gameConfig, IInput input, BallConfig ballConfig)
        {
            _gameConfig = gameConfig;
            _ballConfig = ballConfig;
            _input = input;
            _balls = new List<Ball>();
            _leftPlayerData = new PlayerDataHandler();
            _rightPlayerData = new PlayerDataHandler();
        }

        public void LoadLevel()
        {
            _currentLevel = Instantiate(_levelPrefab);
            _currentLevel.Construct(_gameConfig, _input);
            _ballSpawner = _currentLevel.GetComponent<BallSpawner>();            
        }

        public void ResetLevel()
        {
            _leftPaddle.ResetState();
            _rightPaddle.ResetState();

            _leftPlayerData.SetScore(0);
            _rightPlayerData.SetScore(0);            
        }

        private void SpawnBall()
        {
            BallView ballView = _ballSpawner.Spawn();
            Ball ball = new Ball(ballView, _ballConfig);
            _balls.Add(ball);
            ballView.OnGoalHit += GoalHit;
        }

        private void GoalHit(PaddleSide playerSide)
        {
            switch (playerSide)
            {
                case PaddleSide.Right:
                    {
                        _leftPlayerData.AddScore();
                        OnScore?.Invoke(PaddleSide.Left, _leftPlayerData.Score);
                    }
                    break;

                case PaddleSide.Left:
                    {
                        _rightPlayerData.AddScore();
                        OnScore?.Invoke(PaddleSide.Right, _rightPlayerData.Score);
                    }
                    break;
            }            
        }

        public void ClearBalls()
        {           
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
            _leftPaddle = _currentLevel.SpawnPaddle(_gameConfig.Player1Type, PaddleSide.Left);
            _rightPaddle = _currentLevel.SpawnPaddle(_gameConfig.Player2Type, PaddleSide.Right);
        }

        public void StartGame()
        {           
            _leftPaddle.SetActive(true);
            _rightPaddle.SetActive(true);
            SpawnBall();
        }

        public void StopGame()
        {
            _leftPaddle.SetActive(false);
            _rightPaddle.SetActive(false);
            ClearBalls();
        }
    }
}

public enum PaddleSide
{
    Left,
    Right,
    None
}
