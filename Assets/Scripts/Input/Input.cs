using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace pong.input
{
    public class Input: MonoBehaviour, IInput
    {
        public float Vertical { get; private set; }

        public event Action OnAnyKey;

        private const string Vertical_1 = "Vertical_1";



        private void Update()
        {
            Vertical = UnityEngine.Input.GetAxisRaw(Vertical_1);

            if (UnityEngine.Input.anyKeyDown)
            {
                OnAnyKey?.Invoke();
            }

            //Debug.Log(Vertical);
        }
    }
}
