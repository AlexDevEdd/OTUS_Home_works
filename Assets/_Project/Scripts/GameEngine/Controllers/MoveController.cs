using _Project.Scripts.GameEngine.Interfaces;
using Atomic.Objects;
using Plugins.Atomic.Elements.Scripts.Implementations;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Extensions.Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class MoveController : IInitializable, ITickable
    {
        private readonly AtomicObject _character;
        private IAtomicVariable<Vector3> _moveDirection;
        
        [Inject]
        public MoveController(ICharacter character)
        {
            _character = character as AtomicObject;
        }
        
        public void Initialize()
        {
            _moveDirection = new AtomicProperty<Vector3>(GetMoveDirection, SetMoveDirection);
        }

        public void Tick()
        {
            Update();
        }
        
        private Vector3 GetMoveDirection()
        {
            var direction = _character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            if (direction != null)
            {
                return direction.Value;
            }
            return default;
        }
        
        private void SetMoveDirection(Vector3 value)
        {
            var direction = _character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            if (direction != null)
            {
                direction.Value = value;
            }
        }

        private void Update()
        {
            if (_moveDirection == null)
            {
                return;
            }
            
            var direction = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction.x = 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                direction.z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction.z = -1;
            }
            
            _moveDirection.Value = direction;
        }
    }
}