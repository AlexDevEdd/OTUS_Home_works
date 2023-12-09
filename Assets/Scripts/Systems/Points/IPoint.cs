using Systems.Points.Views;
using UnityEngine;

namespace Systems.Points
{
    public interface IPoint
    {
        public PointType Type { get; }
        public Transform Position { get; }
    }
}