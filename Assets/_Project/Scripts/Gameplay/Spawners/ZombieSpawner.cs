using _Project.Scripts.GameEngine.Interfaces;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Spawners
{
    public class ZombieSpawner : AtomicBehaviour, IZombieSpawner
    {
        [Section]
        [SerializeField]
        private ZombieSpawner_Core _spawnerCore;

        private Character.Character _character; 
        
        [Inject]
        public void Construct(ICharacter character)
        {
            _character = character as Character.Character;
        }
        
        public override void Compose()
        {
            base.Compose();
            _spawnerCore.Compose(_character.IsAlive);
        }

        private void Awake()
        {
            Compose();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _spawnerCore.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _spawnerCore.OnDisable();
        }
        
        private void OnDestroy()
        {
            _spawnerCore.Dispose();
        }
    }
}