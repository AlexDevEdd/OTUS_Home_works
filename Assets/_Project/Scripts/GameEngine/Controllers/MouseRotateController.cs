using Atomic.Elements;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class MouseRotateController
    {
        private readonly IAtomicVariable<Vector3> _rotateDirection;
        private readonly Camera _camera;
        
        public MouseRotateController(IAtomicVariable<Vector3> rotateDirection)
        {
            _rotateDirection = rotateDirection;
            _camera = Camera.main;
        }

        public void Update()
        {
            if (_rotateDirection == null)
                return;
            
            if (Physics.Raycast(GetMouseRay(), out var hit))
            {
                _rotateDirection.Value = hit.point;
            }
        }
        
        private Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}