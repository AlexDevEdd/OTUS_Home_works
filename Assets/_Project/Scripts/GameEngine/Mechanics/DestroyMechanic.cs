using System;
using System.Threading;
using _Project.Scripts.Tools;
using Cysharp.Threading.Tasks;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class DestroyMechanic
    {
        private readonly IAtomicVariable<float> _lifeTime;
        private readonly IAtomicEvent _collisionEvent;
        private readonly GameObject _gameObject;

        private CancellationTokenSource _token;
        public DestroyMechanic(GameObject go, IAtomicEvent collisionEvent, IAtomicVariable<float> lifeTime)
        {
            _gameObject = go;
            _collisionEvent = collisionEvent;
            _lifeTime = lifeTime;
        }
        

        public void OnEnable()
        {
            _collisionEvent.Subscribe(Destroy);
            StartRegeneration().Forget();
        }

        public void OnDisable()
        {
            _collisionEvent.Unsubscribe(Destroy);
        }

        private async UniTaskVoid StartRegeneration()
        {
            _token = new CancellationTokenSource();
            
            await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime.Value), cancellationToken: _token.Token);
            Destroy();
        }

        private void Destroy()
        {
            _token?.Cancel();
            _token?.Dispose();
            _collisionEvent.Unsubscribe(Destroy);
            _gameObject.Destroy();
        }
    }
}