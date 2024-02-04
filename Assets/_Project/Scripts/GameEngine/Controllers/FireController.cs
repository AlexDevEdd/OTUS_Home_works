using _Project.Scripts.GameEngine.Interfaces;
using Atomic.Objects;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Extensions.Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class FireController : IInitializable, ITickable
    {
        private readonly AtomicObject _character;
        private IAtomicAction _fireAction;
        
        [Inject]
        public FireController(ICharacter character)
        {
            _character = character as AtomicObject;
        }
        
        public void Initialize()
        {
            _fireAction = _character.GetAction(ObjectAPI.FireAction);
        }

        public void Tick()
        {
            Update();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _fireAction?.Invoke();
            }
        }
    }
}