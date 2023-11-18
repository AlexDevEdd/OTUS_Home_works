using Bullets;
using Input;
using UnityEngine;

namespace Components
{
    public sealed class PlayerWeaponComponent : MonoBehaviour
    {
        [SerializeField] private InputListener _input;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private Transform _firePoint;
        
        private void OnEnable() 
            => _input.OnFire += OnOnFire;
        
        private void OnDisable()
            => _input.OnFire -= OnOnFire;
        
        private void OnOnFire()
            => bulletSystem.Fire(BulletType.Player, _firePoint.position, _firePoint.rotation);
        
    }
}