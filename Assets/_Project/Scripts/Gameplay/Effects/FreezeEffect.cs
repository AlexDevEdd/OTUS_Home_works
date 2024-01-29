using System;
using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Objects.Scripts;
using UnityEngine;

namespace Sample
{
    public sealed class FreezeEffect : MonoBehaviour, IEnable, IDisable, IUpdate
    {
        private static readonly AtomicValue<bool> FREEZE = new(false);

        private IAtomicObject target;
        
        private Action finishCallback;
        
        [SerializeField]
        private Countdown countdown;

        public void Compose(IAtomicObject target, Action finishCallback)
        {
            this.target = target;
            this.finishCallback = finishCallback;
        }

        public void Enable()
        {
            this.target.Get<AndExpression>("MoveCondition")?.AddMember(FREEZE);
            this.target.Get<AndExpression>("FireCondition")?.AddMember(FREEZE);
        }

        public void Disable()
        {
            this.target.Get<AndExpression>("MoveCondition")?.RemoveMember(FREEZE);
            this.target.Get<AndExpression>("FireCondition")?.RemoveMember(FREEZE);
        }

        public void Update()
        {
            this.countdown.Tick(Time.deltaTime);

            if (this.countdown.IsEnded())
            {
                this.finishCallback?.Invoke();
            }
        }
    }
}