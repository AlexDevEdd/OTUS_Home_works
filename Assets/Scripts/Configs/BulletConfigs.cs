using System;
using System.Collections.Generic;
using System.Linq;
using Bullets;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class BulletConfigs
    {
        [SerializeField] private List<BulletConfig> _bulletConfigs;

        public BulletConfig GetConfigByType(TeamType type)
        {
            var config = _bulletConfigs.FirstOrDefault(c => c.Type == type);
            if (config != null)
                return config;

            throw new ArgumentException($"does not exist config type of {type}");
        }
    }
}