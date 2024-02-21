using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class UnitRotationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Rotation, Position, TargetEntity>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<Rotation> _rotationPool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        
        public void Run(IEcsSystems systems)
        {
            var targetPool = _targetPool.Value;
            var rotationPool = _rotationPool.Value;
            var positionPool = _positionPool.Value;
            
            foreach (var entity in _filter.Value)
            {
                var target = targetPool.Get(entity);
                if(target.Id == default)
                    continue;
                
                ref var rotation = ref rotationPool.Get(entity);
                
                var direction = target.Transform.position - positionPool.Get(entity).Value;
                var targetRotation = Quaternion.LookRotation(direction);
                rotation.Value = targetRotation;

            }
        }
    }
}