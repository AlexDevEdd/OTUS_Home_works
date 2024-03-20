using _Project.Scripts.ScriptableConfigs;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace _Project.Scripts.BootStrap.Steps
{
    [UsedImplicitly]
    public sealed class Test_ThirdAppStep : AppStepBase
    {
        private const int ID  = 3;
        
        public Test_ThirdAppStep(GameBalance gameBalance)
        {
            var config = gameBalance.GetAppStepConfig(ID);
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