using Bullets;
using UnityEngine;

namespace Components
{
    public sealed class DamageComponent : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
                damageable.ApplyDamage(Constants.TotalDamage);
        }
    }
}