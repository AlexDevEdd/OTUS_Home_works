namespace Common.Interfaces
{
    internal interface IFixedTick : IGameListener
    {
        void FixedTick(float fixedDelta);
    }
}