
using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.UI
{
    public class MoneyAdd : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _moneyText;
        [SerializeField] GameObject _keepPlayingObject;
        [SerializeField] TextMeshProUGUI _keepPlayingText;
        [SerializeField] TextMeshProUGUI _needLevelText;
        
        private Tween _tween;
        private Action OnOpenedPanel;
        private Action OnClosedPanel;
        
        public void SetPosition(Vector3 newPos)
        {
            transform.position = newPos;
        }

        public void UpdatePosition(Vector3 position)
        {
            SetPosition(position);
            transform.localRotation = new Quaternion(0, 0, 0, 1);
        }
        
        public void ShowMoneyCount(string text)
        {
            // Show();
            // _moneyText.text = "+ " + text + "$";
            // if (_tween != null)
            // {
            //     _tween.Kill();
            // }
            // _tween = transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
            //
            //  _moneyText.DOFade(0, 0.66f).SetDelay(0.3f).SetEase(Ease.Linear).OnComplete(() => Hide());
            //
            // transform.DOLocalMoveY(0.15f,0.66f).SetDelay(0.3f).SetRelative(true);
        }
        
        public void ShowAnyText(string text)
        {
            // Show();
            // _moneyText.text = text;
            // _tween?.Kill();
            // _tween = transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
            // _moneyText.DOFade(0, 0.66f).SetDelay(0.3f).SetEase(Ease.Linear).OnComplete(Hide);
            // transform.DOLocalMoveY(0.15f,0.66f).SetDelay(0.3f).SetRelative(true);
        }
    }
}
