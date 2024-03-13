using Cysharp.Threading.Tasks;

namespace _Project.Scripts.BootStrap.Steps
{
    public abstract class AppStepBase : IAppStep
    {
        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public float Duration { get; protected set; }
        public virtual async UniTask WaitOnCompleted() { }
    }
}