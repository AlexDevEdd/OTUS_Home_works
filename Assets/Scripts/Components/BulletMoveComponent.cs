using UnityEngine;

namespace Components
{
    public sealed class BulletMoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private float _speed;
        private bool _isPlayer;

        public void SetSpeed(float speed)
            => _speed = speed;

        public void SetIsPlayer(bool isPlayer)
            => _isPlayer = isPlayer;

        public void UpdatePhysics(float fixedDeltaTime)
            => MoveByRigidbodyVelocity();

        private void MoveByRigidbodyVelocity()
        {
            _rigidbody2D.velocity = _isPlayer 
                    ? transform.forward + Vector3.up * _speed 
                    : transform.forward + -Vector3.up * _speed;
        }
    }
}