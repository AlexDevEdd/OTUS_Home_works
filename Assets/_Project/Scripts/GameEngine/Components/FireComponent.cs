using System;
using _Project.Scripts.GameEngine.Actions;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    public sealed class FireComponent : IDisposable
    {
        public AtomicVariable<bool> _enabled = new(true);
        public AtomicEvent _fireEvent;

        public Transform _firePoint;
        public AtomicObject _bulletPrefab;
        public AtomicVariable<int> _charges = new(10);
        
        [Get(ObjectAPI.FireAction)]
        public FireAction _fireAction;

        [Get("FireCondition")]
        public AndExpression _fireCondition;
        
        public SpawnBulletAction _bulletAction;
        
        public void Compose()
        {
            _fireCondition
                .AddMember(_enabled)
                .AddMember(_charges.AsFunction(it => it.Value > 0));
            
            _fireAction.Compose(_bulletAction, _charges, _fireCondition, _fireEvent);
            _bulletAction.Compose(_firePoint, _bulletPrefab);
        }

        public void Dispose()
        {
            _fireEvent?.Dispose();
            _charges?.Dispose();
        }
    }
}