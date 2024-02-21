﻿using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class SpawnSoulVfxListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnSoulEvent, Inactive>> _filter;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsCustomInject<VfxSystem> _vfxSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var transform = _transformViewPool.Value.Get(entity);
                _filter.Pools.Inc1.Del(entity);
                _vfxSystem.Value.PlayFx(VfxType.Soul, transform.Value.position);
            }
        }
    }
}