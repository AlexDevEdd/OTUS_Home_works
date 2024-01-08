using Systems.Points;
using UnityEngine;
using Zenject;

namespace GameCore.Installers
{
    public sealed class PointInstaller
    {
        public PointInstaller(DiContainer container, Transform pointsRoot)
        {
            BindPointSystem(container, pointsRoot);
        }
        
        private void BindPointSystem(DiContainer container, Transform pointsRoot)
        {
            container.BindInterfacesAndSelfTo<PointSystem>()
                .AsSingle()
                .WithArguments(pointsRoot)
                .NonLazy();
        }
    }
}