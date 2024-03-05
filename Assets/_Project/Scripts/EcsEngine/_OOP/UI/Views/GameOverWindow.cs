using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.EcsEngine._OOP.UI.Views
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private EcsAdmin _ecsAdmin;

        [Inject]
        private void Construct(EcsAdmin ecsAdmin)
        {
            _ecsAdmin = ecsAdmin;
        }
        
        public void Show()
        {
            // gameObject.SetActive(false);
            // _canvasGroup.DOFade(1, 0.5f)
            //     .OnComplete(ClearWorlds);
        }

        private void ClearWorlds()
        {
            _ecsAdmin.Dispose();
        }
    }
}