using System;
using Sirenix.OdinInspector;

namespace _Project.Scripts.DI.Installers.ScriptableObjects
{
    [Serializable]
    public class GameBalance
    {
        [Title("Character configuration", TitleAlignment = TitleAlignments.Centered)]
        public CharacterDefaultConfig CharacterDefaultConfig;
        
        [Title("Zombie configuration", TitleAlignment = TitleAlignments.Centered)]
        public ZombieDefaultConfig ZombieDefaultConfig;
        
        [Title("Bullet configuration", TitleAlignment = TitleAlignments.Centered)]
        public BulletDefaultConfig BulletDefaultConfig;
    }
    
    [Serializable]
    public struct CharacterDefaultConfig
    {
        public float MoveSpeed;
        public float RotationSpeed;
        public int Health;
        public int Charges;
        public int MaxCharges;
        public int RegenerationChargesDelay;
        public int RegenerationChargesValue;
    }
    
    [Serializable]
    public struct ZombieDefaultConfig
    {
        public float MoveSpeed;
        public float AttackDistance;
        public float AttackCoolDown;
        public int Health;
        public int Damage;
    }
    
    [Serializable]
    public struct BulletDefaultConfig
    {
        public float MoveSpeed;
        public float LifeTime;
        public int Damage;
    }
}