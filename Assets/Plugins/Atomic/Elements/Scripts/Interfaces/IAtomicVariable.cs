using Atomic.Elements;

namespace Plugins.Atomic.Elements.Scripts.Interfaces
{
    public interface IAtomicVariable<T> : IAtomicValue<T>
    {
        new T Value { get; set; }
    }
}