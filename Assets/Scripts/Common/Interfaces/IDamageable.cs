namespace Common.Interfaces
{
    internal interface IDamageable
    {
        public bool IsPlayer { get; }
        public void ApplyDamage(float damage);
    }
}