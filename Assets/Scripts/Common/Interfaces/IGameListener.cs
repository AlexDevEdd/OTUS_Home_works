namespace Common.Interfaces
{
    public interface IGameListener { }
    
    internal interface IGameFinish : IGameListener
    {
        void OnFinish();
    }
    
    internal interface IGamePause : IGameListener
    {
        void OnPause();
    }
    
    internal interface IGameResume : IGameListener
    {
        void OnResume();
    }
    
    internal interface IGameStart : IGameListener
    {
        void OnStart();
    }
}