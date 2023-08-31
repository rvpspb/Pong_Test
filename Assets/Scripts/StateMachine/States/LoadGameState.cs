using npg.states.Infrastructure;
using Cysharp.Threading.Tasks;

namespace pong.states
{
	public class LoadGameState : IGameState, IState
	{
		private readonly GameStateMachine _gameStateMachine;
		
		public LoadGameState(GameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;			
		}

		public void Enter()
		{
			Load();			
		}

		private async UniTask Load()
		{

		}

		public void Exit()
		{
			
		}
	}
}