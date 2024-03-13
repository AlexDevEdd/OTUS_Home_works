using UnityEngine;

namespace _Project.Scripts.CustomPool
{
    public sealed class PoolsContainer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}