using System.Collections.Generic;
using System;
using pong.config;
using pong.factory;
using pong.data;

namespace pong.core
{
    public class GameController
    {
        private Paddle _leftPaddle;
        private Paddle _rightPaddle;
        private List<Ball> _balls; 
        private Level _currentLevel;
        private readonly GameConfig _gameConfig;
        private readonly BallConfig _ballConfig;
        private readonly BotConfig _botConfig;
        private readonly BonusConfig _bonusConfig;
        private readonly input.Input _input;
        private PlayerDataHandler _leftPlayerData;
        private PlayerDataHandler _rightPlayerData;
        private readonly LevelFactory _levelFactory;
        private BonusController _bonusController;

        public event Action<PaddleSide, int> OnScore;

        public GameController(LevelFactory levelFactory, GameConfig gameConfig, input.Input input, BallConfig ballConfig, BotConfig botConfig, BonusConfig bonusConfig)
        {
            _levelFactory = levelFactory;
            _gameConfig = gameConfig;
            _ballConfig = ballConfig;
            _input = input;
            _botConfig = botConfig;
            _bonusConfig = bonusConfig;

            _balls = new List<Ball>();
            _leftPlayerData = new PlayerDataHandler();
            _rightPlayerData = new PlayerDataHandler();            
        }

        public void LoadLevel()
        {
            _currentLevel = _levelFactory.GetNewInstance();
            _currentLevel.Construct(_gameConfig, _input, _botConfig, _ballConfig);
            SpawnPlayers();
            _bonusController = new BonusController(_bonusConfig, _leftPaddle, _rightPaddle, _currentLevel, _input);            
        }

        public void ResetLevel()
        {
            _leftPaddle.ResetState();
            _rightPaddle.ResetState();

            _leftPlayerData.SetScore(0);
            _rightPlayerData.SetScore(0);

            _bonusController.OnApplyBonus += OnApplyBonus;
        }

        private void SpawnBall()
        {            
            Ball ball = _currentLevel.SpawnBall();
            _balls.Add(ball);
            ball.BallView.OnGoalHit += GoalHit;
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

        private void SpawnPlayers()
        {            
            _leftPaddle = _currentLevel.SpawnPaddle(_gameConfig.Player1Type, PaddleSide.Left);
            _rightPaddle = _currentLevel.SpawnPaddle(_gameConfig.Player2Type, PaddleSide.Right);
        }

        public void StartGame()
        {           
            _leftPaddle.SetActive(true);
            _rightPaddle.SetActive(true);
            SpawnBall();            
            _bonusController.StartSpawnBonuses();            
        }

        public void StopGame()
        {
            _bonusController.Stop();
            _leftPaddle.SetActive(false);
            _rightPaddle.SetActive(false);
            ClearBalls();
        }

        public void EndGame()
        {
            _bonusController.ClearBonuses();

            _bonusController.OnApplyBonus -= OnApplyBonus;
        }

        private void OnApplyBonus(BonusType bonusType)
        {
            if (bonusType == BonusType.CloneBall)
            {
                SpawnBall();
            }
        }
    }
}

public enum PaddleSide
{
    Left,
    Right,
    None
}
