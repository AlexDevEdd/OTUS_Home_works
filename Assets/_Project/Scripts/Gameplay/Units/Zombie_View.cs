using System;
using _Project.Scripts.GameEngine;
using _Project.Scripts.GameEngine.Animator;
using _Project.Scripts.GameEngine.Enums;
using _Project.Scripts.GameEngine.Interfaces;
using Atomic.Objects;
using UnityEngine;


namespace _Project.Scripts.Gameplay.Units
{
    [Serializable]
    public sealed class Zombie_View
    {
        [Get(ObjectAPI.Animator)]
        [Header("Animator")] [SerializeField]
        private Animator _animator;
        
        [Header("Audio")] [SerializeField]
        private AudioSource _audioSource;
        
        private AttackAnimatorController _attackAnimatorController;
        private MoveAnimatorController _movingAnimatorController;
        private DeathAnimatorTrigger _deathAnimatorTrigger;
        
        public void Compose(Zombie_Core core, IAudioSystem audioSystem)
        {
            _attackAnimatorController = new AttackAnimatorController(_animator, core.AttackMeleeComponent.AttackEvent);
            _movingAnimatorController = new MoveAnimatorController(_animator, core.TargetMoveComponent.IsMoving);
            _deathAnimatorTrigger = new DeathAnimatorTrigger(_animator, core.HealthComponent.DeathEvent);
            
            core.HealthComponent.DeathEvent.Subscribe(() => audioSystem.PlayAudio(_audioSource, SfxType.DeathZombie));
        }

        public void OnEnable()
        {
            _deathAnimatorTrigger.OnEnable();
            _attackAnimatorController.OnEnable();
        }

        public void OnDisable()
        {
            _deathAnimatorTrigger.OnDisable();
            _attackAnimatorController.OnDisable();
        }

        public void Update()
        {
            _movingAnimatorController.Update();
        }
    }
}