using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public sealed class UserInfo
    {
        // public event Action<string> OnNameChanged;
        // public event Action<string> OnDescriptionChanged;
        // public event Action<Sprite> OnIconChanged; 

        //[ShowInInspector, ReadOnly]
        public string Name;

        //[ShowInInspector, ReadOnly]
        public string Description;

        //[ShowInInspector, ReadOnly]
        public Sprite Icon;

        // [Button]
        // public void ChangeName(string name)
        // {
        //     Name = name;
        //    // OnNameChanged?.Invoke(name);
        // }
        //
        // [Button]
        // public void ChangeDescription(string description)
        // {
        //     Description = description;
        //    // OnDescriptionChanged?.Invoke(description);
        // }
        //
        // [Button]
        // public void ChangeIcon(Sprite icon)
        // {
        //     Icon = icon;
        //    // OnIconChanged?.Invoke(icon);
        // }
    }
}