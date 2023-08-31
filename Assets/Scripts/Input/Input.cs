using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace pong.input
{
    public class Input: MonoBehaviour, IInput
    {
        public event Action OnUpArrowHold;
        public event Action OnDownArrowHold;
        public event Action OnAnyKey;

        public void GetInput()
        {
            //if (UnityEngine.Input.GetKey(KeyCode.UpArrow)) OnKeyHold?.Invoke(KeyCode.UpArrow);
        }        
    }
}
