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

        public void Construct(GameConfig gameConfig, IInput input)
        {
            _gameConfig = gameConfig;
            _input = input;
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

        private BotPaddle SpawnBotPaddle(PaddleSide playerSide)
        {
            PaddleView paddleView = GetPaddleView(playerSide);
            BotAI botAI = paddleView.gameObject.AddComponent<BotAI>();
            BotPaddle player = new BotPaddle(paddleView, botAI);
            player.ResetState();
            return player;
        }

        private PaddleView GetPaddleView(PaddleSide playerSide)
        {
            PaddleView paddleView = _paddleFactory.GetNewInstance();
            Vector3 startPosition = GetStartPosition(playerSide);
            paddleView.Construct(startPosition, _gameConfig.PuddleSpeed);
            return paddleView;
        }

        public Vector3 GetStartPosition(PaddleSide playerSide)
        {
            return playerSide == PaddleSide.Left ? Player1StartPoint.position : Player2StartPoint.position;
        }

    }
}
