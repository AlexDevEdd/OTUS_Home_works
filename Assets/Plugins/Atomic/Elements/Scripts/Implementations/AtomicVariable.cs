using System;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicVariable<T> : IAtomicVariable<T>, IAtomicObservable<T>, IDisposable
    {
        public T Value
        {
            get { return _value; }
            set
            {
                if(Equals(_value, value)) 
                    return;
                
                _value = value;
                onChanged?.Invoke(value);
            }
        }

        public void Subscribe(Action<T> listener)
        {
            onChanged += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            onChanged -= listener;
        }

        private Action<T> onChanged;

        [FormerlySerializedAs("value")]
        [OnValueChanged("OnValueChanged")]
        [SerializeField]
        private T _value;

        public AtomicVariable()
        {
            _value = default;
        }

        public AtomicVariable(T value)
        {
            _value = value;
        }

#if UNITY_EDITOR
        private void OnValueChanged(T value)
        {
            onChanged?.Invoke(value);
        }
#endif
        public void Dispose()
        {
            AtomicUtils.Dispose(ref onChanged);
        }
    }
}