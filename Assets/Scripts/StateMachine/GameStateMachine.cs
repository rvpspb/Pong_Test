using npg.states;

namespace pong.states
{
	public class GameStateMachine : StateMachine<IGameState>
	{
		public GameStateMachine(StateFactory stateFactory) : base(stateFactory)
		{

		}
	}
}