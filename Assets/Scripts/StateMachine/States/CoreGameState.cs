using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;
using npg.bindlessdi.UnityLayer;
using pong.core;
using pong.config;
using pong.input;
using pong.ui;
using pong.helpers;

namespace pong.states
{
	public class CoreGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;
		private readonly GameConfig _gameConfig;
		private readonly GameController _gameController;
		private readonly Input _input;
		private PlayPanel _playPanel;
		private GameTimer _gameTimer;

		public CoreGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, GameConfig gameConfig, GameController gameController, Input input)
		{
			_gameStateMachine = gameStateMachine;
			_unityObjectContainer = unityObjectContainer;
			_gameConfig = gameConfig;
			_gameController = gameController;
			_input = input;
		}

		public void Enter()
		{
			if (!_unityObjectContainer.TryGetObject(out _playPanel))
			{
				return;
			}

			_playPanel.Show();
			_playPanel.ClearScore();
			_gameController.StartGame();

			_gameTimer = new GameTimer(_gameConfig.GamePeriod, 1);
			_gameTimer.Start();

			_gameController.OnScore += OnScore;
			_gameTimer.OnTargetTime += OnTimer;
		}

		public void Exit()
		{
			_gameController.OnScore -= OnScore;
			_gameTimer.OnTargetTime -= OnTimer;

			_gameController.StopGame();
			_gameController.ClearBalls();
			_playPanel.Hide();
		}

		private void OnTimer()
        {
			_gameStateMachine.Enter<EndGameState, PaddleSide>(PaddleSide.None);
		}

		private void OnScore(PaddleSide paddleSide, int score)
        {
			_playPanel.SetScore(paddleSide, score);

			if (score >= _gameConfig.WinScore)
            {
				_gameStateMachine.Enter<EndGameState, PaddleSide>(paddleSide);
				return;
			}

			StopAndResumeGame();
		}

		private async UniTask StopAndResumeGame()
		{
			_gameController.StopGame();
			await UniTask.Delay(1000);
			_gameController.StartGame();
		}			
	}
}