using System;
using _Project.Scripts.GameEngine.Actions;
using _Project.Scripts.GameEngine.Functions;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    public sealed class FireComponent : IDisposable
    {
        public AtomicVariable<bool> Enabled = new(true);
        public AtomicEvent FireEvent;

        public Transform FirePoint;
        public AtomicObject BulletPrefab;
        public AtomicVariable<int> Charges = new(10);
        public AtomicValue<int> MaxCharges = new(30);
        
        [Get(ObjectAPI.FireAction)]
        public FireAction FireAction;

        [Get("FireCondition")]
        public AndExpression FireCondition;
        
        public SpawnBulletAction BulletAction;
        
        public void Compose()
        {
            FireCondition
                .AddMember(Enabled)
                .AddMember(Charges.AsFunction(it => it.Value > 0));
            
            FireAction.Compose(BulletAction, Charges, FireCondition, FireEvent);
            BulletAction.Compose(FirePoint, BulletPrefab);
        }

        public void Dispose()
        {
            FireEvent?.Dispose();
            Charges?.Dispose();
        }
    }
}