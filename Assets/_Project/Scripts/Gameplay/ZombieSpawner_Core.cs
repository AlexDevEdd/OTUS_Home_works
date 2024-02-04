using System;
using _Project.Scripts.GameEngine.Components;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    [Serializable]
    public sealed class ZombieSpawner_Core : IDisposable
    {
        [SerializeField] private Transform[] _spawnPoints;

        [Section] 
        public SpawnComponent _spawnComponent;
        
        public void Compose()
        {
          _spawnComponent?.Compose();
        }

        public void OnEnable()
        {
            _spawnComponent?.OnEnable();
        }

        public void OnDisable()
        {
           _spawnComponent?.OnDisable();
        }

        // public void Update()
        // {
        //    
        // }

        public void Dispose()
        {
           _spawnComponent?.Dispose();
        } 
    }
}