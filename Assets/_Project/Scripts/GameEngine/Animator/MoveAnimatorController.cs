using Atomic.Behaviours;
using Atomic.Elements;
using Plugins.Atomic.Behaviours.Scripts;

namespace _Project.Scripts.GameEngine.Animator
{
    public sealed class MoveAnimatorController : IUpdate
    {
        private static readonly int IsMoving = UnityEngine.Animator.StringToHash("IsMoving");

        private readonly UnityEngine.Animator _animator;
        private readonly IAtomicValue<bool> _isMoving;

        public MoveAnimatorController(UnityEngine.Animator animator, IAtomicValue<bool> isMoving)
        {
            _animator = animator;
            _isMoving = isMoving;
        }

        public void OnUpdate()
        {
            _animator.SetBool(IsMoving, _isMoving.Value);
        }
    }
}