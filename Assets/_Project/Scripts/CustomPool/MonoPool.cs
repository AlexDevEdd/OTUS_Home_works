using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.CustomPool
{
    public class MonoPool<TContract> where TContract : MonoBehaviour
    {
        private readonly Queue<TContract> _pool = new();
        private readonly HashSet<TContract> _actives = new();
        
        private readonly TContract _prefab;
        private readonly int _size;
        
        private Transform _container;

        public MonoPool(TContract prefab, int size, Transform parent)
        {
            _prefab = prefab;
            _size = size;
            Init(parent);
        }

        private void Init(Transform parent)
        {
            for (var i = 0; i < _size; i++)
            {
                if (_container == null)
                {
                    _container = new GameObject($"{_prefab.name}s").transform;
                    _container.SetParent(parent);
                } 

                var item = CreateObj();
                OnCreated(item);
            }
        }

        public TContract Spawn()
        {
            if(_pool.TryDequeue(out var item))
            {
                OnSpawn(item);
                return item;
            }

            item = CreateObj();
            OnSpawn(item);
            return item;
        }
        
        public void DeSpawn(TContract item)
        {
            if (_actives.Remove(item))
            {
                item.transform.SetParent(_container);
                item.gameObject.SetActive(false);
                _pool.Enqueue(item);
            }
            else
            {
                throw new DuplicateWaitObjectException($"trying to remove {item.name} twice");
            }
        }
        
        private TContract CreateObj() 
            => Object.Instantiate(_prefab, _container);

        private void OnCreated(TContract item)
        {
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        } 
        
        private void OnSpawn(TContract item)
        {
            _actives.Add(item);
            item.gameObject.SetActive(true);
        } 
    }
}