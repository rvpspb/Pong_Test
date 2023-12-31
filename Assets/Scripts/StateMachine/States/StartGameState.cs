using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;
using npg.bindlessdi.UnityLayer;
using pong.core;
using pong.config;
using pong.input;
using pong.ui;

namespace pong.states
{
	public class StartGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;
		private readonly GameController _gameController;
		private readonly Input _input;
		private StartPanel _startPanel;

		public StartGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, GameConfig gameConfig, GameController gameController, Input input)
		{
			_gameStateMachine = gameStateMachine;
			_unityObjectContainer = unityObjectContainer;
			_gameController = gameController;
			_input = input;
		}

		public void Enter()
		{
			if (!_unityObjectContainer.TryGetObject(out _startPanel))
			{
				return;
			}

			_startPanel.Show();
			_gameController.ResetLevel();

			_input.OnAnyKey += StartGame;
		}

		public void Exit()
		{
			_startPanel.Hide();

			_input.OnAnyKey -= StartGame;
		}

		private void StartGame()
        {
			_gameStateMachine.Enter<CoreGameState>();
        }		
	}
}