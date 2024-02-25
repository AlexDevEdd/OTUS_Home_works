using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine._OOP.Factories.Units
{
    public abstract class BaseUnitFactory : IConcreteUnitFactory
    {
        private readonly Pool<Entity> _units;
        private readonly PrefabProvider _prefabProvider;
        private readonly EntityManager _entityManager;
        public string PrefabKey { get; private set; }
        protected abstract int FactoryIndex { get; }
        
        protected BaseUnitFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance)
        {
            _entityManager = entityManager;
            
            var config = balance.UnitFactoryConfigs[FactoryIndex];
            PrefabKey = config.PrefabKey;
            
            var prefab = prefabProvider.GetPrefab(PrefabKey);
            _units = new Pool<Entity>(prefab, config.PoolSize, prefabProvider.PoolsContainer);
        }
        
        public Entity Spawn()
        {
            return _units.Spawn();
        }

        public void DeSpawn(int id)
        {
            var entity = _entityManager.UnRegister(id);
            _units.DeSpawn(entity);
        }
    }
}