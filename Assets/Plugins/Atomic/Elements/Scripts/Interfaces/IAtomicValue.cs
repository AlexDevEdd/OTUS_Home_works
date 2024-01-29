namespace Plugins.Atomic.Elements.Scripts.Interfaces
{
    public interface IAtomicValue<out T>
    {
        T Value { get; }
    }
}