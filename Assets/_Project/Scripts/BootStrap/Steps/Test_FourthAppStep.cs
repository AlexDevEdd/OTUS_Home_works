using _Project.Scripts.ScriptableConfigs;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.BootStrap.Steps
{
    [UsedImplicitly]
    public sealed class Test_FourthAppStep : AppStepBase
    {
        private const int ID  = 4;
        
        public Test_FourthAppStep(GameBalance gameBalance)
        {
            var config = gameBalance.AppStepConfigs.GetConfig(ID);
            Id = ID;
            Title = config.TitleText;
            Duration = config.Duration; 
        }
        
        public override async UniTask WaitOnCompleted()
        {
            await UniTask.Delay((int)(Duration * 1000));
            await UniTask.CompletedTask;
        }
    }
}