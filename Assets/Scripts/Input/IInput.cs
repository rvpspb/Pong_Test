using System;

namespace pong.input
{
    public interface IInput 
    {        
        bool Inverted { get; }
                
        event Action OnAnyKey;
        event Action OnUpdate;

        void SetInverted(bool value);
        float GetVertical(PaddleSide side);
    }
}
