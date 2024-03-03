using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Scripts.EcsEngine._OOP.Factories.Units;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using _Project.Scripts.EcsEngine.Enums;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.Systems
{
    [UsedImplicitly]
    public sealed class UnitSystem : ICustomInject, IDisposable
    {
        private const float FIND_TARGET_DELAY = 1;
        
        private readonly UnitFactory _unitFactory;
        
        private List<CancellationTokenSource> _tokens = new ();
        
        public UnitSystem(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public async UniTaskVoid Spawn(UnitType type, TeamType teamType, Vector3 position, Quaternion rotation)
        {
            var token = new CancellationTokenSource();
            _tokens.Add(token);
            
            var entity = _unitFactory.Spawn(type, teamType, position, rotation);
            
            await UniTask.Delay(TimeSpan.FromSeconds(FIND_TARGET_DELAY), cancellationToken: token.Token);
            
            _tokens.Remove(token);
            token.Dispose();
            
            entity.WithData(new FindTargetRequest());
        }

        public void DeSpawn(int id)
        {
            _unitFactory.DeSpawn(id);
        }

        public bool IsHasTeam(TeamType teamType)
        {
            return _unitFactory.ActiveUnits.Any(u => u.GetData<Team>().Value == teamType);
        }

        public Entity GetClosestByTeam(TransformView ownerTransform, TeamType teamType)
        {
            var entity = _unitFactory.ActiveUnits
                .Where(e => e.GetData<Team>().Value == teamType && !e.HasData<Inactive>())
                .OrderBy(e => Vector3.Distance(ownerTransform.Value.position, e.transform.position))
                .FirstOrDefault();
            
            return entity;
        }

        public void Dispose()
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