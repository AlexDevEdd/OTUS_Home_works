using UnityEngine;

namespace _Project.Scripts.GameEngine.Interfaces
{
    public interface IZombieFactory
    {
        public void Create(Vector3 position, Quaternion rotation);
        public void Remove(IZombie zombie);
    }
}