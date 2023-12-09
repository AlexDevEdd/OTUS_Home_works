using Bullets;
using Systems.InputSystem;
using UnityEngine;

namespace Character
{
    public sealed class PlayerAttackController
    {
        private const TeamType TEAM_TYPE = TeamType.Player;
        
        private readonly IInputFire _input;
        private readonly BulletSystem _bulletSystem;
        private readonly Transform _firePoint;

        public PlayerAttackController(IInputFire input, BulletSystem bulletSystem, Transform firePoint)
        {
            _input = input;
            _bulletSystem = bulletSystem;
            _firePoint = firePoint;
        }
        
        public void OnStart()
            => _input.OnFireEvent += OnFireEvent;

        public void OnFinish()
            => _input.OnFireEvent -= OnFireEvent;
        
        private void OnFireEvent() 
            => _bulletSystem.Fire(TEAM_TYPE, _firePoint.position, _firePoint.up);
    }
}