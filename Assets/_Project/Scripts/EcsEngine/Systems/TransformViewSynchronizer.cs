using _Game.Scripts.Tools;
using _Project.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class TransformViewSynchronizer : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> filter;
        private readonly EcsPoolInject<Rotation> rotationPool;

        void IEcsPostRunSystem.PostRun(IEcsSystems systems)
        {
           // Log.ColorLog("TransformViewSynchronizer", ColorType.Aqua , LogStyle.Warning);
            
            EcsPool<Rotation> rotationPool = this.rotationPool.Value;

            foreach (int entity in this.filter.Value)
            {
                ref TransformView transform = ref this.filter.Pools.Inc1.Get(entity);
                Position position = this.filter.Pools.Inc2.Get(entity);
                
                transform.Value.position = position.Value;

                if (rotationPool.Has(entity))
                {
                    Quaternion rotation = rotationPool.Get(entity).Value;
                    transform.Value.rotation = rotation;
                }
            }
        }
    }
}