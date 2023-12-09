using Bullets;
using Common.Interfaces;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class PlayerAttackController : MonoBehaviour, IGameStart, IGameFinish
    {
        [SerializeField] private InputListener _input;
        [SerializeField] private BulletConfig _config;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private Transform _firePoint;
        
        public void OnStart()
            => _input.OnFire += OnFireEvent;

        public void OnFinish()
            => _input.OnFire -= OnFireEvent;
        
        private void OnFireEvent() 
            => _bulletSystem.Fire(_config, _firePoint.position, _firePoint.up);
        
    }
}