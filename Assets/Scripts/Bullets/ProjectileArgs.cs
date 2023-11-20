﻿using UnityEngine;

namespace Bullets
{
    public struct ProjectileArgs
    {
        public readonly Vector2 Position;
        public readonly Vector2 Velocity;
        public readonly Color Color;
        public readonly int PhysicsLayer;
        public readonly int Damage;
        
        public ProjectileArgs(Vector2 position, Vector2 velocity, Color color, int physicsLayer, int damage)
        {
            Position = position;
            Velocity = velocity;
            Color = color;
            PhysicsLayer = physicsLayer;
            Damage = damage;
        }
    }
}