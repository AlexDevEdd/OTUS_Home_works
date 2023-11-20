using System;
using Common;
using Common.Interfaces;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour, IRemovable
    {
        public event Action<Bullet> OnRemoveBullet;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ApplyDamage(_damage);
                OnRemoveBullet?.Invoke(this);
            }
        } 

        public void SetDamage(int damage) 
            => _damage = damage;
        
        public void SetPhysicsLayer(int physicsLayer) 
            => gameObject.layer = physicsLayer;

        public void SetPosition(Vector3 position) 
            => transform.position = position;
        
        public void SetVelocity(Vector2 velocity) 
            => _rigidbody2D.velocity = velocity;

        public void SetColor(Color color) 
            => _spriteRenderer.color = color;

        public void InvokeRemoveCallback() 
            => OnRemoveBullet?.Invoke(this);
    }
}
