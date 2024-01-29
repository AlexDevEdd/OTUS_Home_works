using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace _Project.Scripts.GameEngine.Animator
{
    public sealed class DeathAnimatorTrigger
    {
        private static readonly int DEATH = UnityEngine.Animator.StringToHash("Death");

        private readonly UnityEngine.Animator _animator;
        private readonly IAtomicObservable _deathEvent;

        public DeathAnimatorTrigger(UnityEngine.Animator animator, IAtomicObservable deathEvent)
        {
            _animator = animator;
            _deathEvent = deathEvent;
        }

        public void OnEnable()
        {
            _deathEvent.Subscribe(OnDeath);
        }

        public void OnDisable()
        {
            _deathEvent.Unsubscribe(OnDeath);
        }
        
        private void OnDeath()
        {
            _animator.SetTrigger(DEATH);
        }
    }
}