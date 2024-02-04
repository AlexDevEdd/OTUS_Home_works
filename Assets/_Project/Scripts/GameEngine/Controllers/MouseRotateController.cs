using _Project.Scripts.GameEngine.Interfaces;
using Atomic.Elements;
using Atomic.Objects;
using Plugins.Atomic.Elements.Scripts.Implementations;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Extensions.Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class MouseRotateController : IInitializable, ITickable
    {
        private readonly AtomicObject _character;
        private IAtomicVariable<Vector3> _rotateDirection;
        private Camera _camera;
        
        [Inject]
        public MouseRotateController(ICharacter character)
        {
            _character = character as AtomicObject;
        }
        
        public void Initialize()
        {
            _camera = Camera.main;
            _rotateDirection = new AtomicProperty<Vector3>(GetRotateDirection, SetRotateDirection);
        }

        public void Tick()
        {
            Update();
        }

        private void Update()
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
        
        private Vector3 GetRotateDirection()
        {
            var direction = _character.GetVariable<Vector3>(ObjectAPI.RotateDirection);
            if (direction != null)
            {
                return direction.Value;
            }

            return default;
        }
        
        private void SetRotateDirection(Vector3 value)
        {
            var direction = _character.GetVariable<Vector3>(ObjectAPI.RotateDirection);
            if (direction != null)
            {
                direction.Value = value;
            }
        }
    }
}