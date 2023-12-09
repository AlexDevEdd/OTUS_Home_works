using System;
using UnityEngine;

namespace Systems.InputSystem
{
    public interface IInput : IInputFire, IPauseResumeInput, IInputMove { }

    public interface IInputFire
    {
        public event Action OnFireEvent;
    }
    
    public interface IInputMove
    {
        public event Action<Vector2> OnMoveEvent;
    }
    
    public interface IPauseResumeInput
    {
        public event Action OnPauseEvent;
        public event Action OnResumeEvent;
    }
}