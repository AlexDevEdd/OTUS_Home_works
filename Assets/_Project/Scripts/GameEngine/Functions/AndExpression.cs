using System;
using GameEngine;

namespace _Project.Scripts.GameEngine.Functions
{
    [Serializable]
    public sealed class AndExpression : AtomicExpression<bool>
    {
        public override bool Invoke()
        {
            for (int i = 0, count = members.Count; i < count; i++)
            {
                if (!members[i].Value)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}