using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;
using npg.bindlessdi.UnityLayer;
using pong.core;
using pong.config;
using pong.input;

namespace pong.states
{
	public class CoreGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly UnityObjectContainer _unityObjectContainer;
		private readonly GameConfig _gameConfig;
		private readonly GameController _gameController;
		private readonly Input _input;

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
			_gameController.StartGame();

			_gameController.OnGoalHit += RestartGame;

			WaitAndStop();
		}

		private async UniTask WaitAndStop()
        {
			//await UniTask.Delay(1000, ignoreTimeScale: false);

			//_gameController.ClearLevel();

			//await UniTask.Delay(1000, ignoreTimeScale: false);

			//_gameStateMachine.Enter<StartGameState>();
		}

		private void RestartGame(PlayerSide playerSide)
        {
			_gameStateMachine.Enter<StartGameState>();
        }

		public void Exit()
		{
			_gameController.OnGoalHit -= RestartGame;

			_gameController.ClearLevel();
		}

		
	}
}