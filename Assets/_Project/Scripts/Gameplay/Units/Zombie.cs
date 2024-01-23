using Atomic.Behaviours;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public sealed class Zombie : AtomicBehaviour
    {
        [Section]
        [SerializeField]
        private Zombie_Core _core;

        [Section]
        [SerializeField]
        private Zombie_View _view;
        
        public override void Compose()
        {
            base.Compose();
            
            _core.Compose();
            _view.Compose(_core);
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