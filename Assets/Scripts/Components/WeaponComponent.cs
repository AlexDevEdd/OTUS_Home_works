using System;
using DG.Tweening;
using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public event Action<Transform> OnFire;
        
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _coolDown;
        
        private Tween _tween;
        
        private void OnEnable()
        {
            _tween = DOVirtual.DelayedCall(_coolDown, () =>
            {
                OnFire?.Invoke(_firePoint);
            }).SetLoops(-1);
        }
        
        private void OnDisable() 
            => _tween?.Kill();
    }
}