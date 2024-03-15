using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Entities;
using _Project.Scripts.ScriptableConfigs;
using _Project.Scripts.Upgrades.Stats;
using JetBrains.Annotations;

namespace _Project.Scripts.Upgrades
{
    [UsedImplicitly]
    public sealed class PlayerUpgradeStatFactory
    {
        private readonly IEntity _player;
        private readonly GameBalance _balance;

        public PlayerUpgradeStatFactory(IEntity player, GameBalance balance)
        {
            _player = player;
            _balance = balance;
        }

        public IEnumerable<Upgrade> CreateAllUpgrades()
        {
            var configs = _balance.GetAllUpgradeConfigs();
            return configs.Select(config => config.InstantiateUpgrade(_player));
        }

        public Upgrade CreateUpgrade(StatType id)
        {
            var config = _balance.FindUpgradeConfig(id);
            
            if (config == null)
                throw new ArgumentException($"No upgrade configuration found for id: {id}");

            return config.InstantiateUpgrade(_player);
        }
    }
}