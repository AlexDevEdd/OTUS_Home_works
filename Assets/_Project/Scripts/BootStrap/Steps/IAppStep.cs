using Cysharp.Threading.Tasks;

namespace _Project.Scripts.BootStrap.Steps
{
    public interface IAppStep
    {
        public int Id { get; }
        public string Title { get; }
        public float Duration { get; }
        public UniTask WaitOnCompleted();
    }
}