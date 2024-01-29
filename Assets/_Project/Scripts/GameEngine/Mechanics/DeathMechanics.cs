using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class DeathMechanics
    {
        private readonly IAtomicObservable<int> _hitPoints;
        private readonly IAtomicEvent _deathEvent;

        public DeathMechanics(IAtomicObservable<int> hitPoints, IAtomicEvent deathEvent)
        {
            _hitPoints = hitPoints;
            _deathEvent = deathEvent;
        }

        public void OnEnable()
        {
            _hitPoints.Subscribe(OnHitPointsChanged);
        }

        public void OnDisable()
        {
            _hitPoints.Unsubscribe(OnHitPointsChanged);
        }

        private void OnHitPointsChanged(int hitPoints)
        {
            if (hitPoints <= 0)
            {
                _deathEvent.Invoke();
            }
        }
    }
}