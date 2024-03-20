using UnityEngine;

namespace _Project.Scripts.CustomPool
{
    public sealed class WorldSpaceContainer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}