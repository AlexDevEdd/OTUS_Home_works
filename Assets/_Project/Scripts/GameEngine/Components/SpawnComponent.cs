using System;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    public class SpawnComponent : IDisposable
    {
        [SerializeField]
        private AtomicVariable<bool> _enabled = new(true);
        
        [SerializeField]
        private AtomicVariable<float> _spawnInterval = new(2);
        
        [Get(ObjectAPI.SpawnEvent)]
        public AtomicEvent SpawnEvent;

        private CycleDelayCallbackMechanics cycleDelayCallbackMechanics;

        public SpawnComponent(AtomicEvent spawnEvent)
        {
            SpawnEvent = spawnEvent;
        }

        public void Compose()
        {
            cycleDelayCallbackMechanics = new CycleDelayCallbackMechanics(_enabled, _spawnInterval, SpawnEvent);
        }
        
        public void OnEnable()
        {
            cycleDelayCallbackMechanics.OnEnable();
        }

        public void OnDisable()
        {
           
        }

        public void Dispose()
        {
            cycleDelayCallbackMechanics?.Dispose();
        }
    }
}