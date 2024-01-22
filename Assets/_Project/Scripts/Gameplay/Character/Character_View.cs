using System;
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
            this.deathAnimatorTrigger = new DeathAnimatorTrigger(this.animator, core._healthComponent.deathEvent);
            
            core._fireComponent._fireEvent.Subscribe(() => this.shootVFX.Play(withChildren: true));
            core._fireComponent._fireEvent.Subscribe(() => this.audioSource.PlayOneShot(this.shootSFX));
            core._healthComponent.deathEvent.Subscribe(() => this.audioSource.PlayOneShot(this.deathSFX));
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