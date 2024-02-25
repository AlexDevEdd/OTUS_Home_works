using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content
{
    public class AnimationEventListener : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        
        public void OnDeath()
        {
            _entity.AddData(new DeSpawnRequest());
        }
        
        public void OnShoot()
        {
            _entity.AddData(new ShootEvent
            {
                SourceEntity = _entity.GetData<SourceEntity>(),
                TargetEntity = _entity.GetData<TargetEntity>()
            });
        }
    }
}