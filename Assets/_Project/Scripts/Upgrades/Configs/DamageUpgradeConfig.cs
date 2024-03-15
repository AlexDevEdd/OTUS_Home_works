using _Project.Scripts.Entities;
using _Project.Scripts.Upgrades.Stats;
using UnityEngine;

namespace _Project.Scripts.Upgrades.Configs
{
    [CreateAssetMenu(fileName = "DamageUpgradeConfig", menuName = "Upgrades/DamageUpgradeConfig")]
    public class DamageUpgradeConfig : UpgradeConfig
    {
        public override Upgrade InstantiateUpgrade(IEntity entity)
        {
            return new DamageUpgrade(this, entity);
        }
        
        public override float GetStatValue(int level)
        {
            return StatUpgradeTable.GetStatValue(level);
        }
    }
}