using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Factories;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces
{
    public interface IVfxFactory
    {
        public VfxType Type { get; }
        public IVfx Spawn(Transform transform);
        public void Remove(IVfx vfx);
    }
}