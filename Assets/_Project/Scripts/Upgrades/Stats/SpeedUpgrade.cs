using _Project.Scripts.Components;
using _Project.Scripts.Entities;
using _Project.Scripts.Upgrades.Configs;
using JetBrains.Annotations;

namespace _Project.Scripts.Upgrades.Stats
{
    [UsedImplicitly]
    public sealed class SpeedUpgrade : Upgrade
    {
        private readonly IEntity _player;

        public SpeedUpgrade(UpgradeConfig config, IEntity player) : base(config)
        {
            _player = player;
        }

        protected override void LevelUp(int level)
        {
            var moveComponent = _player.Get<PlayerMoveComponent>();
            moveComponent.UpgradeStat(GetStatValue(level));
        }
    }
}