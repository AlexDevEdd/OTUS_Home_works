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
}