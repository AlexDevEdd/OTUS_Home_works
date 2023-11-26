using Common.Interfaces;
using UnityEngine;

namespace GameCore
{
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        
        private void Awake()
        {
            var listeners = GetComponentsInChildren<IGameListener>(true);
            _gameManager.Init(listeners);
        }
    }
}