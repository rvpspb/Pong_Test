using System;

namespace pong.input
{
    public interface IInput 
    {
        event Action OnUpArrowHold;
        event Action OnDownArrowHold;
        event Action OnAnyKey;
    }
}
