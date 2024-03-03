using System;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class SpawnUnitSystem : IEcsRunSystem , IEcsDestroySystem
    {
        private EcsFilterInject<Inc<SpawnerTag, UnitSpawnRequest>> _filter;
        
        private readonly EcsPoolInject<UnitSpawnRequest> _requestPool;
        private readonly EcsPoolInject<UnitSpawnEvent> _eventPool;
        
        private readonly EcsCustomInject<UnitSystem> _unitSystem;
        
        private List<CancellationTokenSource> _tokens = new ();
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var request = _requestPool.Value.Get(entity);
                _eventPool.Value.Add(entity) = new UnitSpawnEvent{Position = request.Position};
                SpawnUnit(request).Forget();
                _requestPool.Value.Del(entity);
            }
        }

        private async UniTaskVoid SpawnUnit(UnitSpawnRequest request)
        {
            var token = new CancellationTokenSource();
            _tokens.Add(token);

            await UniTask.Delay(TimeSpan.FromSeconds(request.PrepareTime), cancellationToken: token.Token);
                
            _unitSystem.Value.Spawn(request.UnitType, request.TeamType, request.Position, request.Rotation).Forget();
            
            _tokens.Remove(token);
            token.Dispose();
        }
        
        public void Destroy(IEcsSystems systems)
        {
            for (var i = 0; i < _tokens.Count; i++)
            {
                _tokens[i].Cancel();
                _tokens[i].Dispose();
            }

            _tokens = null;
        }
    }
}