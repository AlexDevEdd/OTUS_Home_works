using System;
using _Project.Scripts.GameEngine.Animator;
using _Project.Scripts.Gameplay.Character;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class Character_View
    {
        [Header("Animator")]
        [Get(ObjectAPI.Animator)]
        [SerializeField]
        private Animator animator;

        [Header("VFX")]
        [SerializeField]
        private ParticleSystem shootVFX;

        [Header("Audio")]
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip shootSFX;

        [SerializeField]
        private AudioClip deathSFX;
        
        private MoveAnimatorController movingAnimatorController;
        private DeathAnimatorTrigger deathAnimatorTrigger;
        
        public void Compose(Character_Core core)
        { 
            this.movingAnimatorController = new MoveAnimatorController(this.animator, core.MoveComponent.IsMoving);
            this.deathAnimatorTrigger = new DeathAnimatorTrigger(this.animator, core.HealthComponent.deathEvent);
            
            core.FireComponent.FireEvent.Subscribe(() => this.shootVFX.Play(withChildren: true));
            core.FireComponent.FireEvent.Subscribe(() => this.audioSource.PlayOneShot(this.shootSFX));
            core.HealthComponent.deathEvent.Subscribe(() => this.audioSource.PlayOneShot(this.deathSFX));
        }

        public void OnEnable()
        {
            this.deathAnimatorTrigger.OnEnable();
        }

        public void OnDisable()
        {
            this.deathAnimatorTrigger.OnDisable();
        }

        public void Update()
        {
             this.movingAnimatorController.OnUpdate();
        }
    }
}