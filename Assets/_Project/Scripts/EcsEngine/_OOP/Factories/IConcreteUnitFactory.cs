using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine._OOP.Factories
{
    public interface IConcreteUnitFactory
    {
        public string PrefabKey { get; }
        public Entity Spawn();
        public void DeSpawn(int id);
    }
}