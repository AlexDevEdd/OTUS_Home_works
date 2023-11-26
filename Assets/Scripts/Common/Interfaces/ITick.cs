namespace Common.Interfaces
{
    internal interface ITick : IGameListener
    {
        void Tick(float delta);
    }
}