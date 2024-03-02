using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.AnimatorListeners
{
    internal sealed class AnimatorAttackListener : IEcsRunSystem
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        private readonly EcsFilterInject<Inc<AnimatorView, AttackEvent>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<AnimatorView> _animatorPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _animatorPool.Value.Get(entity).Value.SetTrigger(Attack);
            }
        }
    }
    
    internal sealed class AnimatorEnableColliderListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AnimatorView, WeaponColliderView, EnableColliderEvent>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<EnableColliderEvent> _eventPool;
        private readonly EcsPoolInject<WeaponColliderView> _colliderViewPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _eventPool.Value.Del(entity);
                ref var colliderView = ref _colliderViewPool.Value.Get(entity);
                colliderView.Value.enabled = true;
            }
        }
    }
    
    internal sealed class AnimatorDisableColliderListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AnimatorView, WeaponColliderView, DisableColliderEvent>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<DisableColliderEvent> _eventPool;
        private readonly EcsPoolInject<WeaponColliderView> _colliderViewPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _eventPool.Value.Del(entity);
                ref var colliderView = ref _colliderViewPool.Value.Get(entity);
                colliderView.Value.enabled = false;
            }
        }
    }
}