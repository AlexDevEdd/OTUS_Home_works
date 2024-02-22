using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine._OOP.Factories
{
    [UsedImplicitly]
    public sealed class WarriorBlueFactory : BaseUnitFactory
    {
        protected override int FactoryIndex => 1;

        public WarriorBlueFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance) 
            : base(entityManager, prefabProvider, balance) { }
    }
}