using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Objects.Scripts;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class DeSpawnMechanics : IDisposable
    {
        private readonly IAtomicEvent<IAtomicObject> _deSpawnEvent;
        private readonly IAtomicValue<float> _deSpawnDelay;
        private readonly IAtomicObject _atomicObject;
        private readonly IAtomicEvent _deathEvent;

        private CancellationTokenSource _token;
        
        public DeSpawnMechanics(IAtomicObject atomicObject, IAtomicValue<float> deSpawnDelay,
            IAtomicEvent deathEvent, IAtomicEvent<IAtomicObject> deSpawnEvent)
        {
            _atomicObject = atomicObject;
            _deSpawnDelay = deSpawnDelay;
            _deathEvent = deathEvent;
            _deSpawnEvent = deSpawnEvent;
        }

        public void OnEnable()
        {
            _deathEvent.Subscribe(OnDeSpawn);
        }
        
        public void OnDisable()
        {
            _deathEvent.Unsubscribe(OnDeSpawn);
        }
        
        private void OnDeSpawn()
        {
            DeSpawnDelayAsync().Forget();
        }

        private async UniTaskVoid DeSpawnDelayAsync()
        {
            _token = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(_deSpawnDelay.Value), cancellationToken: _token.Token);
            _deSpawnEvent?.Invoke(_atomicObject);
        }

        public void Dispose()
        {
            _token?.Cancel();
            _token?.Dispose();
        }
    }
}