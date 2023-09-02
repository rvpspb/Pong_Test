using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace pong.input
{
    public class Input: MonoBehaviour, IInput
    {        
        public bool Inverted { get; private set; }

        private int _directionMod;
        private float _vertical1;
        private float _vertical2;

        public event Action OnAnyKey;
        public event Action OnUpdate;

        private const string Vertical_1 = "Vertical_1";
        private const string Vertical_2 = "Vertical_2";

        private void Awake()
        {
            SetInverted(false);
        }

        private void Update()
        {
            _vertical1 = UnityEngine.Input.GetAxisRaw(Vertical_1) * _directionMod;
            _vertical2 = UnityEngine.Input.GetAxisRaw(Vertical_2) * _directionMod;

            if (UnityEngine.Input.anyKeyDown)
            {
                OnAnyKey?.Invoke();
            }

            OnUpdate?.Invoke();
        }

        public float GetVertical(PaddleSide paddleSide)
        {           
            return paddleSide == PaddleSide.Left ? _vertical1 : _vertical2;
        }

        public void SetInverted(bool value)
        {
            Inverted = value;
            _directionMod = GetDirectionMod(Inverted);
        }

        private int GetDirectionMod(bool inverted)
        {
            return inverted ? -1 : 1;
        }
    }
}
