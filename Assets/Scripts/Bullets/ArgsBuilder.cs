using ShootEmUp;
using UnityEngine;

namespace Bullets
{
    public class ArgsBuilder
    {
        private Args _args;
        
        public ArgsBuilder()
        {
            _args = new Args();
        }
        
        public ArgsBuilder SetPosition(Vector2 position)
        {
            _args.Position = position;
            return this;
        }
        
        public ArgsBuilder SetVelocity(Quaternion rotation, float speed)
        {
            _args.Velocity = rotation * Vector3.up * speed;
            return this;
        }
        
        public ArgsBuilder SetColor(Color color)
        {
            _args.Color = color;
            return this;
        }
        
        public ArgsBuilder SetPhysicsLayer(PhysicsLayer layer)
        {
            _args.PhysicsLayer = (int)layer;
            return this;
        }
        
        public ArgsBuilder SetDamage(int damage)
        {
            _args.Damage = damage;
            return this;
        }
        
        public ArgsBuilder SetSpeed(float speed)
        {
            _args.Speed = speed;
            return this;
        }

        public ArgsBuilder SetIsPlayer(bool isPlayer)
        {
            _args.IsPlayer = isPlayer;
            return this;
        }

        public Args Build() 
            => _args;
    }
}