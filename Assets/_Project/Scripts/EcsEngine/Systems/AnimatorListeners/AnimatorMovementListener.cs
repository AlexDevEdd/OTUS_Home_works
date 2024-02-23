using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.AnimatorListeners
{
    internal sealed class AnimatorMovementListener : IEcsRunSystem
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Move = Animator.StringToHash("Move");

        private readonly EcsFilterInject<Inc<AnimatorView, MoveDirection>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<AnimatorView> _animatorPool;
        private readonly EcsPoolInject<MoveDirection> _moveDirectionPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var animator = _animatorPool.Value.Get(entity);
                var moveDirection = _moveDirectionPool.Value.Get(entity);
                 
                animator.Value.SetTrigger(moveDirection.Value.normalized == Vector3.zero ? Idle : Move);
            }
        }
    }
}