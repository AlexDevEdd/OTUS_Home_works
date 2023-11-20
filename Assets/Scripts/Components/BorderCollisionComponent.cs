using Common.Interfaces;
using UnityEngine;

namespace Components
{
    public sealed class BorderCollisionComponent : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IRemovable>(out var destroy))
                destroy.InvokeRemoveCallback();
        } 
    }
}