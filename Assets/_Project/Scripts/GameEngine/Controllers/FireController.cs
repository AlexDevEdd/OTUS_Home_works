using Atomic.Elements;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class FireController
    {
        private readonly IAtomicAction _fireAction;

        public FireController(IAtomicAction fireAction)
        {
            _fireAction = fireAction;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _fireAction?.Invoke();
            }
        }
    }
}