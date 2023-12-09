using UnityEngine;

namespace Systems.Points.Views
{
    public abstract class PointView : MonoBehaviour, IPoint
    {
        [SerializeField] private PointType _type;
        
        public PointType Type => _type;
        public Transform Position => transform;
    }
}