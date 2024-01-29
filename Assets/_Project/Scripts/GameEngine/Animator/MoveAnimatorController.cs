using Atomic.Elements;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace _Project.Scripts.GameEngine.Animator
{
    public sealed class MoveAnimatorController : IUpdate
    {
        private static readonly int IS_MOVING = UnityEngine.Animator.StringToHash("IsMoving");

        private readonly UnityEngine.Animator _animator;
        private readonly IAtomicValue<bool> _isMoving;

        public MoveAnimatorController(UnityEngine.Animator animator, IAtomicValue<bool> isMoving)
        {
            _animator = animator;
            _isMoving = isMoving;
        }

        private bool _isCurrentMoving;
        public void Update()
        {
            if (_isCurrentMoving != _isMoving.Value)
            {
                _isCurrentMoving = _isMoving.Value;
                _animator.SetBool(IS_MOVING, _isMoving.Value);
            }
        }
    }
}