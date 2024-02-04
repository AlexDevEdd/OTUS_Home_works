using System;
using _Project.Scripts.GameEngine;
using _Project.Scripts.GameEngine.Animator;
using _Project.Scripts.GameEngine.Enums;
using _Project.Scripts.GameEngine.Interfaces;
using Atomic.Objects;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Gameplay.Character
{
    [Serializable]
    public sealed class Character_View
    {
        [Header("Animator")]
        [Get(ObjectAPI.Animator)]
        [SerializeField]
        private Animator _animator;
        
        [Header("VFX")]
        [SerializeField]
        private ParticleSystem _shootVFX;
        
        [Header("Audio")]
        [SerializeField]
        private AudioSource _audioSource;
        
        private MoveAnimatorController _movingAnimatorController;
        private DeathAnimatorTrigger _deathAnimatorTrigger;
        
        public void Compose(Character_Core core, IAudioSystem audioSystem)
        {
            _movingAnimatorController = new MoveAnimatorController(_animator, core.MoveComponent.IsMoving);
            _deathAnimatorTrigger = new DeathAnimatorTrigger(_animator, core.HealthComponent.DeathEvent);
            
            core.FireComponent.FireEvent.Subscribe(() => _shootVFX.Play(withChildren: true));
            core.FireComponent.FireEvent.Subscribe(() => audioSystem.PlayAudio(_audioSource, SfxType.Shoot));
            core.HealthComponent.DeathEvent.Subscribe(() => audioSystem.PlayAudio(_audioSource, SfxType.DeathHuman));
        }

        public void OnEnable()
        {
            _deathAnimatorTrigger.OnEnable();
        }

        public void OnDisable()
        {
            _deathAnimatorTrigger.OnDisable();
        }

        public void Update()
        {
             _movingAnimatorController.Update();
        }
    }
}