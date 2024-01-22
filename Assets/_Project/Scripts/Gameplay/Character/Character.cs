using Atomic.Behaviours;
using Atomic.Objects;
using Sample;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Character
{
    public sealed class Character : AtomicBehaviour
    {
        [Section]
        [SerializeField]
        private Character_Core _core;

        [Section]
        [SerializeField]
        private Character_View _view;
        
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