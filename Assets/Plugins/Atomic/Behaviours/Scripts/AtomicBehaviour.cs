using System.Collections.Generic;
using Atomic.Objects;
using Sirenix.OdinInspector;

namespace Plugins.Atomic.Behaviours.Scripts
{
    public class AtomicBehaviour : AtomicObject
    {
        [Title("Logic"), PropertySpace, PropertyOrder(150)]
        [ShowInInspector, HideInEditorMode]
        private HashSet<ILogic> _logicSet;
        
        [ShowInInspector, HideInEditorMode, PropertyOrder(150)]
        private Dictionary<string, ILogic> _logicMap;

        private List<IEnable> _enables;
        private List<IDisable> _disables;
        private List<IUpdate> _updates;
        private List<IFixedUpdate> _fixedUpdates;
        private List<ILateUpdate> _lateUpdates;

        public override void Compose()
        {
            base.Compose();
            _logicSet = new HashSet<ILogic>();
            _logicMap = new Dictionary<string, ILogic>();

            _enables = new List<IEnable>();
            _disables = new List<IDisable>();

            _updates = new List<IUpdate>();
            _fixedUpdates = new List<IFixedUpdate>();
            _lateUpdates = new List<ILateUpdate>();
        }

        public bool AddLogic(string key, ILogic target)
        {
            if (_logicMap.TryAdd(key, target))
            {
                return AddLogic(target);
            }

            return false;
        }

        public bool AddLogic(ILogic target)
        {
            if (target == null)
            {
                return false;
            }

            if (!_logicSet.Add(target))
            {
                return false;
            }

            if (target is IEnable enable)
            {
                _enables.Add(enable);

                if (enabled)
                {
                    enable.Enable();
                }
            }

            if (target is IDisable disable)
            {
                _disables.Add(disable);
            }

            if (target is IUpdate update)
            {
                _updates.Add(update);
            }

            if (target is IFixedUpdate fixedUpdate)
            {
                _fixedUpdates.Add(fixedUpdate);
            }

            if (target is ILateUpdate lateUpdate)
            {
                _lateUpdates.Add(lateUpdate);
            }

            return true;
        }
        
        public bool RemoveLogic(string key)
        {
            if (_logicMap.Remove(key, out var target))
            {
                return RemoveLogic(target);
            }

            return false;
        }

        public bool RemoveLogic(ILogic target)
        {
            if (target == null)
            {
                return false;
            }
            
            if (!_logicSet.Remove(target))
            {
                return false;
            }

            if (target is IEnable enable)
            {
                _enables.Remove(enable);
            }

            if (target is IUpdate tickable)
            {
                _updates.Remove(tickable);
            }

            if (target is IFixedUpdate fixedTickable)
            {
                _fixedUpdates.Remove(fixedTickable);
            }

            if (target is ILateUpdate lateTickable)
            {
                _lateUpdates.Remove(lateTickable);
            }

            if (target is IDisable disable)
            {
                if (enabled)
                {
                    disable.Disable();
                }
            }

            return true;
        }

        public void AddLogics(IEnumerable<ILogic> targets)
        {
            foreach (var target in targets)
            {
                AddLogic(target);
            }
        }

        public void RemoveLogics(IEnumerable<ILogic> targets)
        {
            foreach (var target in targets)
            {
                RemoveLogic(target);
            }
        }

        public bool FindLogic<T>(out T result) where T : ILogic
        {
            foreach (var element in _logicSet)
            {
                if (element is T tElement)
                {
                    result = tElement;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public bool RemoveLogic<T>() where T : ILogic
        {
            foreach (var element in _logicSet)
            {
                if (element is T)
                {
                    RemoveLogic(element);
                    return true;
                }
            }

            return false;
        }

        protected virtual void OnEnable()
        {
            for (int i = 0, count = _enables.Count; i < count; i++)
            {
                IEnable enable = _enables[i];
                enable.Enable();
            }
        }

        protected virtual void OnDisable()
        {
            for (int i = 0, count = _disables.Count; i < count; i++)
            {
                IDisable disable = _disables[i];
                disable.Disable();
            }
        }

        protected virtual void Update()
        {
            for (int i = 0, count = _updates.Count; i < count; i++)
            {
                IUpdate update = _updates[i];
                update.Update();
            }
        }

        protected virtual void FixedUpdate()
        {
            for (int i = 0, count = _fixedUpdates.Count; i < count; i++)
            {
                IFixedUpdate fixedUpdate = _fixedUpdates[i];
                fixedUpdate.OnFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            for (int i = 0, count = _lateUpdates.Count; i < count; i++)
            {
                ILateUpdate lateUpdate = _lateUpdates[i];
                lateUpdate.OnLateUpdate();
            }
        }
    }
}