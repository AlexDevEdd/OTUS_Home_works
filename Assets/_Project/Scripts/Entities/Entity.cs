using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class Entity : MonoBehaviour, IEntity
    {
        private readonly Dictionary<Type, object> _components = new();
        
        public void Add<T>(T newComponent)
        {
            _components[typeof(T)] = newComponent;
        }
        
        public bool TryComponent<T>(out T component)
        {
            if (_components.TryGetValue(typeof(T), out var value))
            {
                component = (T)value;
                return true;
            }
        
            component = default;
            return false;
        }
        
        public T Get<T>()
        {
            if (_components.TryGetValue(typeof(T), out var value))
            {
                return (T)value;
            }
        
            throw new Exception($"No component of {typeof(T)} found!");
        }
    }
}