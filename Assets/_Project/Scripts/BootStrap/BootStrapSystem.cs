using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.BootStrap.Steps;
using _Project.Scripts.UI.Windows;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.BootStrap
{
    [UsedImplicitly]
    public class BootStrapSystem : IInitializable
    {
        private const int NEXT_SCENE_BUILD_ID = 1;
        
        private readonly LoadingWindow _loadingWindow;
        private readonly List<IAppStep> _steps;

        public BootStrapSystem(LoadingWindow loadingWindow, IEnumerable<IAppStep> steps)
        {
            _loadingWindow = loadingWindow;
            _steps = steps.OrderBy(s => s.Id).ToList();
        }

        public void Initialize()
        {
            StartAllStep().Forget();
        }
        
        private async UniTaskVoid StartAllStep()
        {
            var countStep = _steps.Count;
            _loadingWindow.ResetProgress();
            
            for (var index = 0; index < countStep; index++)
            {
                var step = _steps[index];
                _loadingWindow.UpdateTitle(step.Title);
                _loadingWindow.UpdateProgress( 1, step.Duration);
                
                await step.WaitOnCompleted();
                _loadingWindow.ResetProgress();
            }
            RunScene().Forget();
        }
        
        private async UniTaskVoid RunScene()
        {
            await SceneManager.LoadSceneAsync(NEXT_SCENE_BUILD_ID, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(0).ToUniTask().Forget();
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(NEXT_SCENE_BUILD_ID));
        }
    }
}