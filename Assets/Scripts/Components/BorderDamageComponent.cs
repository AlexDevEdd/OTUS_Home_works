using Bullets;
using UnityEngine;

namespace Components
{
    public sealed class BorderDamageComponent : MonoBehaviour
    {
        [SerializeField] private float _damage = 1000f;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
                damageable.ApplyDamage(_damage);
        } 
    }
}