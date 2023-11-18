using ShootEmUp;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(
        fileName = "EnemyConfig",
        menuName = "Enemy/New EnemyConfig"
    )]
    public sealed class EnemyConfig : ScriptableObject
    {
        [SerializeField] private PhysicsLayer _physicsLayer;
        [SerializeField] private float _speed;
        [SerializeField] private float _health;
        
        public PhysicsLayer PhysicsLayer => _physicsLayer;
        
        public float Speed => _speed;

        public float Health => _health;
    }
}