using System;
using System.Collections.Generic;
using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace GameEngine
{
    [Serializable]
    public abstract class AtomicExpression<T> : IAtomicExpression<T>
    {
        protected readonly List<IAtomicValue<T>> members = new();

        public IAtomicExpression<T> AddMember(IAtomicValue<T> member)
        {
            this.members.Add(member);
            return this;
        }

        public IAtomicExpression<T> RemoveMember(IAtomicValue<T> member)
        {
            this.members.Remove(member);
            return this;
        }
        
        public abstract T Invoke();
    }
}