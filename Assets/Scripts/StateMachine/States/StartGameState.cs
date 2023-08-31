using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;
using npg.bindlessdi.UnityLayer;
using pong.core;
using pong.config;
using pong.input;

namespace pong.states
{
	public class StartGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;
		private readonly GameConfig _gameConfig;
		private readonly GameController _gameController;
		private readonly Input _input;

		public StartGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, GameConfig gameConfig, GameController gameController, Input input)
		{
			_gameStateMachine = gameStateMachine;
			_unityObjectContainer = unityObjectContainer;
			_gameConfig = gameConfig;
			_gameController = gameController;
			_input = input;
		}

		public void Enter()
		{
			_gameController.ResetLevel();

			_input.OnAnyKey += StartGame;
		}

		public void Exit()
		{
			_input.OnAnyKey -= StartGame;
		}

		private void StartGame()
        {
			_gameStateMachine.Enter<CoreGameState>();
        }		
	}
}