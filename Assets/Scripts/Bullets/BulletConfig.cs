using ShootEmUp;
using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField] private BulletType _type;
        [SerializeField] private PhysicsLayer _physicsLayer;
        [SerializeField] private Color _color;
        [SerializeField] private int _damage;

        public BulletType Type => _type;
        public PhysicsLayer PhysicsLayer => _physicsLayer;

        public Color Color => _color;

        public int Damage => _damage;
    }
}