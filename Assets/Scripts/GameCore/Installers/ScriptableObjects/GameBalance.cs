using System;
using Configs;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Installers.ScriptableObjects
{
    [Serializable]
    public class GameBalance
    {
        [Title("Enemy configuration", TitleAlignment = TitleAlignments.Centered)]
        public EnemyConfig EnemyConfig;
        
        [Space, Title("Bullet configurations", TitleAlignment = TitleAlignments.Centered)]
        public BulletConfigs BulletConfigs;
    }
}