using _Project.Scripts.GameEngine.Interfaces;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Objects.Scripts;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public sealed class Zombie : AtomicBehaviour, IDamagable, IZombie
    {
        [Section]
        [SerializeField]
        public Zombie_Core _core;

        [Section]
        [SerializeField]
        private Zombie_View _view;
        
        public IAtomicValue<bool> IsAlive 
            => _core.HealthComponent.IsAlive;
        
        public IAtomicAction<int> OnTakeDamage 
            => _core.HealthComponent.TakeDamageEvent;
        
        public IAtomicEvent<IAtomicObject> DeSpawnEvent 
            => _core.HealthComponent.DeSpawnEvent;
       
        public void Init(Transform target, IAudioSystem audioSystem)
        {
            _core.Compose(target, this);
            _view.Compose(_core, audioSystem);
            _core.OnEnable();
            _view.OnEnable();
        }

        private void Awake()
        {
            Compose();
        }
        
        protected override void OnDisable()
        {
            _core?.OnDisable();
            _view?.OnDisable();
        }

        protected override void Update()
        {
            base.Update();
            
            _core.Update();
            _view.Update();
        }
        
        private void OnDestroy()
        {
            _core.Dispose();
        }
    }
}