using _Project.Scripts.GameEngine;
using _Project.Scripts.GameEngine.Controllers;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public sealed class GameSystem : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;
        
        private MoveController _moveController;
        private FireController _fireController;
        private MouseRotateController _mouseRotateController;
        
        private void Start()
        {
            var moveDirection = new AtomicProperty<Vector3>(GetMoveDirection, SetMoveDirection);
            var rotateDirection = new AtomicProperty<Vector3>(GetRotateDirection, SetRotateDirection);
            var fireAction = character.GetAction(ObjectAPI.FireAction);

            _moveController = new MoveController(moveDirection);
            _fireController = new FireController(fireAction);
            _mouseRotateController = new MouseRotateController(rotateDirection);
        }

        private Vector3 GetMoveDirection()
        {
            var direction = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            if (direction != null)
            {
                return direction.Value;
            }
            return default;
        }
        
        private Vector3 GetRotateDirection()
        {
            var direction = character.GetVariable<Vector3>(ObjectAPI.RotateDirection);
            if (direction != null)
            {
                return direction.Value;
            }

            return default;
        }
        
        private void SetRotateDirection(Vector3 value)
        {
            var direction = character.GetVariable<Vector3>(ObjectAPI.RotateDirection);
            if (direction != null)
            {
                direction.Value = value;
            }
        }
        
        private void SetMoveDirection(Vector3 value)
        {
            var direction = character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            if (direction != null)
            {
                direction.Value = value;
            }
        }

        private void Update()
        {
            _moveController.Update();
            _fireController.Update();
            _mouseRotateController.Update();
        }
    }
}