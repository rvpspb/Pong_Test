using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.input;
using pong.config;

namespace pong.core
{
    public class PlayerController
    {
        //public bool IsActive { get; private set; }
        private PlayerView _playerView;
        private IInput _input;
        private BotAI _botAI;
        
        public PlayerController(PlayerType playerType, PlayerView playerView, IInput input)
        {
            _playerView = playerView;
            if (playerType == PlayerType.Bot)
            {
                _botAI = _playerView.gameObject.AddComponent<BotAI>();
            }
            else
            {
                _input = input;
            }
        }

        public void ResetState()
        {
            _playerView.ResetState();
        }

        public void SetActive(bool value)
        {
            _playerView.SetActive(value);

            if (value)
            {
                if (_botAI) _botAI.OnDirectionChange += SetMoveDirection;
            }
            else
            {
                if (_botAI) _botAI.OnDirectionChange -= SetMoveDirection;
            }
        }

        private void SetMoveDirection(float direction)
        {
            _playerView.SetVertical(direction);
        }

        public void ClearView()
        {
            _playerView.Dispose();
        }


    }
}
