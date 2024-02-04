using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Factories
{
    public class Pool<T>  where T: MonoBehaviour
    {
        private readonly Queue<T> _pool = new();
        private readonly HashSet<T> _actives = new();
        
        private readonly T _prefab;
        private readonly int _size;
        
        private Transform _container;

        public Pool(T prefab, int size)
        {
            _prefab = prefab;
            _size = size;
            Init();
        }

        private void Init()
        {
            for (var i = 0; i < _size; i++)
            {
                if (_container == null) 
                    _container = new GameObject($"{typeof(T)}s").transform;

                var obj = CreateObj();
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        public T Spawn()
        {
            if(_pool.TryDequeue(out var obj))
            {
                _actives.Add(obj);
                obj.gameObject.SetActive(true);
                return obj;
            }

            obj = CreateObj();
            obj.gameObject.SetActive(true);
            _actives.Add(obj);
            return obj;
        }
        
        public void DeSpawn(T obj)
        {
            if (_actives.Remove(obj))
            {
                obj.transform.SetParent(_container);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }
        
        private T CreateObj()
            => Object.Instantiate(_prefab, _container);
    }
}