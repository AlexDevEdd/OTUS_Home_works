using System;
using System.Collections.Generic;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem
{
    [UsedImplicitly]
    public class VfxSystem : IVfxSystem, ICustomInject
    {
        private readonly Dictionary<VfxType, IVfxFactory> _fxFactories = new ();
        
        public VfxSystem(IEnumerable<IVfxFactory> factories)
        {
            foreach (var factory in factories)
            {
                _fxFactories.TryAdd(VfxType.Soul, factory);
            }
        }

        public void PlayFx(VfxType type, Transform transform)
        {
            if (_fxFactories.TryGetValue(type, out var factory))
            {
                var vfx = factory.Spawn(transform);
                vfx.Show(transform.position);
                vfx.OnVfxEnd += Remove;
            }
            else
                throw new ArgumentException($"Doesn't exist FxType of {type}");
        }
        
        private void Remove(IVfx vfx)
        {
            vfx.OnVfxEnd -= Remove;
            
            if (_fxFactories.TryGetValue(vfx.Type, out var factory)) 
                factory.Remove(vfx);
        }
    }
}