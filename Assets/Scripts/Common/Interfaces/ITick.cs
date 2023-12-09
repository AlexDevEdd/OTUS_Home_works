namespace Common.Interfaces
{
    internal interface ITick : IGameListener
    {
        void Tick(float delta);
    }
    
    internal interface IFixedTick : IGameListener
    {
        void FixedTick(float fixedDelta);
    }
    
    internal interface ILateTick : IGameListener
    {
        void LateTick(float delta);
    }
}