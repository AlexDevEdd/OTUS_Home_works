using System;
using _Project.Scripts.GameEngine;
using _Project.Scripts.GameEngine.Animator;
using Atomic.Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Gameplay.Character
{
    [Serializable]
    public sealed class Character_View
    {
        [FormerlySerializedAs("animator")]
        [Header("Animator")]
        [Get(ObjectAPI.Animator)]
        [SerializeField]
        private Animator _animator;

        [FormerlySerializedAs("shootVFX")]
        [Header("VFX")]
        [SerializeField]
        private ParticleSystem _shootVFX;

        [FormerlySerializedAs("audioSource")]
        [Header("Audio")]
        [SerializeField]
        private AudioSource _audioSource;

        [FormerlySerializedAs("shootSFX")] [SerializeField]
        private AudioClip _shootSFX;

        [FormerlySerializedAs("deathSFX")] [SerializeField]
        private AudioClip _deathSFX;
        
        private MoveAnimatorController _movingAnimatorController;
        private DeathAnimatorTrigger _deathAnimatorTrigger;
        
        public void Compose(Character_Core core)
        { 
            _movingAnimatorController = new MoveAnimatorController(_animator, core.MoveComponent.IsMoving);
            _deathAnimatorTrigger = new DeathAnimatorTrigger(_animator, core.HealthComponent.DeathEvent);
            
            core.FireComponent.FireEvent.Subscribe(() => _shootVFX.Play(withChildren: true));
            core.FireComponent.FireEvent.Subscribe(() => _audioSource.PlayOneShot(_shootSFX));
            core.HealthComponent.DeathEvent.Subscribe(() => _audioSource.PlayOneShot(_deathSFX));
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