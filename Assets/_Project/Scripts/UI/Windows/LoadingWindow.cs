using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Windows
{
    public sealed class LoadingWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _progressTitle;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Image _fillImage;
        
        private Tween _fillAmountTween;
        
        public void UpdateTitle(string text)
        {
            _progressTitle.text = text;
        }
        
        public void UpdateProgress(float fillAmount, float duration)
        {
            _fillAmountTween?.Kill();
            _fillAmountTween = _fillImage.DOFillAmount(fillAmount, duration).SetEase(Ease.Linear);
            _fillAmountTween.OnUpdate(FillAmountCallback);
        }

        private void FillAmountCallback()
        {
            var progress = _fillAmountTween.ElapsedPercentage();
            _progressText.text = $"{progress * 100:0.}%";
        }

        public void ResetProgress()
        {
            _fillImage.fillAmount = 0;
        }
    }
}