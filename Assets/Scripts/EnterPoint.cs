using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using npg.bindlessdi;
using pong.states;


namespace pong.init
{
    public class EnterPoint : MonoBehaviour
    {
        [SerializeField] private input.Input _input;

        private void Awake()
        {
            Container container = Container.Initialize();

            container.BindInstance(_input);

            GameStateMachine gameStateMachine = container.Resolve<GameStateMachine>();
            gameStateMachine.Enter<LoadGameState>();
        }
    }
}
