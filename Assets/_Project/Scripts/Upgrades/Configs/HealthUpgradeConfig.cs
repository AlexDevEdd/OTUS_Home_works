using _Project.Scripts.Entities;
using _Project.Scripts.Upgrades.Stats;
using UnityEngine;

namespace _Project.Scripts.Upgrades.Configs
{
    [CreateAssetMenu(fileName = "HealthUpgradeConfig", menuName = "Upgrades/HealthUpgradeConfig")]
    public class HealthUpgradeConfig : UpgradeConfig
    {
        public override Upgrade InstantiateUpgrade(IEntity entity)
        {
            return new HealthUpgrade(this, entity);
        }
        
        public override float GetStatValue(int level)
        {
            return StatUpgradeTable.GetStatValue(level);
        }
    }
}