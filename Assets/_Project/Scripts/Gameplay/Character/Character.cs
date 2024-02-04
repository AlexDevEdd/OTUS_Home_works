using _Project.Scripts.GameEngine.Interfaces;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Character
{
    public sealed class Character : AtomicBehaviour, IDamagable, ICharacter
    {
        [Section]
        [SerializeField]
        private Character_Core _core;

        [Section]
        [SerializeField]
        private Character_View _view;

        private IAudioSystem _audioSystem;
        
        public IAtomicValue<bool> IsAlive 
            => _core.HealthComponent.IsAlive;

        public IAtomicAction<int> OnTakeDamage 
            => _core.HealthComponent.TakeDamageEvent;
        
        [Inject]
        public void Construct(IAudioSystem audioSystem)
        {
            _audioSystem = audioSystem;
        }
        
        public override void Compose()
        {
            base.Compose();
            
            _core.Compose();
            _view.Compose(_core, _audioSystem);
        }

        private void Awake()
        {
            Compose();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _core.OnEnable();
            _view.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _core.OnDisable();
            _view.OnDisable();
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