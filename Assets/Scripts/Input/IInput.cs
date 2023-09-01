using System;

namespace pong.input
{
    public interface IInput 
    {
        public float Vertical { get; }
                
        event Action OnAnyKey;
        event Action OnUpdate;
    }
}
