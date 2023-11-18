using System;
using Components;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BulletMoveComponent _moveComponent;
        
        public int Damage { get; private set; }
        
        private void OnCollisionEnter2D(Collision2D collision) 
            => OnCollisionEntered?.Invoke(this, collision);

        public void SetDamage(int damage) 
            => Damage = damage;
        
        public void SetPhysicsLayer(int physicsLayer) 
            => gameObject.layer = physicsLayer;

        public void SetPosition(Vector3 position) 
            => transform.position = position;

        public void SetColor(Color color) 
            => _spriteRenderer.color = color;

        public void SetSpeed(float speed)
            => _moveComponent.SetSpeed(speed);
        
        public void SetIsPlayer(bool isPlayer)
            => _moveComponent.SetIsPlayer(isPlayer);

        public void UpdatePhysics(float fixedDeltaTime) 
            => _moveComponent.UpdatePhysics(fixedDeltaTime);
    }
}
