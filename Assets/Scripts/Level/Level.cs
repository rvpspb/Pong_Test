using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.factory;
using pong.config;
using pong.input;

namespace pong.core
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform Player1StartPoint { get; private set; }
        [field: SerializeField] public Transform Player2StartPoint { get; private set; }

        [SerializeField] private BallFactory _ballFactory;
        [SerializeField] private PaddleFactory _paddleFactory;

        private GameConfig _gameConfig;
        private IInput _input;
        private BotConfig _botConfig;
        private BallConfig _ballConfig;

        public void Construct(GameConfig gameConfig, IInput input, BotConfig botConfig, BallConfig ballConfig)
        {
            _gameConfig = gameConfig;
            _input = input;
            _botConfig = botConfig;
            _ballConfig = ballConfig;
        }

        public Ball SpawnBall()
        {
            BallView ballView = _ballFactory.GetNewInstance();
            Ball ball = new Ball(ballView, _ballConfig);            
            return ball;            
        }

        public Paddle SpawnPaddle(PaddleType paddleType, PaddleSide paddleSide)
        {
            switch (paddleType)
            {
                case PaddleType.Player : return SpawnPlayerPaddle(paddleSide); 
                case PaddleType.Bot: return SpawnBotPaddle(paddleSide);
                default: return null;
            }
        }

        private PlayerPaddle SpawnPlayerPaddle(PaddleSide playerSide)
        {
            PaddleView paddleView = GetPaddleView(playerSide);
            PlayerPaddle player = new PlayerPaddle(paddleView, _input);
            player.ResetState();
            return player;
        }

        private BotPaddle SpawnBotPaddle(PaddleSide paddleSide)
        {
            PaddleView paddleView = GetPaddleView(paddleSide);
            BotAI botAI = paddleView.gameObject.AddComponent<BotAI>();
            botAI.Construct(_botConfig, paddleSide);
            BotPaddle player = new BotPaddle(paddleView, botAI);
            player.ResetState();
            return player;
        }

        private PaddleView GetPaddleView(PaddleSide paddleSide)
        {
            PaddleView paddleView = _paddleFactory.GetNewInstance();
            Vector3 startPosition = GetStartPosition(paddleSide);
            paddleView.Construct(paddleSide, startPosition, _gameConfig.PuddleSpeed);
            return paddleView;
        }

        public Vector3 GetStartPosition(PaddleSide paddleSide)
        {
            return paddleSide == PaddleSide.Left ? Player1StartPoint.position : Player2StartPoint.position;
        }

    }
}
