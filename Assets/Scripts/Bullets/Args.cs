using UnityEngine;

namespace Bullets
{
    public struct Args
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Color;
        public int PhysicsLayer;
        public int Damage;
        public float Speed;
        public bool IsPlayer;
    }
}