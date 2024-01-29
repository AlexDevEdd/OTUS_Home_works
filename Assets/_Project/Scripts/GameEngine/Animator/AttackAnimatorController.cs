using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace _Project.Scripts.GameEngine.Animator
{
    public sealed class AttackAnimatorController
    {
        private static readonly int ATTACK = UnityEngine.Animator.StringToHash("Attack");
        
        private readonly UnityEngine.Animator _animator;
        private readonly IAtomicObservable _attackEvent;
        
        public AttackAnimatorController(UnityEngine.Animator animator, IAtomicObservable attackEvent)
        {
            _animator = animator;
            _attackEvent = attackEvent;
        }

        public void OnEnable()
        {
            _attackEvent.Subscribe(OnAttack);
        }
        
        public void OnDisable()
        {
            _attackEvent.Unsubscribe(OnAttack);
        }

        private void OnAttack()
        {
            _animator.SetTrigger(ATTACK);
        }
    }
}