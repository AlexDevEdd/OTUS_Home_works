using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace _Project.Scripts.GameEngine.Interfaces
{
    public interface IDamagable
    {
        IAtomicValue<bool> IsAlive { get; }
        public IAtomicAction<int> OnTakeDamage { get; }
    }
}