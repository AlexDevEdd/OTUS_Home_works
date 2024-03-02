﻿using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content
{
    public class AnimationEventListener : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        
        [UsedImplicitly]
        public void OnDeath()
        {
            _entity.AddData(new DeSpawnRequest());
        }
        
        [UsedImplicitly]
        public void OnShoot()
        {
            if (_entity.HasData<TargetEntity>())
            {
                _entity.AddData(new ShootEvent
                {
                    SourceEntity = _entity.GetData<SourceEntity>(),
                    TargetEntity = _entity.GetData<TargetEntity>()
                });
            }
        }
        
        [UsedImplicitly]
        public void OnEnableCollider()
        {
            _entity.AddData(new EnableColliderEvent());
        }
        
        [UsedImplicitly]
        public void OnDisableCollider()
        {
            _entity.AddData(new DisableColliderEvent());
        }
    }
}