using _Project.Scripts.BootStrap.Steps;
using _Project.Scripts.Core.Installers;
using Zenject;

namespace _Project.Scripts.BootStrap
{
    public class AppStepInstaller : BaseInstaller
    {
        public AppStepInstaller(DiContainer container) : base(container)
        {
            BindInterfacesAndSelfTo<Test_FirstAppStep>(container);
            BindInterfacesAndSelfTo<Test_SecondAppStep>(container);
            BindInterfacesAndSelfTo<Test_ThirdAppStep>(container);
            BindInterfacesAndSelfTo<Test_FourthAppStep>(container);
        }
    }
}