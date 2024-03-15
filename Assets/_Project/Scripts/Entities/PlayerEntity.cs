using _Project.Scripts.Components;

namespace _Project.Scripts.Entities
{
    public sealed class PlayerEntity : Entity
    {
        private void Awake()
        {
            Add(new PlayerMoveComponent());
            Add(new PlayerDamageComponent());
            Add(new PlayerHealthComponent());
        }
    }
}