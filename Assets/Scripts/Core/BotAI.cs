using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace pong.core
{
    public class BotAI : MonoBehaviour
    {
        //private PlayerView playerView;

        public event Action<float> OnDirectionChange;

        private void Update()
        {
            OnDirectionChange?.Invoke(-0.5f + UnityEngine.Random.value);
        }
    }
}
