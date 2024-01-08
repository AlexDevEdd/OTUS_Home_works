using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using Systems.Points.Views;
using UnityEngine;
using Zenject;

namespace Systems.Points
{
    public class PointSystem : IInitializable
    {
        private readonly Dictionary<PointType, List<IPoint>> _pointViews = new();
        private readonly Transform _pointsRoot;

        [Inject]
        public PointSystem(Transform pointsRoot)
        {
            _pointsRoot = pointsRoot;
        }

        public void Initialize()
        {
            RegisterPoints();
        }

        private void RegisterPoints()
        {
            var points = _pointsRoot.GetComponentsInChildren<IPoint>(true);
            points.ForEach(point =>
            {
                if (_pointViews.TryGetValue(point.Type, out var pointList))
                {
                    pointList.Add(point);
                }
                else
                {
                    _pointViews.TryAdd(point.Type, new List<IPoint>());
                    if (_pointViews.TryGetValue(point.Type, out var list))
                        list.Add(point);
                }
            });
        }


        public IPoint GetFirstPointByType(PointType type)
        {
            if (_pointViews.TryGetValue(type, out var points))
                return points.FirstOrDefault();

            throw new KeyNotFoundException($"Doesn't exist PointType of {type}");
        }
        
        public T[] GetPointsByType<T>(PointType type)
        {
            if (_pointViews.TryGetValue(type, out var points))
                return points.OfType<T>().ToArray();

            throw new KeyNotFoundException($"Doesn't exist PointType of {type}");
        }
    }
}