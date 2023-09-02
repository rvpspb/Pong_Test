using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;
using npg.bindlessdi.UnityLayer;
using pong.core;
using pong.config;

namespace pong.states
{
	public class LoadGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		//private readonly UnityObjectContainer _unityObjectContainer;
		//private readonly GameConfig _gameConfig;
		private readonly GameController _gameController;

		public LoadGameState(GameStateMachine gameStateMachine, UnityObjectContainer unityObjectContainer, GameConfig gameConfig, GameController gameController)
		{
			_gameStateMachine = gameStateMachine;
			//_unityObjectContainer = unityObjectContainer;
			//_gameConfig = gameConfig;
			_gameController = gameController;
		}

		public void Enter()
		{
			Load();			
		}

		public void Exit()
		{

		}

		private void Load()
		{	 
			_gameController.LoadLevel();
			//_gameController.SpawnPlayers();

			_gameStateMachine.Enter<StartGameState>();
		}		
	}
}