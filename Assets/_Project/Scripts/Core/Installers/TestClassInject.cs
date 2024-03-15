using System;
using _Project.Scripts.Entities;
using _Project.Scripts.ScriptableConfigs;
using _Project.Scripts.Upgrades;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Installers
{
    public sealed class TestClassInject : MonoBehaviour
    {
        [Inject] private GameBalance _balance;
        [Inject] private PlayerEntity _player;
        [Inject] private PlayerUpgradeSystem _upgradeSystem;
        [Inject] private MoneyStorage _moneyStorage;
        
        [Button]
        public void AddMoney(int value = 400)
        {
           _moneyStorage.EarnMoney(value);
        }
        
        [Button]
        public void UpgradeSpeed()
        {
            _upgradeSystem.LevelUp(StatType.Speed);
        }
        
        [Button]
        public void UpgradeHealth()
        {
            _upgradeSystem.LevelUp(StatType.Health);
        }
        [Button]
        public void UpgradeDamage()
        {
            _upgradeSystem.LevelUp(StatType.Damage);
        }
    }
}