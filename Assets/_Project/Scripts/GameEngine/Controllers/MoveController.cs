using Atomic.Elements;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class MoveController
    {
        private readonly IAtomicVariable<Vector3> _moveDirection;

        public MoveController(IAtomicVariable<Vector3> moveDirection)
        {
            _moveDirection = moveDirection;
        }

        public void Update()
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