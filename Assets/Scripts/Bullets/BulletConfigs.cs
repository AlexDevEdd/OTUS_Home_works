using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(
        fileName = "BulletConfigs",
        menuName = "BulletConfigs/New BulletConfigs"
    )]
    public sealed class BulletConfigs : ScriptableObject
    {
        [SerializeField] private List<BulletConfig> _bulletConfigs;

        public BulletConfig GetConfigByType(BulletType type)
            => _bulletConfigs.FirstOrDefault(c => c.Type == type);
    }
}