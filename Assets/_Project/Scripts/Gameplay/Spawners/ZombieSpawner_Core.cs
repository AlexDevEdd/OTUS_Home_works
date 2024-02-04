using System;
using _Project.Scripts.GameEngine;
using _Project.Scripts.GameEngine.Components;
using Atomic.Objects;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Spawners
{
    [Serializable]
    public sealed class ZombieSpawner_Core : IDisposable
    {
        [Get(ObjectAPI.Transforms)]
        [SerializeField]
        private Transform[] _spawnPoints;

        [Section] 
        public SpawnComponent _spawnComponent;
        
        public void Compose(IAtomicValue<bool> isAlive)
        {
            _spawnComponent?.Compose(isAlive);
        }

        public void OnEnable()
        {
            _spawnComponent?.OnEnable();
        }

        public void OnDisable()
        {
           _spawnComponent?.OnDisable();
        }
        
        public void Dispose()
        {
           _spawnComponent?.Dispose();
        } 
    }
}