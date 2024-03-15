using _Project.Scripts.Entities;
using _Project.Scripts.Upgrades.Stats;
using UnityEngine;

namespace _Project.Scripts.Upgrades.Configs
{
    [CreateAssetMenu(fileName = "SpeedUpgradeConfig", menuName = "Upgrades/SpeedUpgradeConfig")]
    public class SpeedUpgradeConfig : UpgradeConfig
    {
        public override Upgrade InstantiateUpgrade(IEntity entity)
        {
            return new SpeedUpgrade(this, entity);
        }
        
        public override float GetStatValue(int level)
        {
            return StatUpgradeTable.GetLinearInterpolationStatValue(level);
        }
    }
}