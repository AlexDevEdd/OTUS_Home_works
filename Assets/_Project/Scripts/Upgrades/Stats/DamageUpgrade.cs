using _Project.Scripts.Components;
using _Project.Scripts.Entities;
using _Project.Scripts.Upgrades.Configs;
using JetBrains.Annotations;

namespace _Project.Scripts.Upgrades.Stats
{
    [UsedImplicitly]
    public sealed class DamageUpgrade : Upgrade
    {
        private readonly IEntity _player;
        
        public DamageUpgrade(UpgradeConfig config, IEntity player) : base(config)
        {
            _player = player;
        }

        protected override void LevelUp(int level)
        {
            var damageComponent = _player.Get<PlayerDamageComponent>();
            damageComponent.UpgradeStat(GetStatValue(level));
        }
    }
}