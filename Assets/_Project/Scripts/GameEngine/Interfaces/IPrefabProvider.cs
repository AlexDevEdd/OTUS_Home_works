using UnityEngine;

namespace _Project.Scripts.GameEngine.Interfaces
{
    public interface IPrefabProvider
    {
        public T GetPrefab<T>(string key) where T : MonoBehaviour;
    }
}