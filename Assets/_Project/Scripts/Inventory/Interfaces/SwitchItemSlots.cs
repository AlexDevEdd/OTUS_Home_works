using UnityEngine;

namespace _Project.Scripts.Inventory.Interfaces
{
    public struct SwitchItemSlots
    {
        public readonly Vector2Int First;
        public readonly Vector2Int Second;

        public SwitchItemSlots(Vector2Int first, Vector2Int second)
        {
            First = first;
            Second = second;
        }
    }
}