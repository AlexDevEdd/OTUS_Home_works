using System;
using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace Plugins.Atomic.Elements.Scripts.Implementations
{
    [Serializable]
    public sealed class AtomicObservable : IAtomicObservable
    {
        private Action<Action> _subscribe;
        private Action<Action> _unsubscribe;

        public AtomicObservable()
        {
        }

        public AtomicObservable(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            _subscribe = subscribe;
            _unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            _subscribe = subscribe;
            _unsubscribe = unsubscribe;
        }

        public void Subscribe(Action action)
        {
            _subscribe.Invoke(action);
        }

        public void Unsubscribe(Action action)
        {
            _unsubscribe.Invoke(action);
        }
    }

    [Serializable]
    public sealed class AtomicObservable<T> : IAtomicObservable<T>
    {
        private Action<Action<T>> _subscribe;
        private Action<Action<T>> _unsubscribe;

        public AtomicObservable()
        {
        }

        public AtomicObservable(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            _subscribe = subscribe;
            _unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            _subscribe = subscribe;
            _unsubscribe = unsubscribe;
        }

        public void Subscribe(Action<T> action)
        {
            _subscribe.Invoke(action);
        }

        public void Unsubscribe(Action<T> action)
        {
            _unsubscribe.Invoke(action);
        }
    }
}